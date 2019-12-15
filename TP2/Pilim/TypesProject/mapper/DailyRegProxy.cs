using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.concrete;
using TypesProject.model;

namespace TypesProject.mapper
{
    
    class DailyRegProxy : DailyReg
    {
        IContext context;
        int? instumentId;

        public DailyRegProxy(DailyReg dr, IContext ctx, int? instrumentID): base()
        {
            context = ctx;
            instumentId = instrumentID;
            base.closingval = dr.closingval;
            base.dailydate = dr.dailydate;
            base.instrument = null;
            base.isin = dr.isin;
            base.maxval = dr.maxval;
            base.minval = dr.minval;
            base.openingval = dr.openingval;
        }

        public override Instrument instrument {
            get
            {
                if(base.instrument == null)
                {
                  DailyRegMapper im = new DailyRegMapper(context);
                    base.instrument=im.LoadInstrument(this);
                }
                return base.instrument;
            }
            set => base.instrument = value; 
        }
    }
}
