using System;
using System.Collections.Generic;
using System.Text;

namespace OC.Model.Entities
{
    public class Brand : BaseEntity
    {
        public string BrandName { get; set; }

        public string ManufacturerCountry { get; set; }

        public double Rating { get; set; }

        public int ProductClass { get; set; } // middle, hight

        public virtual ICollection<Product> Products { get; set; }
    }
}
