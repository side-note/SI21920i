using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.concrete;
using TypesProject.model;

namespace TypesProject.mapper
{
    class PhoneProxy :Phone
    {
        private IContext context;
        public PhoneProxy(Phone p, IContext ctx) : base()
        {
            context = ctx;
            base.areacode = p.areacode;
            base.code =p.code;
            base.description = p.description;
            base.number = p.number;
            base.clientPhone = null;
        }

        public override IClient clientPhone 
        {
            get
            {
                if (base.clientPhone == null)
                {
                    PhoneMapper pm = new PhoneMapper(context);
                    base.clientPhone = pm.LoadClient(this);
                }
                return base.clientPhone;
            }
            
            set => base.clientPhone = value; 
        }
    }
}
