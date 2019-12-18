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

        public IEnumerable<IDailyReg> Find(Func<IDailyReg, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<IDailyReg> FindAll()
        {
            return new DailyRegMapper(context).ReadAll();
        }
    }
}

