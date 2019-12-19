using System.Collections.Generic;

namespace TypesProject.model
{
    public interface IMarket
    {
        public int code { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public ICollection<IDailyMarket> dailyMarkets { get; set; }
        public ICollection<IInstrument> marketInstruments { get; set; }
    }
}
