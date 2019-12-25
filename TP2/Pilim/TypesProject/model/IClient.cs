using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
    public interface IClient
    {
        public decimal nif { get; set; }
        public decimal ncc { get; set; }
        public String name { get; set; }

        public  IPortfolio portfolio { get; set; }
        public ICollection<IEmail> email { get; set; }
        public  ICollection<IPhone> phone { get; set; }
    }
}

