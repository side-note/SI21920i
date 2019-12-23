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
        public string name { get; set; }

        public decimal totalval { get; set; }

        public virtual IClient client { get; set; }

        public virtual IEnumerable<IPosition> Position { get; set; }
    }
}
