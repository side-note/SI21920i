using System;

namespace TypesProject.model
{
    public interface IDailyReg
    {
        public string isin { get; set; }
        public decimal? minval { get; set; }
        public decimal? openingval { get; set; }
        public decimal? maxval { get; set; }
        public decimal? closingval { get; set; }
        public DateTime dailydate { get; set; }
        public IInstrument instrument { get; set; } //relação instrument com dailyreg
    }
}
