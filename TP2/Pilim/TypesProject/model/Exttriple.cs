using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
    public class Exttriple : IExtTriple
    {
        public Exttriple() { }

        public decimal value { get; set; }

        public int id { get; set; }

        public DateTime datetime { get; set; }
    }
}
