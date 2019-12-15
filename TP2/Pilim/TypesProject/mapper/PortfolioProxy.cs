using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.concrete;
using TypesProject.model;

namespace TypesProject.mapper
{
    class PortfolioProxy : Portfolio
    {
        IContext context;
        public PortfolioProxy(Portfolio p, IContext ctx) : base()
        {
            context = ctx;
            base.client = null;
            base.name = p.name;
            base.portfolioInstruments = null;
            base.totalval = p.totalval;
        }
        public override Client client 
        {
            get
            {
                if(base.client== null)
                {
                    PortfolioMapper pm = new PortfolioMapper(context);
                    base.client = pm.LoadClient(this);
                }
                return base.client;
            }
                set => base.client = value;
            }
        public override ICollection<Instrument> portfolioInstruments 
        {
            get
            {
                if (base.portfolioInstruments == null)
                {
                    PortfolioMapper pm = new PortfolioMapper(context);
                    base.portfolioInstruments = pm.LoadPortfolios(this);
                }
                return base.portfolioInstruments;
            }
            set => base.portfolioInstruments = value; 
        }

    }
}
