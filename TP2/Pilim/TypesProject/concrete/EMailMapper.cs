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
    class EmailMapper : IEmailMapper
    {
        MapperHelper<IEmail, int, List<IEmail>> mapperHelper;
        public EmailMapper(IContext ctx)
        {
            mapperHelper = new MapperHelper<IEmail, int, List<IEmail>>(ctx, this);
        }
        internal IClient LoadClient(Email e)
        {
            ClientMapper cm = new ClientMapper(mapperHelper.context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", e.code));

            using (IDataReader rd = mapperHelper.ExecuteReader("select client from email where emailId=@id", parameters))
            {
                if (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    return cm.Read(key);
                }
            }
            return null;

        }

        protected void DeleteParameters(IDbCommand cmd, IEmail e)
        {

            SqlParameter id = new SqlParameter("@id", e.code);
            cmd.Parameters.Add(id);
        }

        protected void InsertParameters(IDbCommand cmd, IEmail e)
        {
            SqlParameter addr = new SqlParameter("@addr", e.addr);
            SqlParameter id = new SqlParameter("@id", e.code);
            SqlParameter desc = new SqlParameter("@desc", e.description);
            id.Direction = ParameterDirection.InputOutput;

            cmd.Parameters.Add(id);
            cmd.Parameters.Add(addr);
            cmd.Parameters.Add(desc);
        }


        protected void SelectParameters(IDbCommand cmd, int? k)
        {
            SqlParameter id = new SqlParameter("@id", k);
            cmd.Parameters.Add(id);
        }

        protected void UpdateParameters(IDbCommand cmd, IEmail e)
        {
            InsertParameters(cmd, e);
        }

        public IEmail Map(IDataRecord record)
        {
            Email e = new Email();
            e.code = record.GetInt32(0);
            e.addr = record.GetString(1);
            e.description = record.GetString(2);
            return new EmailProxy(e, mapperHelper.context);
        }
        public IEmail Create(IEmail entity)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                mapperHelper.Create(entity,
                    (cmd, email) => InsertParameters(cmd, email),
                     "INSERT INTO Email (code, addr, description) VALUES(@id, @addr, @desc); select @id=code"
                    );
                ts.Complete();
                return entity;
            }
        }

        public List<IEmail> ReadAll()
        {
            return mapperHelper.ReadAll(
            cmd => { },
            "select code, addr, description from Email"
            );
        }

        public IEmail Read(int id)
        {
            return mapperHelper.Read(id,
              (cmd, i) => SelectParameters(cmd, i),
             "select code, addr, description from Email where emailId=@id"
              );
        }

        public bool Update(IEmail entity)
        {
            return mapperHelper.Update(entity,
                (cmd, email) => UpdateParameters(cmd, email),
                "update Email set addr=@addr, description=@desc where emailId=@id"
                );
        }

        public bool Delete(IEmail entity)
        {
            return mapperHelper.Delete(entity,
                (cmd, dailyreg) => DeleteParameters(cmd, dailyreg),
                "delete from Email where emailId = @id"
                );
        }

        public List<IEmail> MapAll(IDataReader reader)
        {
            return mapperHelper.MapAll(reader);
        }
    }
}

