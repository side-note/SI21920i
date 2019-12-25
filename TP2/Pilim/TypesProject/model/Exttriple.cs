using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
    public class Exttriple : IExttriple
    {
        public decimal? value { get; set; }
        public string id { get; set ; }
        public DateTime datetime { get ; set; }
    }
}
