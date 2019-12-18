using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.mapper
{
    public static class CollectionExtensions
    {
        public static void AddRange(this IDataParameterCollection collection, IEnumerable<IDataParameter> newItems)
        {
            foreach (IDataParameter item in newItems)
            {
                collection.Add(item);
            }
        }
    }

    class MapperHelper <T,Tid,TCol> where TCol : IList<T>, IEnumerable<T>, new()
    {
        public IContext context;
        IMapper<T, Tid, TCol> mapper;
        public MapperHelper(IContext ctx, IMapper<T,Tid,TCol> mapper) {
            context = ctx;
            this.mapper = mapper;
        }
        protected virtual CommandType SelectCommandType { get { return System.Data.CommandType.Text; } }
        protected virtual CommandType InsertCommandType { get { return System.Data.CommandType.Text; } }
        protected virtual CommandType DeleteCommandType { get { return System.Data.CommandType.Text; } }
        protected virtual CommandType SelectAllCommandType { get { return System.Data.CommandType.Text; } }
        protected virtual CommandType UpdateCommandType { get { return System.Data.CommandType.Text; } }
        protected void EnsureContext()
        {
            if (context == null)
                throw new InvalidOperationException("Data Context not set.");
        }
        public IDataReader ExecuteReader(String commandText, List<IDataParameter> parameters)
        {
            using (IDbCommand cmd = context.createCommand())
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                cmd.CommandText = commandText;
                return cmd.ExecuteReader(CommandBehavior.Default);
            }
        }

     
        protected void ExecuteNonQuery(String commandText, List<IDataParameter> parameters)
        {
            using (IDbCommand cmd = context.createCommand())
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                cmd.CommandText = commandText;
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
        }
        public virtual  T Create(T entity, Action<IDbCommand, T> InsertParameters, string command) {
            EnsureContext();
            using (IDbCommand cmd = context.createCommand())
            {
                cmd.CommandText = command;
                cmd.CommandType = InsertCommandType;
                InsertParameters(cmd, entity);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return entity;
            }
        }
        public virtual T Read(Tid id, Action<IDbCommand,Tid> SelectParameters, string command) {
            EnsureContext();
            using (IDbCommand cmd = context.createCommand())
            {
                cmd.CommandText = command;
                cmd.CommandType = SelectCommandType;
                SelectParameters(cmd, id);
                using (IDataReader reader = cmd.ExecuteReader())
                    return reader.Read() ? mapper.Map(reader) : default;
            }
        }
        public virtual TCol ReadAll(Action <IDbCommand> SelectAllParameters, string command) {
            EnsureContext();

            using (IDbCommand cmd = context.createCommand())
            {
                cmd.CommandText = command;
                cmd.CommandType = SelectAllCommandType;
                SelectAllParameters(cmd);
                using (IDataReader reader = cmd.ExecuteReader())
                    return MapAll(reader);
            }
        }
        public virtual bool Update(T entity,Action<IDbCommand,T> UpdateParameters, string command) {
            if (entity == null)
                throw new ArgumentException("The " + typeof(T) + " to update cannot be null");

            EnsureContext();

            using (IDbCommand cmd = context.createCommand())
            {
                cmd.CommandText = command;
                cmd.CommandType = UpdateCommandType;
                UpdateParameters(cmd, entity);
                int result = cmd.ExecuteNonQuery();
                return (result == 0) ? false : true;
            }
        }
        public virtual bool Delete(T entity, Action<IDbCommand, T> DeleteParameters, string command) {
            if (entity == null)
                throw new ArgumentException("The " + typeof(T) + " to delete cannot be null");

            EnsureContext();

            using (IDbCommand cmd = context.createCommand())
            {
                cmd.CommandText = command;
                cmd.CommandType = DeleteCommandType;
                DeleteParameters(cmd, entity);
                int result = cmd.ExecuteNonQuery();
                return (result == 0) ? false : true;
            }
        }
       
        public virtual TCol MapAll(IDataReader reader) {
            TCol collection = new TCol();
            while (reader.Read())
            {
                try
                {
                    collection.Add(mapper.Map(reader));
                }
                catch
                {
                    throw;
                }

            }
            return collection;

        }
    }
}
