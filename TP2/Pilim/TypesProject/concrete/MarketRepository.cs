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
    class MarketRepository: IMarketRepository
    {
        private IContext context;
        public MarketRepository(IContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<IMarket> Find(Func<IMarket, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<IMarket> FindAll()
        {
            return new MarketMapper(context).ReadAll();
        }
    }
}

