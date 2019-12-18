using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
    public class Portfolio : IPortfolio
    {
        public Portfolio() { }
        public String name { get; set; }

        public double totalval { get; set; }

        public virtual IClient client { get; set; }

        public virtual ICollection<IPosition> positionInstruments { get; set; }
    }
}
