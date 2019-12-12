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
        public DailyRegRepository(IContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<DailyReg> Find(System.Func<DailyReg, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<DailyReg> FindAll()
        {
            return new DailyRegMapper(context).ReadAll();
        }
    }
}

