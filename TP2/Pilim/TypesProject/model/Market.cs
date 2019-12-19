using System;
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
        public int code { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public virtual ICollection<IDailyMarket> dailyMarkets { get; set; }
        public virtual ICollection<IInstrument> marketInstruments { get; set; }
    }
}
