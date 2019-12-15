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
    class DailyMarketMapper: AbstractMapper<DailyMarket,KeyValuePair< Market, DateTime>, List<DailyMarket>>, IDailyMarketMapper
    {
        public DailyMarketMapper(IContext ctx) : base(ctx)
        {
        }

        internal Market LoadMarket(DailyMarket dm)
        {
           MarketMapper mm = new MarketMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", dm.Code));
            parameters.Add(new SqlParameter("@datetime", dm.Date));

            using (IDataReader rd = ExecuteReader("select market from dailymarket where dailymarketId=@id and dailymarketdt=@datetime ", parameters))
            {
                if (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    return mm.Read(key);
                }
            }
            return null;

        }

        protected override string DeleteCommandText
        {
            get
            {
                return "delete from DailyMarket where dailyMarketId=@id and dailymarketdt = @datetime";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO DailyMarket (idxMrkt,dailyvar,idxopeningval, code, date) VALUES(@idxmrkt, @dailyvar, @idxopv, @id, @datetime); select @id=code, @datetime=date";
            }
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "select idxMrkt,dailyvar,idxopeningval, code, date from DailyMarket";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} where dailyMarketId=@id and dailymarketdt=@datetime", SelectAllCommandText); ;
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update DailyMarket set idxMrkt=@idxmrkt, dailyvar=@dailyvar, idxopeningval=@idxopv where dailyMarketId=@id and dailymarketdt=@datetime";
            }
        }

        protected override void DeleteParameters(IDbCommand cmd, DailyMarket dm)
        {

            SqlParameter id = new SqlParameter("@id", dm.Code);
            SqlParameter datetime = new SqlParameter("@datetime", dm.Date);

            cmd.Parameters.Add(id);
            cmd.Parameters.Add(datetime);
        }

        protected override void InsertParameters(IDbCommand cmd, DailyMarket dm)
        {
            SqlParameter idm = new SqlParameter("@IdxMrkt", dm.IdxMrkt);
            SqlParameter id = new SqlParameter("@id", dm.Code);
            SqlParameter dt = new SqlParameter("@datetime", dm.Date);
            SqlParameter dv = new SqlParameter("@dailyVar", dm.DailyVar);
            SqlParameter iov = new SqlParameter("@IdxOpeningVal", dm.IdxOpeningVal);
            id.Direction = ParameterDirection.InputOutput;
            dt.Direction = ParameterDirection.InputOutput;

            cmd.Parameters.Add(idm);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(dt);
            cmd.Parameters.Add(dv);
            cmd.Parameters.Add(iov);
        }


        protected override void SelectParameters(IDbCommand cmd, KeyValuePair<Market, DateTime> p )
        {
            SqlParameter id = new SqlParameter("@id", p.Key);
            SqlParameter dt = new SqlParameter("@datetime", p.Value);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(dt);
        }

        protected override DailyMarket UpdateEntityID(IDbCommand cmd, DailyMarket dm)
        {
            var paramid = cmd.Parameters["@id"] as SqlParameter;
            var paramdatetime = cmd.Parameters["@datetime"] as SqlParameter;
          // dm.Code =.Parse(paramid.Value.ToString());
           dm.Date = DateTime.Parse(paramdatetime.Value.ToString());
            return dm;
        }

        protected override void UpdateParameters(IDbCommand cmd, DailyMarket dm)
        {
            InsertParameters(cmd, dm);
        }

        protected override DailyMarket Map(IDataRecord record)
        {
            DailyMarket dm = new DailyMarket();
            dm.DailyVar = record.GetDouble(0);
            dm.IdxMrkt = record.GetDouble(1);
            dm.IdxOpeningVal = record.GetDouble(2);
            dm.Date = record.GetDateTime(3);
            return new DailyMarketProxy(dm,context, record.GetInt32(4));
        }
      

}
}

