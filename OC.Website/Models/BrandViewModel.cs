using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OC.Website.Models
{
    public class BrandViewModel
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string BrandName { get; set; }

        [Required, MaxLength(50)]
        public string ManufacturerCountry { get; set; }

        [Range(1, 10)]
        public double Rating { get; set; }

        [Range(1, 5)]
        public int ProductClass { get; set; }
    }
}
