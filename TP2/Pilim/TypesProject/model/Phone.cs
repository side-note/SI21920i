using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
    public class Phone
    {
        public Phone() { }

        public int code { get; set; }

        public int number { get; set; }

        public string description { get; set; }

        public string areacode { get; set; }

        public virtual Client clientPhone { get; set; }
    }
}
