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
    class ExttripleMapper: AbstractMapper<Exttriple, KeyValuePair<int, DateTime>, List<Exttriple>>, IExttripleMapper
    {
        public ExttripleMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText
        {
            get
            {
                return "delete from Exttriple where exttripleId=@id and exttripleDatetime = @datetime";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO EXttriple(exttripleId, exttripleDatetime, exttripleValue) values(@id, @datetime, @value); select @id=exttripleId, @datetime= exttripleDatetime from Exttriple;";
            }
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "select exttripleId, exttripleDatetime, exttripleValue from Exttriple";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} where exttripleId=@id and exttripleDatetime = @datetime", SelectAllCommandText); ;
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update Exttriple set exttripleValue=@value where exttripleId=@id and exttripleDatetime = @datetime";
            }
        }

        protected override void DeleteParameters(IDbCommand cmd, Exttriple e)
        {

            SqlParameter id = new SqlParameter("@id", e.id);
            SqlParameter datetime = new SqlParameter("@datetime", e.datetime);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(datetime);
        }

        protected override void InsertParameters(IDbCommand cmd, Exttriple e)
        {
            SqlParameter id = new SqlParameter("@id", e.id);
            SqlParameter datetime = new SqlParameter("@datetime", e.datetime);
            SqlParameter value = new SqlParameter("value",e.value);
            id.Direction = ParameterDirection.InputOutput;
            datetime.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(datetime);
            cmd.Parameters.Add(value);
        }


        protected override void SelectParameters(IDbCommand cmd, KeyValuePair< int, DateTime> p)
        {
            SqlParameter id = new SqlParameter("@id",p.Key);
            SqlParameter datetime = new SqlParameter("@datetime",p.Value);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(datetime);
        }

        protected override Exttriple UpdateEntityID(IDbCommand cmd, Exttriple e)
        {
            var paramid = cmd.Parameters["@id"] as SqlParameter;
            var paramdt = cmd.Parameters["@datetime"] as SqlParameter;
            e.id = int.Parse(paramid.Value.ToString());
            e.datetime = DateTime.Parse(paramdt.Value.ToString());
            return e;
        }

        protected override void UpdateParameters(IDbCommand cmd, Exttriple e)
        {
            InsertParameters(cmd, e);
        }

        protected override Exttriple Map(IDataRecord record)
        {
            Exttriple e= new Exttriple();
            e.id = record.GetInt32(0);
            e.datetime = record.GetDateTime(1);
            e.value = record.GetDouble(2);
            return e;
        }
    }
}

