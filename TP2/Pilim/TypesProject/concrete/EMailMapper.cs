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
    class EmailMapper :AbstractMapper<Email, int, List<Email>>, IEmailMapper
    {
        public EmailMapper(IContext ctx) : base(ctx)
        {
        }
        internal Client LoadClient(Email e)
        {
           ClientMapper cm = new ClientMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", e.code));

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

        protected override string DeleteCommandText
        {
            get
            {
                return "delete from Email where emailId=@id";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Email (code, addr, description) VALUES(@id, @addr, @desc); select @id=code";
            }
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "select code, addr, description from Email";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} where emailId=@id", SelectAllCommandText); ;
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update Email set addr=@addr, description=@desc where emailId=@id";
            }
        }

        protected override void DeleteParameters(IDbCommand cmd, Email e)
        {

            SqlParameter id = new SqlParameter("@id", e.code);
            cmd.Parameters.Add(id);
        }

        protected override void InsertParameters(IDbCommand cmd, Email e)
        {
            SqlParameter addr = new SqlParameter("@addr", e.addr);
            SqlParameter id = new SqlParameter("@id", e.code);
            SqlParameter desc = new SqlParameter("@desc", e.description);
            id.Direction = ParameterDirection.InputOutput;

            cmd.Parameters.Add(id);
            cmd.Parameters.Add(addr);
            cmd.Parameters.Add(desc);
        }


        protected override void SelectParameters(IDbCommand cmd, int? k)
        {
            SqlParameter id = new SqlParameter("@id", k);
            cmd.Parameters.Add(id);
        }

        protected override Email UpdateEntityID(IDbCommand cmd, Email e)
        {
            var param = cmd.Parameters["@id"] as SqlParameter;
            e.code = int.Parse(param.Value.ToString());
            return e;
        }

        protected override void UpdateParameters(IDbCommand cmd, Email e)
        {
            InsertParameters(cmd, e);
        }

        protected override Email Map(IDataRecord record)
        {
            Email e = new Email();
            e.code = record.GetInt32(0);
            e.addr = record.GetString(1);
            e.description = record.GetString(2);
            return new EmailProxy(e,context);
        }
        public override Email Create(Email email)
        {

            return new EmailProxy(email, context);

        }


        public override Email Update(Email email)
        {
            return new EmailProxy(base.Update(email), context);
        }
    }
}

