using System;

namespace TypesProject.model
{
    public interface IExtTriple
    {
        public decimal value { get; set; }

        public int id { get; set; }

        public DateTime datetime { get; set; }
    }
}
