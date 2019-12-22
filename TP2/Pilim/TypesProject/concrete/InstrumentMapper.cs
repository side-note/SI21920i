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
   public class InstrumentMapper: IInstrumentMapper
    {
        MapperHelper<IInstrument, string, List<IInstrument>> mapperHelper;
        public InstrumentMapper(IContext ctx) 
        {
            mapperHelper = new MapperHelper<IInstrument, string, List<IInstrument>>(ctx, this);
        }
        internal ICollection<IDailyReg> LoadDailyRegs(IInstrument i)
        {
            List<IDailyReg> lst = new List<IDailyReg>();

            DailyRegMapper drm = new DailyRegMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", i.isin));
            using (IDataReader rd = mapperHelper.ExecuteReader("select isin,minval ,openingval, maxval,closingval, dailydate from DailyReg where isin=@id", parameters))
            {
                while (rd.Read())
                {
                    DailyReg dr = new DailyReg
                    {
                        isin = rd.GetString(0),
                        minval = rd.GetDecimal(1),
                        openingval = rd.GetDecimal(2),
                        maxval = rd.GetDecimal(3),
                        closingval = rd.GetDecimal(4),
                        dailydate = rd.GetDateTime(5),
                        instrument = i
                    };

                    lst.Add(dr);
                }
            }
            return lst;
        }
        internal ICollection<IPosition> LoadPosition(IInstrument i)
        {
            List<IPosition> lst = new List<IPosition>();

            PortfolioMapper pm = new PortfolioMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", i.isin));
            using (IDataReader rd = mapperHelper.ExecuteReader("select isin, name, quantity from Position where isin=@id ", parameters))
            {
                while (rd.Read())
                {
                    Position pos = new Position {
                        Instrument = i,
                        Portfolio = pm.Read(rd.GetString(0)),
                        isin=rd.GetString(0),
                        name=rd.GetString(1),
                        quantity= rd.GetInt32(2)
                    };
                    lst.Add(pos);
                }
            }
            return lst;
        }
        internal IMarket LoadMarket(IInstrument i)
        {
            MarketMapper mm = new MarketMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", i.mrktcode));
            using (IDataReader rd = mapperHelper.ExecuteReader("select code, name, description from Market where mrktcode=@id", parameters))
            {
                if (rd.Read())
                {
                    Market m = new Market
                    {
                        code = rd.GetInt32(0),
                        name = rd.GetString(1),
                        description = rd.GetString(2)
                    };
                    return new MarketProxy(m,mapperHelper.context);
                }
            }
            return null;
        }

        protected void DeleteParameters(IDbCommand cmd, IInstrument i)
        {

            SqlParameter id = new SqlParameter("@id", i.isin);
            cmd.Parameters.Add(id);
        }

        protected void InsertParameters(IDbCommand cmd, IInstrument i)
        {
            SqlParameter desc = new SqlParameter("@desc", i.description);
            SqlParameter id = new SqlParameter("@id", i.isin);
            SqlParameter mc = new SqlParameter("@code", i.mrktcode);
            id.Direction = ParameterDirection.InputOutput;


            cmd.Parameters.Add(desc);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(mc);
        }


        protected void SelectParameters(IDbCommand cmd, string k)
        {
            SqlParameter p1 = new SqlParameter("@id", k);
            cmd.Parameters.Add(p1);
        }

      
        protected void UpdateParameters(IDbCommand cmd, IInstrument i)
        {
            InsertParameters(cmd, i);
        }

        public IInstrument Map(IDataRecord record)
        {
            Instrument i = new Instrument();
            i.isin = record.GetString(0);
            i.description = record.GetString(1);
            i.mrktcode = record.GetInt32(2);
            return new InstrumentProxy(i, mapperHelper.context);
        }
        public  IInstrument Create(IInstrument instrument)
        {

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                mapperHelper.Create(instrument,
                    (cmd, ins) => InsertParameters(cmd, ins),
                     "INSERT INTO Instrument (isin, description, mrktcode) VALUES(@id, @desc, @code); select @id=isin"
                    );
                ts.Complete();
                return instrument;
            }

        }

        public IInstrument Read(string id)
        {
            return mapperHelper.Read(id,
                (cmd, i) => SelectParameters(cmd, i),
                "select isin, description, mrktcode from Instrument where isin=@id"
                );
        }

        public List<IInstrument> ReadAll()
        {
            return mapperHelper.ReadAll(
                cmd => { },
                "select isin, description, mrktcode from Instrument"
                );
        }

        public bool Update(IInstrument entity)
        {
            return mapperHelper.Update(entity,
                (cmd, ins) => UpdateParameters(cmd,ins),
                "update Instrument set description = @desc, mrktcode = @code where isin=@id"
                );
            
        }

        public bool Delete(IInstrument entity)
        {
            return mapperHelper.Delete(entity,
                (cmd, ins) => DeleteParameters(cmd, ins),
                "delete from Instrument where isin=@id"
                );
        }

   

        public List<IInstrument> MapAll(IDataReader reader)
        {
            return mapperHelper.MapAll(reader);
        }

       

       
    }
}

