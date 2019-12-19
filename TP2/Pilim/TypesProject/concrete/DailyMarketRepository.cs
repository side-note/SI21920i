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
        private DailyMarketMapper mapper;


        public DailyMarketRepository(IContext ctx)
        {
            context = ctx;
            mapper = new DailyMarketMapper(ctx);

        }

        public bool Delete(IDailyMarket value, Func<IDailyMarket, bool> criteria)
        {
            if (criteria(value))
                return mapper.Delete(value);
            return false;
        }

        public IEnumerable<IDailyMarket> Find(Func<IDailyMarket, bool> criteria)
        {
            
            return FindAll().Where(criteria);
        }

        public IEnumerable<IDailyMarket> FindAll()
        {
            return mapper.ReadAll();
        }

        public IDailyMarket Insert(IDailyMarket value)
        {
            return mapper.Create(value);
        }

        public bool Update(IDailyMarket value, Func<IDailyMarket, bool> criteria)
        {
            if (criteria(value))
                return mapper.Update(value);
            return false;
        }
    }
}

