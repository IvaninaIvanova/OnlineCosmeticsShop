using System;
using System.Collections.Generic;
using System.Text;

namespace OC.Model.Entities
{
    public class Category : BaseEntity
    {
        public string NameCategory { get; set; }

        public string Subcategory { get; set; }

        public double Stars { get; set; }

        public bool Delivery { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}
