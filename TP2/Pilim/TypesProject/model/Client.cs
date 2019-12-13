using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
    class Client
    {
        public Client() { }

        public int? nif { get; set; }

        public int ncc { get; set; }

        public String name { get; set; }

         public virtual ICollection<Portfolio> portfolios { get; set; }
        // N tenho a certeza, Client has contact 1 para n
        public virtual ICollection<Email> email { get; set; }
        public virtual ICollection<Phone> phone { get; set; }
    }
}
