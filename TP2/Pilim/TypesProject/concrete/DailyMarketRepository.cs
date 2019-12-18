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
    class DailyMarketRepository : IDailyMarketRepository
    {
        private IContext context;
        public DailyMarketRepository(IContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<IDailyMarket> Find(Func<IDailyMarket, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<IDailyMarket> FindAll()
        {
            return new DailyMarketMapper(context).ReadAll();
        }
    }
}

