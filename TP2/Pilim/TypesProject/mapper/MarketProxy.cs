
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.concrete;
using TypesProject.model;

namespace TypesProject.mapper
{
    public class MarketProxy : Market
    {
        private IContext context;
        public MarketProxy(IMarket m,IContext ctx ) : base()
        {
            context = ctx;
            base.code = m.code;
            base.description = m.description;
            base.name = m.name;
            base.marketInstruments = null;
            base.dailyMarkets = null;

        }

        public override ICollection<IDailyMarket> dailyMarkets { 
            get 
            {
                if (base.dailyMarkets == null)
                {
                    MarketMapper mm = new MarketMapper(context);
                    base.dailyMarkets = mm.LoadDailyMarkets(this);
                }
                return base.dailyMarkets;

            } 
            set => base.dailyMarkets = value; 
        }
        public override ICollection<IInstrument> marketInstruments {
            get
            {
                if(base.marketInstruments == null){
                    MarketMapper mm = new MarketMapper(context);
                    base.marketInstruments = mm.LoadInstruments(this);
                }
                return base.marketInstruments;
            }
            
            set => base.marketInstruments = value;
        }
    }
}
