using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OC.Website.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string ProductName { get; set; }

        [Range(1.0, 5.0)]
        public double Stars { get; set; }

        [Required, MaxLength(150)]
        public string Description { get; set; }

        public bool Stock { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public CategoryViewModel Category { get; set; }

        [DisplayName("Brand")]
        public int BrandId { get; set; }

        public BrandViewModel Brand { get; set; }
    }
}
