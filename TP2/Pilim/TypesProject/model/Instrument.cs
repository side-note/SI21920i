using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
    public class Instrument: IInstrument
    {
        public Instrument() { }

        public string isin { get; set; }

        public string description { get; set; }

        public int? mrktcode { get; set; }

        public virtual ICollection<IPosition> instrumentposition { get; set; }
        public virtual ICollection <IDailyReg> dailyRegs { get; set; }
        public virtual IMarket instrumentMarket { get; set; }
    }
}
