using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
   public class Position: IPosition
    {
        public int? quantity { get; set; }
        public string name { get; set; }
        public string isin { get; set; }

        public virtual IPortfolio Portfolio { get; set; }
        public virtual IInstrument Instrument { get; set; }
    }
}
