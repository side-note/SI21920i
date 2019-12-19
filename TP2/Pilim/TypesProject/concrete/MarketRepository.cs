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
        private MarketMapper mapper;
        public MarketRepository(IContext ctx)
        {
            context = ctx;
        }

        public bool Delete(IMarket value, Func<IMarket, bool> criteria)
        {
            if (criteria(value))
                return mapper.Delete(value);
            return false;
        }

        public IEnumerable<IMarket> Find(Func<IMarket, bool> criteria)
        {
            return FindAll().Where(criteria);
        }

        public IEnumerable<IMarket> FindAll()
        {
            return mapper.ReadAll();
        }

        public IMarket Insert(IMarket value)
        {
            return mapper.Create(value);
        }

        public bool Update(IMarket value, Func<IMarket, bool> criteria)
        {
            if (criteria(value))
                return mapper.Update(value);
            return false;
        }
    }
}

