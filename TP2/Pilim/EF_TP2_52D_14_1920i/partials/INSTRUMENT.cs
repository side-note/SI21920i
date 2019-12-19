using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.model;

namespace EF_TP2_52D_14_1920i
{
    public partial class Instrument : IInstrument
    {
        public ICollection<IPosition> instrumentposition { get; set; }
        public ICollection<IDailyReg> dailyRegs { get; set; }
        public IMarket instrumentMarket { get; set ; }
    }
}
