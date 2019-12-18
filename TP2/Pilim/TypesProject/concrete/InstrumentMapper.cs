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
    class InstrumentMapper: IInstrumentMapper
    {
        MapperHelper<IInstrument, string, List<IInstrument>> mapperHelper;
        public InstrumentMapper(IContext ctx) 
        {
            mapperHelper = new MapperHelper<IInstrument, string, List<IInstrument>>(ctx, this);
        }
        internal ICollection<IDailyReg> LoadDailyRegs(Instrument i)
        {
            List<IDailyReg> lst = new List<IDailyReg>();

            DailyRegMapper drm = new DailyRegMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", i.isin));
            using (IDataReader rd = mapperHelper.ExecuteReader("select dailyregid, dailyregdt from instrumentdailyreg where instrumentId=@id", parameters))
            {
                while (rd.Read())
                {
                    KeyValuePair<string, DateTime> pair = new KeyValuePair<string, DateTime>(rd.GetString(0), rd.GetDateTime(1));
               
                    lst.Add(drm.Read(pair));
                }
            }
            return lst;
        }
        internal ICollection<IPosition> LoadPosition(Instrument i)
        {
            List<IPosition> lst = new List<IPosition>();

            PositionMapper pm = new PositionMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", i.isin));
            using (IDataReader rd = mapperHelper.ExecuteReader("select portfolioid from portfolioinstrument where instrumentId=@id", parameters))
            {
                while (rd.Read())
                {
                    KeyValuePair<string, string> pair = new KeyValuePair<string, string>(rd.GetString(0), rd.GetString(1));
                    lst.Add(pm.Read(pair));
                }
            }
            return lst;
        }
        internal IMarket LoadMarket(Instrument i)
        {
            MarketMapper mm = new MarketMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", i.isin));
            using (IDataReader rd = mapperHelper.ExecuteReader("select market from instrument where instrumentId=@id", parameters))
            {
                if (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    return mm.Read(key);
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
                     "INSERT INTO Instrument (isin, mrktcode, description) VALUES(@id, @code, @desc); select @id=isin"
                    );
                ts.Complete();
                return instrument;
            }

        }

        public IInstrument Read(string id)
        {
            return mapperHelper.Read(id,
                (cmd, i) => SelectParameters(cmd, i),
                "select isin, mrktcode, description from Instrument where instrumentId=@id"
                );
        }

        public List<IInstrument> ReadAll()
        {
            return mapperHelper.ReadAll(
                cmd => { },
                "select isin, mrktcode, description from Instrument"
                );
        }

        public bool Update(IInstrument entity)
        {
            return mapperHelper.Update(entity,
                (cmd, ins) => UpdateParameters(cmd,ins),
                "update Instrument set mrktcode = @code, description = @desc where instrumentId = @id"
                );
            
        }

        public bool Delete(IInstrument entity)
        {
            return mapperHelper.Delete(entity,
                (cmd, ins) => DeleteParameters(cmd, ins),
                "delete from Instrument where instrumentId=@id"
                );
        }

   

        public List<IInstrument> MapAll(IDataReader reader)
        {
            return mapperHelper.MapAll(reader);
        }

       

       
    }
}

