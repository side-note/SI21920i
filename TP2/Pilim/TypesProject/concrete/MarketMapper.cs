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

            DailyMarketMapper dmm = new DailyMarketMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", m.code));
            using (IDataReader rd = mapperHelper.ExecuteReader("select dailymarketid, dailymarketdt from marketdailymarket where marketId=@id", parameters))
            {
                while (rd.Read())
                {
                    KeyValuePair<int, DateTime> pair = new KeyValuePair<int, DateTime>(rd.GetInt32(0), rd.GetDateTime(1));
                    lst.Add(dmm.Read(pair));
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
            using (IDataReader rd = mapperHelper.ExecuteReader("select instrumentid from marketinstrument where marketId=@id", parameters))
            {
                while (rd.Read())
                {
                    string key = rd.GetString(0);
                    lst.Add(im.Read(key));
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
            SqlParameter n = new SqlParameter("@Name", m.name);
            SqlParameter id = new SqlParameter("@id", m.code);
            SqlParameter d = new SqlParameter("@desc", m.description);
            id.Direction = ParameterDirection.InputOutput;

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
            m.code = record.GetInt32(0);
            m.name = record.GetString(1);
            m.description = record.GetString(2);
            return new MarketProxy(m, mapperHelper.context);
        }
        public IMarket Create(IMarket market)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                mapperHelper.Create(market,
                    (cmd, market) => InsertParameters(cmd, market),
                    "INSERT INTO Market (code, description, name) VALUES(@id,@desc,@name); select @id=code"
                    );
                ts.Complete();
                return market;
            }
        }


        public bool Update(IMarket market)
        {
            return mapperHelper.Update(market,
                 (cmd, market) => UpdateParameters(cmd, market),
                 "update Market set description=@desc, name=@name where mrktId=@id"
                 );
        }

        public IMarket Read(int id)
        {
            return mapperHelper.Read(id,
              (cmd, i) => SelectParameters(cmd, i),
             "select code, description,name from Market where mrktId=@id"
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
                 "delete from Market where mrktId=@id"
                 );
        }

        public List<IMarket> MapAll(IDataReader reader)
        {
            return mapperHelper.MapAll(reader);
        }
    }
}

