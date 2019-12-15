using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
    public class Client
    {
        public Client() { }

        public int? nif { get; set; }

        public int ncc { get; set; }

        public String name { get; set; }

        public virtual Portfolio portfolio { get; set; }
        public virtual ICollection<Email> email { get; set; }
        public virtual ICollection<Phone> phone { get; set; }
    }
}
