using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.concrete;
using TypesProject.model;

namespace TypesProject.mapper
{
    class DailyMarketProxy : DailyMarket
    {
        private IContext context;
        private int? marketId;
        public DailyMarketProxy(DailyMarket dm, IContext ctx, int? marketID) : base()
        {
            context = ctx;
            base.Code = dm.Code;
            base.DailyVar = dm.DailyVar;
            base.Date = dm.Date;
            base.IdxMrkt = dm.IdxMrkt;
            base.IdxOpeningVal = dm.IdxOpeningVal;
            base.market = null;
            marketId = marketID;

        }

        public override Market market {
            get
            {
                if (market == null)
                {
                    DailyMarketMapper dmm = new DailyMarketMapper(context);
                    base.market = dmm.LoadMarket(this);
                }
                return base.market;
            }
            set => base.market = value; 
        }
    }
}
