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
    class DailyMarketMapper: AbstractMapper<DailyMarket, Market, List<DailyMarket>>, IDailyMarketMapper
    {
        public DailyMarketMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText
        {
            get
            {
                return "delete from DailyMarket where dailyMarketId=@id";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO DailyMarket (Name) VALUES(@Name); select @id=scope_identity()";
            }
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "select clientId,name from DailyMarket";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} where dailyMarketId=@id", SelectAllCommandText); ;
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update DailyMarket set name=@name where clientId=@id";
            }
        }

        protected override void DeleteParameters(IDbCommand cmd, DailyMarket dm)
        {

            SqlParameter p1 = new SqlParameter("@id", dm.Code);
            cmd.Parameters.Add(p1);
        }

        protected override void InsertParameters(IDbCommand cmd, DailyMarket dm)
        {
            SqlParameter p = new SqlParameter("@IdxMrkt", SqlDbType.Money);
            SqlParameter p1 = new SqlParameter("@id", dm.Code);
            SqlParameter p2 = new SqlParameter("@date", SqlDbType.DateTime);
            SqlParameter p3 = new SqlParameter("@dailyVar", SqlDbType.Money);
            SqlParameter p4 = new SqlParameter("@IdxOpeningVal", SqlDbType.Money);
            p1.Direction = ParameterDirection.InputOutput;

            if (c.nif != null)
                p1.Value = c.nif;
            else
                p1.Value = DBNull.Value;

            cmd.Parameters.Add(p);
            cmd.Parameters.Add(p1);
        }


        protected override void SelectParameters(IDbCommand cmd, Market k)
        {
            SqlParameter p1 = new SqlParameter("@id", k);
            cmd.Parameters.Add(p1);
        }

        protected override DailyMarket UpdateEntityID(IDbCommand cmd, DailyMarket dm)
        {
            var param = cmd.Parameters["@id"] as SqlParameter;
            c.nif = int.Parse(param.Value.ToString());
            return c;
        }

        protected override void UpdateParameters(IDbCommand cmd, DailyMarket dm)
        {
            InsertParameters(cmd, dm);
        }

        protected override DailyMarket Map(IDataRecord record)
        {
            DailyMarket dm = new DailyMarket();
            c.nif = record.GetInt32(0);
            c.name = record.GetString(1);
            return dm;
        }
    }
}
}
