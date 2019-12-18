using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.concrete;
using TypesProject.model;

namespace TypesProject.mapper
{
    class PositionProxy : Position
    {
        private IContext context;

        public PositionProxy(Position p, IContext ctx): base()
        {
            context = ctx;
            base.instruments = null;
            base.isin = p.isin;
            base.name = p.name;
            base.portfolios = null;
            base.quantity = p.quantity;
        }
        public override ICollection<IInstrument> instruments {
            get
            {
                if (base.instruments == null)
                {
                    PositionMapper pm = new PositionMapper(context);
                    base.instruments = pm.LoadInstruments(this);
                }
                return base.instruments;
            }
            set => base.instruments = value;
        }
        public override ICollection<IPortfolio> portfolios {
            get {
                if (base.portfolios == null)
                {
                    PositionMapper pm = new PositionMapper(context);
                    base.portfolios = pm.LoadPortfolios(this);
                }
                return base.portfolios; 
            }
            set => base.portfolios = value; }
    }
}
