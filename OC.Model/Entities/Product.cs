﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OC.Model.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public double Stars { get; set; }

        public string Description { get; set; }

        public bool Stock { get; set; }
    }
}
