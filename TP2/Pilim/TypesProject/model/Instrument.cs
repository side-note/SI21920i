﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
    class Instrument
    {
        public Instrument() { }

        public int? isin { get; set; }

        public string description { get; set; }

        public Market mrktcode { get; set; }

        public virtual ICollection<Portfolio> instrumentPortfolios { get; set; }
        public virtual ICollection <DailyReg> dailyRegs { get; set; }
    }
}
