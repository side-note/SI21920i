using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.concrete;
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
                base.portfolio = null;
                base.phone = null;
                base.email = null;
               
            }
             public override Portfolio portfolio
            {
                get
                {
                    if (base.portfolio == null)
                    {
                      ClientMapper cm = new ClientMapper(context);
                        base.portfolio = cm.LoadPortfolio(this);
                    }
                    return base.portfolio;
                }
                set
                {
                    base.portfolio = value;
                }
            }

        public override ICollection<Email> email
        {
            get
            {
                if (base.email == null)
                {
                    ClientMapper cm = new ClientMapper(context);
                    base.email = cm.LoadEmails(this);
                }
                return base.email;
            
            } 
            set => base.email = value; 
        
        }

        public override ICollection<Phone> phone 
        { get
            {
                if(base.phone==null)
                {
                    ClientMapper cm = new ClientMapper(context);
                    base.phone = cm.LoadPhones(this);
                }
                return base.phone;
            }
            
            
            set => base.phone = value;
        }

    }
}
