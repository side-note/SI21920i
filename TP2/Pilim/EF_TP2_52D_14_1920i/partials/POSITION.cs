﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesProject.model;

namespace EF_TP2_52D_14_1920i
{
    public partial class Position : IPosition
    {
        IPortfolio IPosition.Portfolio { get => Portfolio; set =>Portfolio = (Portfolio) value; }
        IInstrument IPosition.Instrument { get => Instrument; set=> Instrument = (Instrument) value; }
    }
}
