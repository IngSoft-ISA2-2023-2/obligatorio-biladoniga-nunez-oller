using PharmaGo.Exceptions;
using System;
using System.Collections.Generic;

namespace PharmaGo.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Deleted { get; set; }
        public int Stock { get; set; }
        public Pharmacy? Pharmacy { get; set; }

        public void ValidOrFail()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Description)
                     || Price <= 0 || Code <=0 || Code.ToString().Length != 5 ||
                     Name.Length > 30 || Description.Length > 70
                     )
            {
                throw new InvalidResourceException("The Product is not correctly created.");
            }
        }
    }
}
