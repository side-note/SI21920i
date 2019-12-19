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
    class DailyRegRepository : IDailyRegRepository
    {
        private IContext context;
        private DailyRegMapper mapper;
        public DailyRegRepository(IContext ctx)
        {
            context = ctx;
            mapper = new DailyRegMapper(context);
        }

        public bool Delete(IDailyReg value, Func<IDailyReg, bool> criteria)
        {
            if (criteria(value))
                return mapper.Delete(value);
            return false;
        }

        public IEnumerable<IDailyReg> Find(Func<IDailyReg, bool> criteria)
        { 
            return FindAll().Where(criteria);
        }

        public IEnumerable<IDailyReg> FindAll()
        {
            return mapper.ReadAll();
        }

        public IDailyReg Insert(IDailyReg value)
        {
            return mapper.Create(value);
        }

        public bool Update(IDailyReg value, Func<IDailyReg, bool> criteria)
        {
            if (criteria(value))
                return mapper.Update(value);
            return false;
        }
    }
}

