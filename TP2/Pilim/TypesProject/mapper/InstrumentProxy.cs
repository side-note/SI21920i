﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.concrete;
using TypesProject.model;

namespace TypesProject.mapper
{
    public class InstrumentProxy: Instrument
    {
        IContext context;
        public InstrumentProxy(IInstrument i, IContext ctx) : base()
        {
            context = ctx;
            base.description = i.description;
            base.isin = i.isin;
            base.mrktcode = i.mrktcode;
            base.instrumentMarket = null;
            base.instrumentposition = null;
            base.dailyRegs = null;
        }

        public override ICollection<IDailyReg> dailyRegs {
            get
            {
                if (base.dailyRegs == null)
                {
                    InstrumentMapper im = new InstrumentMapper(context);
                    base.dailyRegs = im.LoadDailyRegs(this);
                }
                return base.dailyRegs;
            }
            set => base.dailyRegs = value; 
        }
        public decimal dailyvar { get; set; }
        public decimal currval { get; set; }
        public decimal avg6m { get; set; }
        public decimal var6m { get; set; }
        public decimal dailyvarperc { get; set; }
        public decimal var6mperc { get; set; }
        public override IMarket instrumentMarket {
            get
            {
                if (base.instrumentMarket == null)
                {
                    InstrumentMapper im = new InstrumentMapper(context);
                    base.instrumentMarket = im.LoadMarket(this);
                }
                return base.instrumentMarket;
            }
            set => base.instrumentMarket = value;
        }
        public override ICollection<IPosition> instrumentposition {
            get
            {
                if (base.instrumentposition == null)
                {
                    InstrumentMapper im = new InstrumentMapper(context);
                    base.instrumentposition = im.LoadPosition(this);
                }
                return base.instrumentposition;
            }
            set => base.instrumentposition = value; }
    }
}
