﻿using System;

namespace TypesProject.model
{
    public interface IDailyReg
    {
        public String isin { get; set; }
        public double minval { get; set; }
        public double openingval { get; set; }
        public double maxval { get; set; }
        public double closingval { get; set; }
        public DateTime dailydate { get; set; }
        public IInstrument instrument { get; set; } //relação instrument com dailyreg
    }
}