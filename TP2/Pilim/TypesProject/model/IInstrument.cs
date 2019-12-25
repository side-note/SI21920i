using System.Collections.Generic;

namespace TypesProject.model
{
    public interface IInstrument
    {
        public string isin { get; set; }

        public string description { get; set; }

        public int mrktcode { get; set; }

        public ICollection<IPosition> instrumentposition { get; set; }
        public ICollection<IDailyReg> dailyRegs { get; set; }
        public IMarket instrumentMarket { get; set; }
    }
}
