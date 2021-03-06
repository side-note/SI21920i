﻿namespace TypesProject.model
{
    public interface IEmail
    {
        public string addr { get; set; }
        public string description { get; set; }
        public int code { get; set; }
        public decimal? nif { get; set; }
        public  IClient ClientEmail { get; set; }
    }
}
