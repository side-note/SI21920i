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

        public virtual ICollection<IPortfolio> portfolios { get; set; }
        public virtual ICollection<IInstrument> instruments { get; set; }
    }
}
