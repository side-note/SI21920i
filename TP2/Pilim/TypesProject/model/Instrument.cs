using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
    public class Instrument
    {
        public Instrument() { }

        public string isin { get; set; }

        public string description { get; set; }

        public int mrktcode { get; set; }

        public virtual ICollection<Portfolio> instrumentPortfolios { get; set; }
        public virtual ICollection <DailyReg> dailyRegs { get; set; }
        public virtual Market instrumentMarket { get; set; }
    }
}
