using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.concrete;
using TypesProject.model;

namespace TypesProject.mapper
{
    public class PositionProxy : Position
    {
        private IContext context;

        public PositionProxy(IPosition p, IContext ctx): base()
        {
            context = ctx;
            base.Instrument = null;
            base.isin = p.isin;
            base.name = p.name;
            base.Portfolio = null;
            base.quantity = p.quantity;
        }

        public decimal CurrVal { get; set; }
        public decimal Dailyvarperc { get; set; }
        public override IInstrument Instrument {
            get
            {
                if (base.Instrument == null)
                {
                    PositionMapper pm = new PositionMapper(context);
                    base.Instrument = pm.LoadInstruments(this);
                }
                return base.Instrument;
            }
            set => base.Instrument = value;
        }
        public override IPortfolio Portfolio {
            get {
                if (base.Portfolio == null)
                {
                    PositionMapper pm = new PositionMapper(context);
                    base.Portfolio = pm.LoadPortfolios(this);
                }
                return base.Portfolio; 
            }
            set => base.Portfolio = value; 
        }
    }
}
