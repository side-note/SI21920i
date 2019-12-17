using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
    interface IDailyMarket
    {
        public double IdxMrkt { get; set; }
        public double DailyVar { get; set; }
        public double IdxOpeningVal { get; set; }
        public int Code { get; set; } //como saber que o code é mesmo do Market???
        public DateTime Date { get; set; }
        public Market market { get; set; } //relação daily market com market

    }
}
