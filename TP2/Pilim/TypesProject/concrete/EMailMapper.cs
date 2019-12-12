using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.mapper;
using TypesProject.model;

namespace TypesProject.concrete
{
    class EmailMapper
    {
        public ClientMapper(IContext ctx) : base(ctx)
        {
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
                return "INSERT INTO Client (Name) VALUES(@Name); select @id=scope_identity()";
            }
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "select clientId,name from Client";
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
                return "update Client set name=@name where clientId=@id";
            }
        }

        protected override void DeleteParameters(IDbCommand cmd, Client c)
        {

            SqlParameter p1 = new SqlParameter("@id", c.nif);
            cmd.Parameters.Add(p1);
        }

        protected override void InsertParameters(IDbCommand cmd, Client c)
        {
            SqlParameter p = new SqlParameter("@Name", c.name);
            SqlParameter p1 = new SqlParameter("@id", SqlDbType.Int);
            p1.Direction = ParameterDirection.InputOutput;

            if (c.nif != null)
                p1.Value = c.nif;
            else
                p1.Value = DBNull.Value;

            cmd.Parameters.Add(p);
            cmd.Parameters.Add(p1);
        }


        protected override void SelectParameters(IDbCommand cmd, int? k)
        {
            SqlParameter p1 = new SqlParameter("@id", k);
            cmd.Parameters.Add(p1);
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
            c.name = record.GetString(1);
            return c;
        }
    }
}
}
