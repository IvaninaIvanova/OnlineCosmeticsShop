using System;
using System.Collections.Generic;
using System.Text;

namespace OC.Business.DTOs
{
    public class CategoryDto : BaseDto, IValidateable
    {
        public string NameCategory { get; set; }

        public string Subcategory { get; set; }

        public double Stars { get; set; }

        public bool Delivery { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(NameCategory) && NameCategory.Length < 50 
                && !string.IsNullOrWhiteSpace(Subcategory) && Subcategory.Length < 50
                && Stars >=1 && Stars <= 5;
        }
    }
}
