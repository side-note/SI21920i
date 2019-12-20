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
    public class DailyMarketRepository : IDailyMarketRepository
    {
        private readonly IContext context;
        private DailyMarketMapper mapper;


        public DailyMarketRepository(IContext ctx)
        {
            context = ctx;
            mapper = new DailyMarketMapper(ctx);

        }

        public bool Delete(IDailyMarket value)
        {
            return mapper.Delete(value);
        }

        public IDailyMarket Find(params object[] keys)
        {
            KeyValuePair<int, DateTime> key = new KeyValuePair<int, DateTime>((int)keys[0], (DateTime)keys[1]);
            return mapper.Read(key);
        }

        public IDailyMarket Insert(IDailyMarket value)
        {
            return mapper.Create(value);
        }

        public bool Update(IDailyMarket value)
        {
            return mapper.Update(value);
        }
    }
}

