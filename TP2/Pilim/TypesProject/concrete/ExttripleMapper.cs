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

    public class ExttripleMapper: IExttripleMapper
    {
        MapperHelper<IExttriple, KeyValuePair<string, DateTime>, List<IExttriple>> mapperHelper;
        public ExttripleMapper(IContext ctx) 
        {
            mapperHelper = new MapperHelper<IExttriple, KeyValuePair<string, DateTime>, List<IExttriple>>(ctx, this);
        }
        protected void DeleteParameters(IDbCommand cmd, IExttriple e)
        {

            SqlParameter id = new SqlParameter("@id", e.id);
            SqlParameter datetime = new SqlParameter("@datetime", e.datetime);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(datetime);
        }

        protected void InsertParameters(IDbCommand cmd, IExttriple e)
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


        protected void SelectParameters(IDbCommand cmd, KeyValuePair< string, DateTime> p)
        {
            SqlParameter id = new SqlParameter("@id",p.Key);
            SqlParameter datetime = new SqlParameter("@datetime",p.Value);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(datetime);
        }

        protected void UpdateParameters(IDbCommand cmd, IExttriple e)
        {
            InsertParameters(cmd, e);
        }

        public  IExttriple Map(IDataRecord record)
        {
            Exttriple e = new Exttriple();
            e.value = record.IsDBNull(0) ? default : record.GetDecimal(0);
            e.datetime = record.IsDBNull(1) ? default : record.GetDateTime(1);
            e.id = record.IsDBNull(2) ? default : record.GetString(2);
            return e;
        }

        public IExttriple Create(IExttriple entity)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                mapperHelper.Create(entity,
                    (cmd, exttriple) => InsertParameters(cmd, exttriple),
                     "INSERT INTO EXttriple(id, datetime, value) values(@id, @datetime, @value); select @id=id, @datetime=datetime from Exttriple;"
                    );
                ts.Complete();
                return entity;
            }
        }

        public IExttriple Read(KeyValuePair<string, DateTime> id)
        {
            return mapperHelper.Read(id,
                (cmd,i) => SelectParameters(cmd,i),
                "select id,datetime, value from Exttriple where id=@id and datetime=@datetime"
                );
        }

        public List<IExttriple> ReadAll()
        {
            return mapperHelper.ReadAll(
                cmd => { },
                "select id, datetime, value from Exttriple"
                );
        }

        public bool Update(IExttriple entity)
        {
            return mapperHelper.Update(entity,
                (cmd, exttriple) => UpdateParameters(cmd, exttriple),
                "update Exttriple set value=@value where id=@id and datetime=@datetime"
               );
        }

        public bool Delete(IExttriple entity)
        {
            return mapperHelper.Delete(entity,
                (cmd, exttriple) => DeleteParameters(cmd, exttriple),
                 "delete from Exttriple where id=@id and datetime=@datetime"
                );
        }

      
        public List<IExttriple> MapAll(IDataReader reader)
        {
            return mapperHelper.MapAll(reader);
        }
    }
}

