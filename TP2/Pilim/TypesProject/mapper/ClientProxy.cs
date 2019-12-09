using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.model;

namespace TypesProject.mapper
{
    class ClientProxy : Client
    {
     
            private IContext context;
            public ClientProxy(Client c, IContext ctx) : base()
            {
                context = ctx;

                base.nif = c.nif;
                base.name = c.name;
                base.ncc = c.ncc;
                base.portfolios = null;
            }
            public override ICollection<Portfolio> portfolios
            {
                get
                {
                    if (base.portfolios == null)//lazy load
                    {
                        IClientMapper ic = new IClientMapper(context);
                        base.portfolios = ic.Load(this);
                    }
                    return base.portfolios;
                }

                set
                {
                    base.portfolios = value;
                }
            }

        
    }
}
