using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
    class DailyReg
    {
        public DailyReg() { }

        public Instrument? isin { get; set; }

        public double minval { get; set; }

        public double openingval { get; set; }

        public double maxval { get; set; }

        public double closingval { get; set; }

        public DateTime? dailydate { get; set; }

        //relação instrument com dailyreg

        public virtual ICollection<Instrument> instruments { get; set; }
    }
}