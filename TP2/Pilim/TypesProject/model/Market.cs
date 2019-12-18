﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
    public class Market : IMarket
    {
        public Market()
        {
        }
        public int Code { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public virtual ICollection<IDailyMarket> dailyMarkets { get; set; }
        public virtual ICollection<IInstrument> marketInstruments { get; set; }
    }
}
