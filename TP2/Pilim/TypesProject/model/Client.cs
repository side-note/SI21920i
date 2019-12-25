using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
    public class Client : IClient
    {
        public Client() { }
        public decimal nif { get; set; }
        public decimal ncc { get; set; }
        public String name { get; set; }
        public virtual IPortfolio portfolio { get; set; }
        public virtual ICollection<IEmail> email { get; set; }
        public virtual ICollection<IPhone> phone { get; set; }
    }
}
