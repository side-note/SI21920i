using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
    public class Email : IEmail
    {
        public Email(){}

        public string addr { get; set; }

        public string description { get; set; }

        public int code { get; set; }

        public virtual Client ClientEmail { get; set; }
    }
}
