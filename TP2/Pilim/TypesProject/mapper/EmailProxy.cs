using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.concrete;
using TypesProject.model;

namespace TypesProject.mapper
{
   public  class EmailProxy: Email
    {
        IContext context;
        public EmailProxy(IEmail e, IContext ctx): base()
        {
            context = ctx;
            base.addr=e.addr;
            base.code = e.code;
            base.description = e.description;
            base.ClientEmail = null;
        }

        public override IClient ClientEmail {
            get
            {
                if(base.ClientEmail == null)
                {
                    EmailMapper em = new EmailMapper(context);
                    base.ClientEmail = em.LoadClient(this);
                }
                return base.ClientEmail;
            }

            set => base.ClientEmail = value; 
        }
    }
}
