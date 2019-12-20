using System;

namespace TypesProject.model
{
    public interface IExttriple
    {
        public decimal value { get; set; }

        public string id { get; set; }

        public DateTime datetime { get; set; }
    }
}
