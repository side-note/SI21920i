using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.model;

namespace EF_TP2_52D_14_1920i
{

    public partial class PORTFOLIO : IPortfolio
    {
        //public string name { get; set ; }
       //public double totalval { get ; set ; }
        public IClient client { get; set ; }
        public ICollection<IPosition> positionInstruments { get; set; }
    }
}
