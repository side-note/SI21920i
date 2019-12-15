using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.concrete;
using TypesProject.model;

namespace TypesProject.mapper
{
    class MarketProxy : Market
    {
        private IContext context;
        public MarketProxy(Market m,IContext ctx ) : base()
        {
            context = ctx;
            base.Code = m.Code;
            base.Description = m.Description;
            base.Name = m.Name;
            base.marketInstruments = null;
            base.dailyMarkets = null;

        }

        public override ICollection<DailyMarket> dailyMarkets { 
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
        public override ICollection<Instrument> marketInstruments {
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
