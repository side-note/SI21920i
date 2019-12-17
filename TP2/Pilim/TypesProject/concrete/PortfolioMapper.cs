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
    class PortfolioMapper: AbstractMapper<Portfolio, String, List<Portfolio>>, IPortfolioMapper
    {
        public PortfolioMapper(IContext ctx) : base(ctx)
        {
        }

        internal Client LoadClient(Portfolio p)
        {
            ClientMapper cm = new ClientMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", p.name));

            using (IDataReader rd = ExecuteReader("select client from email where emailId=@id", parameters))
            {
                if (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    return cm.Read(key);
                }
            }
            return null;

        }

        internal ICollection<Instrument> LoadPortfolios(Portfolio p)
        {
            List<Instrument> lst = new List<Instrument>();

            InstrumentMapper im = new InstrumentMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", p.name));
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
                return "delete from Portfolio where potfolioId=@id";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO portfolio (name, totalval) VALUES(@id, @total); select @id=name";
            }
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "select name,totalval from Portfolio";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} where portfolioId=@id", SelectAllCommandText); ;
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update Portfolio set totalval=@total where portfolioId=@id";
            }
        }

        protected override void DeleteParameters(IDbCommand cmd, Portfolio p)
        {

            SqlParameter id = new SqlParameter("@id",p.name);
            cmd.Parameters.Add(id);
        }

        protected override void InsertParameters(IDbCommand cmd, Portfolio p)
        {
            SqlParameter id = new SqlParameter("@id", p.name);
            SqlParameter tv = new SqlParameter("@total", p.totalval);
            id.Direction = ParameterDirection.InputOutput;

            cmd.Parameters.Add(id);
            cmd.Parameters.Add(tv);
        }


        protected override void SelectParameters(IDbCommand cmd, String k)
        {
            SqlParameter id = new SqlParameter("@id", k);
            cmd.Parameters.Add(id);
        }

        protected override Portfolio UpdateEntityID(IDbCommand cmd,Portfolio p)
        {
            var param = cmd.Parameters["@id"] as SqlParameter;
            p.name= string.Parse(param.Value.ToString());
            return p;
        }

        protected override void UpdateParameters(IDbCommand cmd, Portfolio p)
        {
            InsertParameters(cmd, p);
        }

        protected override Portfolio Map(IDataRecord record)
        {
            Portfolio p = new Portfolio();
            p.totalval = record.GetDouble(0);
            p.name = record.GetString(1);
            return new PortfolioProxy( p, context);

        }

        public override Portfolio Create(Portfolio portfolio)
        {

            return new PortfolioProxy(portfolio, context);

        }


        public override Portfolio Update(Portfolio portfolio)
        {
            return new PortfolioProxy(base.Update(portfolio), context);
        }
    }
}

