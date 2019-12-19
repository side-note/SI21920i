using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.model;

namespace EF_TP2_52D_14_1920i
{
    public partial class POSITION : IPosition
    {

        public ICollection<IPortfolio> portfolios { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICollection<IInstrument> instruments { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
