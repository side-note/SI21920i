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
    class ClientMapper : AbstractMapper<Client, int?, List<Client>>, IClientMapper
    {
        public ClientMapper(IContext ctx) : base(ctx)
        {
        }

        internal ICollection<Email> LoadEmails(Client c)
        {
            List<Email> lst = new List<Email>();

            EmailMapper em = new EmailMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", c.nif));
            using (IDataReader rd = ExecuteReader("select emailid from clientemail where clientId=@id", parameters))
            {
                while (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    lst.Add(em.Read(key));
                }
            }
            return lst;
        }
        internal ICollection<Phone> LoadPhones(Client c)
        {
            List<Phone> lst = new List<Phone>();

            PhoneMapper pm = new PhoneMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", c.nif));
            using (IDataReader rd = ExecuteReader("select phoneid from phoneclient where clientId=@id", parameters))
            {
                while (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    lst.Add(pm.Read(key));
                }
            }
            return lst;
        }
        internal Portfolio LoadPortfolio(Client c)
        {
            PortfolioMapper pm = new PortfolioMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", c.nif));
            using (IDataReader rd = ExecuteReader("select portfolio from client where clientId=@id", parameters))
            {
                if (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    return pm.Read(key);
                }
            }
            return null;

        }

        public override Client Create(Client client)
        {
           
                return new ClientProxy(client,context) ;

        }


        public override Client Update(Client client)
        {
            return new ClientProxy(base.Update(client), context);
        }
        protected override string DeleteCommandText
        {
            get
            {
                return "delete from Client where clientId=@id";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Client (Name, ncc, nif) VALUES(@Name, @ncc, @id); select @id=   nif";
            }
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "select clientId, name, ncc from Client";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} where clientId=@id", SelectAllCommandText); ;
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update Client set name=@name, ncc = @ncc where clientId=@id";
            }
        }

        protected override void DeleteParameters(IDbCommand cmd, Client c)
        {

            SqlParameter id = new SqlParameter("@id", c.nif);
            cmd.Parameters.Add(id);
        }

        protected override void InsertParameters(IDbCommand cmd, Client c)
        {
            SqlParameter name = new SqlParameter("@Name", c.name);
            SqlParameter id = new SqlParameter("@id", c.nif);
            SqlParameter ncc = new SqlParameter("@ncc", c.ncc);
            id.Direction = ParameterDirection.InputOutput;

            cmd.Parameters.Add(id);
            cmd.Parameters.Add(name);
            cmd.Parameters.Add(ncc);

        }


        protected override void SelectParameters(IDbCommand cmd, int? k)
        {
            SqlParameter id = new SqlParameter("@id", k);
            cmd.Parameters.Add(id);
        }

        protected override Client UpdateEntityID(IDbCommand cmd, Client c)
        {
            var param = cmd.Parameters["@id"] as SqlParameter;
            c.nif = int.Parse(param.Value.ToString());
            return c;
        }

        protected override void UpdateParameters(IDbCommand cmd, Client c)
        {
            InsertParameters(cmd, c);
        }

        protected override Client Map(IDataRecord record)
        {
            Client c = new Client();
            c.nif = record.GetInt32(0);
            c.ncc = record.GetInt32(1);
            c.name = record.GetString(2);
            return new ClientProxy(c,context);
        }
    }
}
    

     
