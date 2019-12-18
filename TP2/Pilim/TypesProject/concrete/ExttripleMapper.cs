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

    class ExttripleMapper: IExttripleMapper
    {
        MapperHelper<IExtTriple, KeyValuePair<int, DateTime>, List<IExtTriple>> mapperHelper;
        public ExttripleMapper(IContext ctx) 
        {
            mapperHelper = new MapperHelper<IExtTriple, KeyValuePair<int, DateTime>, List<IExtTriple>>(ctx, this);
        }
        protected void DeleteParameters(IDbCommand cmd, IExtTriple e)
        {

            SqlParameter id = new SqlParameter("@id", e.id);
            SqlParameter datetime = new SqlParameter("@datetime", e.datetime);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(datetime);
        }

        protected void InsertParameters(IDbCommand cmd, IExtTriple e)
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


        protected void SelectParameters(IDbCommand cmd, KeyValuePair< int, DateTime> p)
        {
            SqlParameter id = new SqlParameter("@id",p.Key);
            SqlParameter datetime = new SqlParameter("@datetime",p.Value);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(datetime);
        }

        protected void UpdateParameters(IDbCommand cmd, IExtTriple e)
        {
            InsertParameters(cmd, e);
        }

        public  IExtTriple Map(IDataRecord record)
        {
            Exttriple e= new Exttriple();
            e.id = record.GetInt32(0);
            e.datetime = record.GetDateTime(1);
            e.value = record.GetDouble(2);
            return e;
        }

        public IExtTriple Create(IExtTriple entity)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                mapperHelper.Create(entity,
                    (cmd, exttriple) => InsertParameters(cmd, exttriple),
                     "INSERT INTO EXttriple(exttripleId, exttripleDatetime, exttripleValue) values(@id, @datetime, @value); select @id=exttripleId, @datetime= exttripleDatetime from Exttriple;"
                    );
                ts.Complete();
                return entity;
            }
        }

        public IExtTriple Read(KeyValuePair<int, DateTime> id)
        {
            return mapperHelper.Read(id,
                (cmd,i) => SelectParameters(cmd,i),
                "select exttripleId, exttripleDatetime, exttripleValue from Exttriple where exttripleId=@id and exttripleDatetime = @datetime"
                );
        }

        public List<IExtTriple> ReadAll()
        {
            return mapperHelper.ReadAll(
                cmd => { },
                "select exttripleId, exttripleDatetime, exttripleValue from Exttriple"
                );
        }

        public bool Update(IExtTriple entity)
        {
            return mapperHelper.Update(entity,
                (cmd, exttriple) => UpdateParameters(cmd, exttriple),
                "update Exttriple set exttripleValue=@value where exttripleId=@id and exttripleDatetime = @datetime"
               );
        }

        public bool Delete(IExtTriple entity)
        {
            return mapperHelper.Delete(entity,
                (cmd, exttriple) => DeleteParameters(cmd, exttriple),
                 "delete from Exttriple where exttripleId=@id and exttripleDatetime = @datetime"
                );
        }

      
        public List<IExtTriple> MapAll(IDataReader reader)
        {
            return mapperHelper.MapAll(reader);
        }
    }
}

