using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace ATCSDL
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Price {  get; set; }
        public int number { get; set; }
        public string Datetime { get; set; }
        public byte[] image { get; set; }
        public string description { get; set; }
        public double score { get; set; }
        public int category { get; set; }
        public string loginSupplier { get; set; }

        public Product(int iD, string name, int price, int number, string datetime, byte[] image, string description, double score, int category, string loginSupplier)
        {
            ID = iD;
            this.loginSupplier = loginSupplier;
            Name = name;
            Price = price;
            this.number = number;
            Datetime = datetime;
            this.image = image;
            this.description = description;
            this.score = score;
            this.category = category;
        }

        public Product() { }
    }
}