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
    class ExttripleRepository : IExttripleRepository
    {
        private IContext context;
        private ExttripleMapper mapper;
        public ExttripleRepository(IContext ctx)
        {
            context = ctx;
            mapper = new ExttripleMapper(ctx);
        }

        public bool Delete(IExtTriple value, Func<IExtTriple, bool> criteria)
        {
            if (criteria(value))
                return mapper.Delete(value);
            return false;
        }

        public IEnumerable<IExtTriple> Find(Func<IExtTriple, bool> criteria)
        {
            return FindAll().Where(criteria);
        }

        public IEnumerable<IExtTriple> FindAll()
        {
            return mapper.ReadAll();
        }

        public IExtTriple Insert(IExtTriple value)
        {
            return mapper.Create(value);
        }

        public bool Update(IExtTriple value, Func<IExtTriple, bool> criteria)
        {
            if (criteria(value))
                return mapper.Update(value);
            return false;
        }
    }
}

