using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OC.Website.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string NameCategory { get; set; }

        [Required, MaxLength(50)]

        public string Subcategory { get; set; }

        [Range(1,5)]
        public double Stars { get; set; }

        public bool Delivery { get; set; }
    }
}
