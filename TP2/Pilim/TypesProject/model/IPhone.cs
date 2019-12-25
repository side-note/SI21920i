namespace TypesProject.model
{
    public interface IPhone
    {
        public int code { get; set; }

        public decimal number { get; set; }

        public string description { get; set; }

        public string areacode { get; set; }

        public decimal? nif { get; set; }

        public  IClient clientPhone { get; set; }

    }
}
