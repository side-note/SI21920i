using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
    interface IClient
    {
        public int nif { get; set; }
        public int ncc { get; set; }
        public String name { get; set; }
    }
}
