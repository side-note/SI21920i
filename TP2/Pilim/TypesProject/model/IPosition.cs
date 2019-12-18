using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
    public interface IPosition
    {
        public int quantity { get; set; }
        public string name { get; set; }
        public string isin { get; set; }
    }
}
