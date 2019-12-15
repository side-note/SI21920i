using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.concrete;
using TypesProject.model;

namespace TypesProject.mapper
{
    class InstrumentProxy: Instrument
    {
        IContext context;
        public InstrumentProxy(Instrument i, IContext ctx) : base()
        {
            base.description = i.description;
            base.isin = i.isin;
            base.mrktcode = i.mrktcode;
            base.instrumentMarket = null;
            base.instrumentPortfolios = null;
            base.dailyRegs = null;
        }

        public override ICollection<DailyReg> dailyRegs {
            get
            {
                if (base.dailyRegs == null)
                {
                    InstrumentMapper im = new InstrumentMapper(context);
                    base.dailyRegs = im.LoadDailyRegs(this);
                }
                return base.dailyRegs;
            }
            set => base.dailyRegs = value; 
        }
        public override Market instrumentMarket {
            get
            {
                if (base.instrumentMarket == null)
                {
                    InstrumentMapper im = new InstrumentMapper(context);
                    base.instrumentMarket = im.LoadMarket(this);
                }
                return base.instrumentMarket;
            }
            set => base.instrumentMarket = value;
        }
        public override ICollection<Portfolio> instrumentPortfolios {
            get
            {
                if (base.instrumentPortfolios == null)
                {
                    InstrumentMapper im = new InstrumentMapper(context);
                    base.instrumentPortfolios = im.LoadPortfolios(this);
                }
                return base.instrumentPortfolios;
            }
            set => base.instrumentPortfolios = value;
        }
    }
}
