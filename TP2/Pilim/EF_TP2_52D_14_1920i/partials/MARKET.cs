using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.model;

namespace EF_TP2_52D_14_1920i
{
    public partial class Market : IMarket
    {
        public ICollection<IDailyMarket> dailyMarkets { get; set ; }
        public ICollection<IInstrument> marketInstruments { get; set; }
    }
}
