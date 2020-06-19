using System;
using System.Collections.Generic;
using System.Text;

namespace OC.Business.DTOs
{
    public class BrandDto : BaseDto,IValidateable
    {
        public string BrandName { get; set; }

        public string ManufacturerCountry { get; set; }

        public double Rating { get; set; }

        public int ProductClass { get; set; } // middle, hight

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(BrandName) && BrandName.Length < 50 
                && !string.IsNullOrWhiteSpace(ManufacturerCountry) && ManufacturerCountry.Length < 50
                && Rating >= 0 && Rating <= 10
                && ProductClass >= 1 && ProductClass <= 5;
        }
    }
}
