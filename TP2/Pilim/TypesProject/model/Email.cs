﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesProject.model
{
    class Email
    {
        public Email(){}

        public string addr { get; set; }

        public string description { get; set; }

        public int? code { get; set; }

        public virtual ICollection<Client> emailClients { get; set; }
    }
}
