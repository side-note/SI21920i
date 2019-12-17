using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using TypesProject.mapper;
using TypesProject.model;

namespace TypesProject.concrete
{
    class ClientMapper :  IClientMapper
    {
        MapperHelper<IClient, int, List<IClient>> mapperHelper;
        public ClientMapper(IContext ctx)
        {
            mapperHelper = new MapperHelper<IClient, int, List<IClient>>(ctx,this);
        }

        internal ICollection<Email> LoadEmails(Client c)
        {
            List<Email> lst = new List<Email>();

            EmailMapper em = new EmailMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", c.nif));
            using (IDataReader rd = mapperHelper.ExecuteReader("select emailid from clientemail where clientId=@id", parameters))
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

            PhoneMapper pm = new PhoneMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", c.nif));
            using (IDataReader rd = mapperHelper.ExecuteReader("select phoneid from phoneclient where clientId=@id", parameters))
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
            PortfolioMapper pm = new PortfolioMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", c.nif));
            using (IDataReader rd = mapperHelper.ExecuteReader("select portfolio from client where clientId=@id", parameters))
            {
                if (rd.Read())
                {
                    String key = rd.GetString(0);
                    return pm.Read(key);
                }
            }
            return null;

        }

        public IClient Create(IClient iclient) {
           
            using(TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                mapperHelper.Create(iclient,
                    (cmd, client) => InsertParameters(cmd,client),
                     "INSERT INTO Client (Name, ncc, nif) VALUES(@Name, @ncc, @id); select @id=   nif"
                    );
                ts.Complete();
                return iclient;
            }
        }


        public bool Update(IClient iclient) {
             return mapperHelper.Update(iclient,
                    (cmd, client) => UpdateParameters(cmd, client),
                    "update Client set name=@name, ncc = @ncc where clientId=@id"
                    );



        }
        public IClient Read(int id)
        {
            return mapperHelper.Read(id,
                (cmd,i)=>SelectParameters(cmd,i),
                "select clientId, name, ncc from Client where clientId=@id"
                );
    }

    public List<IClient> ReadAll()
    {
        return mapperHelper.ReadAll(
            cmd=> { },
            "select clientId, name, ncc from Client"
            );
    }

        public bool Delete(IClient iclient)
    {
            return mapperHelper.Delete(iclient,
                (cmd,client) => DeleteParameters(cmd,client),
                "delete from Client where clientId=@id"
                );
    }



    public List<IClient> MapAll(IDataReader reader)
    {
            return mapperHelper.MapAll(reader);
    }


        

        protected  void DeleteParameters(IDbCommand cmd, IClient c)
        {

            SqlParameter id = new SqlParameter("@id", c.nif);
            cmd.Parameters.Add(id);
        }

        protected  void InsertParameters(IDbCommand cmd, IClient c)
        {
            SqlParameter name = new SqlParameter("@Name", c.name);
            SqlParameter id = new SqlParameter("@id", c.nif);
            SqlParameter ncc = new SqlParameter("@ncc", c.ncc);
            id.Direction = ParameterDirection.InputOutput;

            cmd.Parameters.Add(id);
            cmd.Parameters.Add(name);
            cmd.Parameters.Add(ncc);

        }


        protected void SelectParameters(IDbCommand cmd, int k)
        {
            SqlParameter id = new SqlParameter("@id", k);
            cmd.Parameters.Add(id);
        }


        protected void UpdateParameters(IDbCommand cmd, IClient c)
        {
            InsertParameters(cmd, c);
        }

        public IClient Map(IDataRecord record)
        {
            Client c = new Client();
            c.nif = record.GetInt32(0);
            c.ncc = record.GetInt32(1);
            c.name = record.GetString(2);
            return new ClientProxy(c, mapperHelper.context);
        }

    }
}
    

     
