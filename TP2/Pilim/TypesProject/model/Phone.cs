using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
    public class Phone : IPhone
    {
        public Phone() { }

        public int code { get; set; }

        public decimal number { get; set; }

        public string description { get; set; }

        public string areacode { get; set; }

        public decimal? nif { get; set; }

        public virtual IClient clientPhone { get; set; }
    }
}
