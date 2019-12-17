namespace TypesProject.model
{
    interface IInstrument
    {
        public string isin { get; set; }

        public string description { get; set; }

        public int mrktcode { get; set; }
    }
}
