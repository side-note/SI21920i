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
    class MarketMapper: AbstractMapper<Market, int, List<Market>>, IMarketMapper
    {
        public MarketMapper(IContext ctx) : base(ctx)
        {
        }
        internal ICollection<DailyMarket> LoadDailyMarkets(Market m)
        {
            List<DailyMarket> lst = new List<DailyMarket>();

            DailyMarketMapper dmm = new DailyMarketMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", m.Code));
            using (IDataReader rd = ExecuteReader("select dailymarketid, dailymarketdt from marketdailymarket where marketId=@id", parameters))
            {
                while (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    lst.Add(dmm.Read(key));
                }
            }
            return lst;
        }
        internal ICollection<Instrument> LoadInstruments(Market m)
        {
            List<Instrument> lst = new List<Instrument>();

            InstrumentMapper im = new InstrumentMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", m.Code));
            using (IDataReader rd = ExecuteReader("select instrumentid from marketinstrument where marketId=@id", parameters))
            {
                while (rd.Read())
                {
                    string key = rd.GetString(0);
                    lst.Add(im.Read(key));
                }
            }
            return lst;
        }
        protected override string DeleteCommandText
        {
            get
            {
                return "delete from Market where mrktId=@id";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Market (code, description, name) VALUES(@id,@desc,@name); select @id=code";
            }
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "select code, description,name from Market";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} where mrktId=@id", SelectAllCommandText); ;
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update Market set description=@desc, name=@name where mrktId=@id";
            }
        }

        protected override void DeleteParameters(IDbCommand cmd, Market m)
        {

            SqlParameter id = new SqlParameter("@id", m.Code);
            cmd.Parameters.Add(id);
        }

        protected override void InsertParameters(IDbCommand cmd, Market m)
        {
            SqlParameter n = new SqlParameter("@Name", m.Name);
            SqlParameter id = new SqlParameter("@id", m.Code);
            SqlParameter d = new SqlParameter("@desc", m.Description);
            id.Direction = ParameterDirection.InputOutput;

            cmd.Parameters.Add(n);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(d);
        }


        protected override void SelectParameters(IDbCommand cmd, int k)
        {
            SqlParameter id = new SqlParameter("@id", k);
            cmd.Parameters.Add(id);
        }

        protected override Market UpdateEntityID(IDbCommand cmd, Market m)
        {
            var param = cmd.Parameters["@id"] as SqlParameter;
            m.Code = int.Parse(param.Value.ToString());
            return m;
        }

        protected override void UpdateParameters(IDbCommand cmd, Market m)
        {
            InsertParameters(cmd, m);
        }

        protected override Market Map(IDataRecord record)
        {
            Market m = new Market();
            m.Code = record.GetInt32(0);
            m.Name = record.GetString(1);
            m.Description = record.GetString(2);
            return new MarketProxy(m, context);
        }
        public override Market Create(Market market)
        {

            return new MarketProxy(market, context);

        }


        public override Market Update(Market market)
        {
            return new MarketProxy(base.Update(market), context);
        }
    }
}

