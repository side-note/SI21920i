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
            base.positionInstruments = null;
            base.totalval = p.totalval;
        }
        public override IClient client 
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

        public override ICollection<IPosition> positionInstruments {
            get
            {
                if (base.positionInstruments==null) 
                {
                    PortfolioMapper pm = new PortfolioMapper(context);
                    base.positionInstruments=pm.LoadPositions(this);
                }
               return base.positionInstruments;
            }
            set => base.positionInstruments = value; }

    }
}
