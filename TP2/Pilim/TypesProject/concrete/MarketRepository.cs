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
    public class MarketRepository: IMarketRepository
    {
        private IContext context;
        private MarketMapper mapper;
        public MarketRepository(IContext ctx)
        {
            context = ctx;
        }

        public bool Delete(IMarket value)
        {
            return mapper.Delete(value);
        }

        public IMarket Find(params object[] keys)
        {
            return mapper.Read((int)keys[0]);
        }

        public IMarket Insert(IMarket value)
        {
            return mapper.Create(value);
        }

        public bool Update(IMarket value)
        {
            return mapper.Update(value);
        }
    }
}

