using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.dal;
using TypesProject.mapper;
using TypesProject.model;

namespace TypesProject.concrete
{
   public class ExttripleRepository : IExttripleRepository
    {
        private IContext context;
        private ExttripleMapper mapper;
        public ExttripleRepository(IContext ctx)
        {
            context = ctx;
            mapper = new ExttripleMapper(ctx);
        }

        public bool Delete(IExttriple value)
        {
            return mapper.Delete(value);
        }

        public IExttriple Find(params object[] keys)
        {
            KeyValuePair<string, DateTime> key = new KeyValuePair<string, DateTime>((string)keys[0], (DateTime)keys[1]);
            return mapper.Read(key);
        }

        public IExttriple Insert(IExttriple value)
        {
            return mapper.Create(value);
        }

        public bool Update(IExttriple value)
        {
            return mapper.Update(value);
        }
    }
}

