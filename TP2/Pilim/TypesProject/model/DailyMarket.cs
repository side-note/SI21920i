using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
     public class DailyMarket
       {
        public double IdxMrkt { get; set; }
        public double DailyVar  { get; set; }
        public double IdxOpeningVal { get; set; }
        public Market? Code { get; set; }
        public DateTime? Date { get; set; }

        //relação daily market com market

        public virtual Market market { get; set; }

    }
}
