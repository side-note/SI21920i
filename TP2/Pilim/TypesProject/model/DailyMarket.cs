using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
     public class DailyMarket : IDailyMarket
       {
        public decimal? idxmrkt { get; set; }
        public decimal? dailyvar  { get; set; }
        public decimal? idxopeningval { get; set; }
        public int code { get; set; } //como saber que o code é mesmo do Market???
        public DateTime date { get; set; }
        public IMarket market { get; set; } //relação daily market com market
    }
}
