using System;
using System.Collections.Generic;

namespace TypesProject.model
{
    public interface IPortfolio
    {
        string name { get; set; }

        decimal totalval { get; set; }

         IClient client { get; set; }

        IList<IPosition> Positions { get; set; }
    }
}
