using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
    class Portfolio
    {
        public Portfolio() { }
        public String? name { get; set; }

        public double totalval { get; set; }

        ICollection<Client> clients { get; set; }

        ICollection<Instrument> portfolioInstruments { get; set; }
    }
}
