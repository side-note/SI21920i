using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.model;

namespace TypesProject.mapper
{
    interface IDailyMarketMapper : IMapper<IDailyMarket, KeyValuePair<int,DateTime>, List<IDailyMarket>>
    {
    }
}
