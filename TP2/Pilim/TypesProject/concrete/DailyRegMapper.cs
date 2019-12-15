using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.mapper;
using TypesProject.model;

namespace TypesProject.concrete
{
    class DailyRegMapper : AbstractMapper<DailyReg, KeyValuePair<Instrument?, DateTime?>, List<DailyReg>>, IDailyRegMapper
    {
        public DailyRegMapper(IContext ctx) : base(ctx)
        {
        }
        internal Instrument LoadMarket(DailyReg dr)
        {
            InstrumentMapper im = new InstrumentMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id",dr.isin));
            parameters.Add(new SqlParameter("@datetime", dr.dailydate));

            using (IDataReader rd = ExecuteReader("select instrument from dailyreg where dailyregId=@id and dailyregdt=@datetime ", parameters))
            {
                if (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    return im.Read(key);
                }
            }
            return null;

        }


        protected override string DeleteCommandText
        {
            get
            {
                return "delete from DailyReg where dailyregId=@id and dailyregdt=@datetime";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO DailyReg (isin, minval, openingval,  maxval, closingval, dailydate) VALUES(@id, @minval, @openingval, @maxval, @closingval, @datetime); select @id=isin, @datetime=dailydate";
            }
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "select isin, minval, openingval,  maxval, closingval, dailydate from DailyReg";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} where dailyregId=@id and dailyregdt=@datetime", SelectAllCommandText); ;
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update DailyReg set minval=@minval, openingval=@openingval, maxval=@maxval, closingval=@closingval where dailyregId=@id and dailyregdt0@datetime";
            }
        }

        protected override void DeleteParameters(IDbCommand cmd, DailyReg dr)
        {

            SqlParameter id = new SqlParameter("@id",dr.isin);
            SqlParameter dt = new SqlParameter("@datetime",dr.dailydate);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(dt);
        }

        protected override void InsertParameters(IDbCommand cmd, DailyReg dr)
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
        protected override void SelectParameters(IDbCommand command, KeyValuePair<Instrument, DateTime?> k)
        {
            SqlParameter id = new SqlParameter("@id", k.Key);
            SqlParameter dt = new SqlParameter("@datetime", k.Value);
            command.Parameters.Add(id);
            command.Parameters.Add(dt);
        }

        

        protected override DailyReg UpdateEntityID(IDbCommand cmd, DailyReg dr)
        {
            var paramid = cmd.Parameters["@id"] as SqlParameter;
            var paramdt = cmd.Parameters["@datetime"] as SqlParameter;
          // dr.isin = int.Parse(paramid.Value.ToString());
            dr.dailydate= DateTime.Parse(paramdt.Value.ToString());
            return dr;
        }

        protected override void UpdateParameters(IDbCommand cmd, DailyReg dr)
        {
            InsertParameters(cmd, dr);
        }

        protected override DailyReg Map(IDataRecord record)
        {
            DailyReg dr = new DailyReg();
            dr.maxval = record.GetDouble(0);
            dr.minval = record.GetDouble(1);
            dr.openingval = record.GetDouble(2);
            dr.closingval = record.GetDouble(3);
            dr.dailydate = record.GetDateTime(4);

            return new DailyRegProxy(dr, context,record.GetInt32(5));
        }
    }
}

