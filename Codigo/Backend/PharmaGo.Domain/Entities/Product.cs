﻿namespace PharmaGo.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Deleted { get; set; }
        public Pharmacy? Pharmacy { get; set; }

    }
}
