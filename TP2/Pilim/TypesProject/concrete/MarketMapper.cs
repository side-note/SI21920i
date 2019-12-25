using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TypesProject.mapper;
using TypesProject.model;

namespace TypesProject.concrete
{
    public class MarketMapper: IMarketMapper
    {
        MapperHelper<IMarket, int, List<IMarket>> mapperHelper;
        public MarketMapper(IContext ctx)
        {
            mapperHelper = new MapperHelper<IMarket, int, List<IMarket>>(ctx, this);
        }
        internal ICollection<IDailyMarket> LoadDailyMarkets(Market m)
        {
            List<IDailyMarket> lst = new List<IDailyMarket>();
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", m.code));
            using (IDataReader rd = mapperHelper.ExecuteReader("select idxmrkt, dailyvar, idxopeningval, code, date from dailymarket where code=@id", parameters))
            {
                while (rd.Read())
                {
                    DailyMarket dm = new DailyMarket
                    {
                        code = rd.IsDBNull(3) ? default : rd.GetInt32(3),
                        dailyvar = rd.IsDBNull(1) ? default : rd.GetDecimal(1),
                        date = rd.IsDBNull(4) ? default : rd.GetDateTime(4),
                        idxmrkt = rd.IsDBNull(0) ? default : rd.GetDecimal(0),
                        idxopeningval = rd.IsDBNull(2) ? default : rd.GetDecimal(2),
                        market = m
                    };
                    lst.Add(dm);
                }
            }
            return lst;
        }
        internal ICollection<IInstrument> LoadInstruments(Market m)
        {
            List<IInstrument> lst = new List<IInstrument>();

            InstrumentMapper im = new InstrumentMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", m.code));
            using (IDataReader rd = mapperHelper.ExecuteReader("select isin, mrktcode, description from Instrument where mrktcode=@id", parameters))
            {
                while (rd.Read())
                {
                    Instrument i = new Instrument
                    {
                        isin = rd.IsDBNull(0) ? default : rd.GetString(0),
                        mrktcode = rd.IsDBNull(1) ? default : rd.GetInt32(1),
                        description = rd.IsDBNull(2) ? default : rd.GetString(2),
                        instrumentMarket = m
                    };

                    lst.Add(new InstrumentProxy(i,mapperHelper.context));
                }
            }
            return lst;

        }

        protected void DeleteParameters(IDbCommand cmd, IMarket m)
        {

            SqlParameter id = new SqlParameter("@id", m.code);
            cmd.Parameters.Add(id);
        }

        protected void InsertParameters(IDbCommand cmd, IMarket m)
        {
            SqlParameter n = new SqlParameter("@name", m.name);
            SqlParameter id = new SqlParameter("@id", m.code);
            SqlParameter d = new SqlParameter("@desc", m.description);

            cmd.Parameters.Add(n);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(d);
        }


        protected void SelectParameters(IDbCommand cmd, int k)
        {
            SqlParameter id = new SqlParameter("@id", k);
            cmd.Parameters.Add(id);
        }

        protected void UpdateParameters(IDbCommand cmd, IMarket m)
        {
            InsertParameters(cmd, m);
        }

        public IMarket Map(IDataRecord record)
        {
            Market m = new Market();
            m.code = record.IsDBNull(0) ? default : record.GetInt32(0);
            m.description = record.IsDBNull(1) ? default : record.GetString(1);
            m.name = record.IsDBNull(2) ? default : record.GetString(2);
            return new MarketProxy(m, mapperHelper.context);
        }
        public IMarket Create(IMarket market)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                mapperHelper.Create(market,
                    (cmd, market) => InsertParameters(cmd, market),
                    "INSERT INTO Market (code, description, name) VALUES(@id,@desc,@name)"
                    );
                ts.Complete();
                return market;
            }
        }


        public bool Update(IMarket market)
        {
            return mapperHelper.Update(market,
                 (cmd, market) => UpdateParameters(cmd, market),
                 "update Market set description=@desc, name=@name where code=@id"
                 );
        }

        public IMarket Read(int id)
        {
            return mapperHelper.Read(id,
              (cmd, i) => SelectParameters(cmd, i),
             "select code, description,name from Market where code=@id"
              );
        }

        public List<IMarket> ReadAll()
        {
            return mapperHelper.ReadAll(
            cmd => { },
            "select code, description,name from Market"
            );
        }

        public bool Delete(IMarket entity)
        {
            return mapperHelper.Delete(entity,
                 (cmd, dailyreg) => DeleteParameters(cmd, dailyreg),
                 "delete from Market where code=@id"
                 );
        }

        public List<IMarket> MapAll(IDataReader reader)
        {
            return mapperHelper.MapAll(reader);
        }
    }
}

