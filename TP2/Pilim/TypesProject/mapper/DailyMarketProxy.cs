using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.model;

namespace TypesProject.mapper
{
    class DailyMarketProxy : DailyMarket
    {
        private IContext context;
        public DailyMarketProxy(DailyMarket dm, IContext ctx, int? marketID) : base()
        {
            context = ctx;

        }
    }
}
