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
    class DailyRegMapper : IDailyRegMapper
    {
        MapperHelper<IDailyReg, KeyValuePair<string, DateTime>, List<IDailyReg>> mapperHelper;
        public DailyRegMapper(IContext ctx)
        {
            mapperHelper = new MapperHelper<IDailyReg, KeyValuePair<string, DateTime>, List<IDailyReg>>(ctx, this);
        }
        internal IInstrument LoadInstrument(DailyReg dr)
        {
            InstrumentMapper im = new InstrumentMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id",dr.isin));
            parameters.Add(new SqlParameter("@datetime", dr.dailydate));

            using (IDataReader rd = mapperHelper.ExecuteReader("select instrument from dailyreg where dailyregId=@id and dailyregdt=@datetime ", parameters))
            {
                if (rd.Read())
                {
                    string key = rd.GetString(0);
                    return im.Read(key);
                }
            }
            return null;
        }

        protected void DeleteParameters(IDbCommand cmd, IDailyReg dr)
        {

            SqlParameter id = new SqlParameter("@id",dr.isin);
            SqlParameter dt = new SqlParameter("@datetime",dr.dailydate);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(dt);
        }

        protected void InsertParameters(IDbCommand cmd, IDailyReg dr)
        {
            SqlParameter dt = new SqlParameter("@datetime", dr.dailydate);
            SqlParameter id = new SqlParameter("@id", dr.isin);
            SqlParameter miv = new SqlParameter("@minval", dr.minval);
            SqlParameter mav = new SqlParameter("@maxval", dr.maxval);
            SqlParameter ov = new SqlParameter("@openingval", dr.openingval);
            SqlParameter cv = new SqlParameter("@closingval", dr.closingval);

            dt.Direction = ParameterDirection.InputOutput;
            id.Direction = ParameterDirection.InputOutput;


         
            cmd.Parameters.Add(dt);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(miv);
            cmd.Parameters.Add(mav);
            cmd.Parameters.Add(ov);
            cmd.Parameters.Add(cv);
        }
        protected void SelectParameters(IDbCommand command, KeyValuePair<string, DateTime> k)
        {
            SqlParameter id = new SqlParameter("@id", k.Key);
            SqlParameter dt = new SqlParameter("@datetime", k.Value);
            command.Parameters.Add(id);
            command.Parameters.Add(dt);
        }

        

    
        protected void UpdateParameters(IDbCommand cmd, IDailyReg dr)
        {
            InsertParameters(cmd, dr);
        }

        public IDailyReg Map(IDataRecord record)
        {
            DailyReg dr = new DailyReg();
            dr.maxval = record.GetDouble(0);
            dr.minval = record.GetDouble(1);
            dr.openingval = record.GetDouble(2);
            dr.closingval = record.GetDouble(3);
            dr.dailydate = record.GetDateTime(4);

            return new DailyRegProxy(dr, mapperHelper.context,record.GetInt32(5));
        }

        public IDailyReg Create(IDailyReg entity)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                mapperHelper.Create(entity,
                    (cmd, dailyReg) => InsertParameters(cmd, dailyReg),
                     "INSERT INTO DailyReg (isin, minval, openingval,  maxval, closingval, dailydate) VALUES(@id, @minval, @openingval, @maxval, @closingval, @datetime); select @id=isin, @datetime=dailydate"
                    );
                ts.Complete();
                return entity;
            }
        }

        public IDailyReg Read(KeyValuePair<string, DateTime> id)
        {
            return mapperHelper.Read(id,
               (cmd, i) => SelectParameters(cmd, i),
               "select isin, minval, openingval,  maxval, closingval, dailydate from DailyReg where dailyregId=@id and dailyregdt=@datetime"
               );            
        }

        public List<IDailyReg> ReadAll()
        {
            return mapperHelper.ReadAll(
            cmd => { },
            "select isin, minval, openingval,  maxval, closingval, dailydate from DailyReg"
            );
        }

        public bool Update(IDailyReg entity)
        {
            return mapperHelper.Update(entity,
                   (cmd, dailyReg) => UpdateParameters(cmd, dailyReg),
                    "update DailyReg set minval=@minval, openingval=@openingval, maxval=@maxval, closingval=@closingval where dailyregId=@id and dailyregdt0@datetime"
                   );
        }

        public bool Delete(IDailyReg entity)
        {
            return mapperHelper.Delete(entity,
                (cmd, dailyreg) => DeleteParameters(cmd, dailyreg),
                "delete from DailyReg where dailyregId=@id and dailyregdt=@datetime"
                );            
        }

        public List<IDailyReg> MapAll(IDataReader reader)
        {
            return mapperHelper.MapAll(reader);
        }
    }
}

