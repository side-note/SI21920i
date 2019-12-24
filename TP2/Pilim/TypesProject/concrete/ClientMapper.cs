using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using TypesProject.mapper;
using TypesProject.model;

namespace TypesProject.concrete
{
   public  class ClientMapper :  IClientMapper
    {
        MapperHelper<IClient, decimal, List<IClient>> mapperHelper;
        public ClientMapper(IContext ctx)
        {
            mapperHelper = new MapperHelper<IClient, decimal, List<IClient>>(ctx,this);
        }

        internal ICollection<IEmail> LoadEmails(IClient c)
        {
            List<IEmail> lst = new List<IEmail>();

            EmailMapper em = new EmailMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@nif", c.nif));
            using (IDataReader rd = mapperHelper.ExecuteReader("select code from Client_Email where nif=@nif", parameters))
            {
                while (rd.Read())
                    lst.Add(em.Read(rd.GetInt32(0)));

            }
            return lst;
        }
        internal ICollection<IPhone> LoadPhones(IClient c)
        {
            List<IPhone> lst = new List<IPhone>();

            PhoneMapper pm = new PhoneMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@nif", c.nif));
            using (IDataReader rd = mapperHelper.ExecuteReader("select phoneid from phoneclient where nif=@nif", parameters))
            {
                while (rd.Read())
                    lst.Add(pm.Read(rd.GetInt32(0)));
            }
            return lst;
        }
        internal IPortfolio LoadPortfolio(IClient c)
        {
       
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@nif", c.nif));
            using (IDataReader rd = mapperHelper.ExecuteReader("select name,nif from Client_Portfolio where nif=@nif", parameters))
            {
                if (rd.Read())
                {
                    Portfolio p = new Portfolio
                    {
                        name = rd.GetString(0),
                        totalval = rd.GetDecimal(1)
                    };
                    return new PortfolioProxy(p, mapperHelper.context);
                }
            }
            return null;

        }
     

      
        public IClient Create(IClient iclient) {
           
            using(TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                mapperHelper.Create(iclient,
                    (cmd, client) => InsertParameters(cmd,client),
                     "INSERT INTO Client (Name, ncc, nif) VALUES(@Name, @ncc, @id); select @id=nif"
                    );
                ts.Complete();
                return iclient;
            }
        }


        public bool Update(IClient iclient) {
             return mapperHelper.Update(iclient,
                    (cmd, client) => UpdateParameters(cmd, client),
                    "update Client set name=@name, ncc = @ncc where nif=@nif"
                    );



        }
      

    public List<IClient> ReadAll()
    {
        return mapperHelper.ReadAll(
            cmd=> { },
            "select nif, name, ncc from Client"
            );
    }

        public bool Delete(IClient iclient)
    {
            return mapperHelper.Delete(iclient,
                (cmd,client) => DeleteParameters(cmd,client),
                "delete from Client where nif=@nif"
                );
    }



    public List<IClient> MapAll(IDataReader reader)
    {
            return mapperHelper.MapAll(reader);
    }


        

        protected  void DeleteParameters(IDbCommand cmd, IClient c)
        {

            SqlParameter id = new SqlParameter("@nif", c.nif);
            cmd.Parameters.Add(id);
        }

        protected  void InsertParameters(IDbCommand cmd, IClient c)
        {
            SqlParameter name = new SqlParameter("@Name", c.name);
            SqlParameter id = new SqlParameter("@nif", c.nif);
            SqlParameter ncc = new SqlParameter("@ncc", c.ncc);
            id.Direction = ParameterDirection.InputOutput;

            cmd.Parameters.Add(id);
            cmd.Parameters.Add(name);
            cmd.Parameters.Add(ncc);

        }


        protected void SelectParameters(IDbCommand cmd, decimal k)
        {
            SqlParameter id = new SqlParameter("@nif", k);
            cmd.Parameters.Add(id);
        }


        protected void UpdateParameters(IDbCommand cmd, IClient c)
        {
            InsertParameters(cmd, c);
        }

        public IClient Map(IDataRecord record)
        {
            Client c = new Client();
            c.nif = record.GetDecimal(0);
            c.name = record.GetString(1);
            c.ncc = record.GetDecimal(2);
            return new ClientProxy(c, mapperHelper.context);
        }

        public IClient Read(decimal id)
        {
            return mapperHelper.Read(id,
                (cmd, i) => SelectParameters(cmd, i),
                "select nif, name, ncc from Client where nif=@nif"
                );
        }
    }
}
    

     
