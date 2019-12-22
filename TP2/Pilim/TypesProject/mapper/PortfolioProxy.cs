using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.concrete;
using TypesProject.model;

namespace TypesProject.mapper
{
   public class PortfolioProxy : Portfolio
    {
        IContext context;
        public PortfolioProxy(IPortfolio p, IContext ctx) : base()
        {
            context = ctx;
            base.client = null;
            base.name = p.name;
            base.Positions = null;
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

        public override IList<IPosition> Positions {
            get
            {
                if (base.Positions==null) 
                {
                    PortfolioMapper pm = new PortfolioMapper(context);
                    base.Positions=pm.LoadPositions(this);
                }
               return base.Positions;
            }
            set => base.Positions = value; }

    }
}
