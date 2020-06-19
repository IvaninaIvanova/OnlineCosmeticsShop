using System;
using System.Collections.Generic;
using System.Text;

namespace OC.Business.DTOs
{
    public class ProductDto : BaseDto,IValidateable
    {
        public string ProductName { get; set; }

        public int CategoryId { get; set; }

        public virtual CategoryDto Category { get; set; }

        public int BrandId { get; set; }

        public virtual BrandDto Brand { get; set; }

        public double Stars { get; set; }

        public string Description { get; set; }

        public bool Stock { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(ProductName) && ProductName.Length < 50
               && !string.IsNullOrWhiteSpace(Description) && Description.Length < 250
               && Stars >= 1 && Stars <= 5;
        }
    }
}
