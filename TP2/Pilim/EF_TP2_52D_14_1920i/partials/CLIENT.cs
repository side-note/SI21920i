using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.model;

namespace EF_TP2_52D_14_1920i
{
    public partial class Client : IClient
    {
        public IPortfolio portfolio { get; set; }
        public ICollection<IEmail> email { get; set; }
        public ICollection<IPhone> phone { get; set; }
       
    }
}
