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
   public  class DailyRegMapper : IDailyRegMapper
    {
        MapperHelper<IDailyReg, KeyValuePair<string, DateTime>, List<IDailyReg>> mapperHelper;
        public DailyRegMapper(IContext ctx)
        {
            mapperHelper = new MapperHelper<IDailyReg, KeyValuePair<string, DateTime>, List<IDailyReg>>(ctx, this);
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
            dr.isin = record.IsDBNull(0) ? default : record.GetString(0);
            dr.minval = record.IsDBNull(1) ? default : record.GetDecimal(1);
            dr.openingval = record.IsDBNull(2) ? default : record.GetDecimal(2);
            dr.maxval = record.IsDBNull(3) ? default : record.GetDecimal(3);
            dr.closingval = record.IsDBNull(4) ? default : record.GetDecimal(4);
            dr.dailydate = record.IsDBNull(5) ? default : record.GetDateTime(5);

            return dr;
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
               "select isin, minval, openingval,  maxval, closingval, dailydate from DailyReg where isin=@id and dailydate=@datetime"
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
                    "update DailyReg set minval=@minval, openingval=@openingval, maxval=@maxval, closingval=@closingval where isin=@id and dailydate=@datetime"
                   );
        }

        public bool Delete(IDailyReg entity)
        {
            return mapperHelper.Delete(entity,
                (cmd, dailyreg) => DeleteParameters(cmd, dailyreg),
                "delete from DailyReg where isin=@id and dailydate=@datetime"
                );            
        }

        public List<IDailyReg> MapAll(IDataReader reader)
        {
            return mapperHelper.MapAll(reader);
        }
    }
}

