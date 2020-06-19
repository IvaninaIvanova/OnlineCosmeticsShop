using OC.Business.DTOs;
using OC.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace OC.WcfServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Brands" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Brands.svc or Brands.svc.cs at the Solution Explorer and start debugging.
    public class Brands : IBrands
    {
        private readonly BrandService brandService = new BrandService();

         public IEnumerable<BrandDto> GetAllByBrandName(string brandName) 
         {
             return brandService.GetAllByBrandName(brandName);
         }

         public IEnumerable<BrandDto> GetAll()
         {
             return brandService.GetAll();
         }

       

        public BrandDto GetById(int brandId)
        {
            return brandService.GetById(brandId);
        }

        public string Create(BrandDto brand)
        {
            if (!brand.IsValid())
            {
                return "Invalid Brand";
            }

            bool isCreated = brandService.Create(brand);

            return isCreated ? "Brand added successfully." : "Failed to create the Brand.";
        }

        public string Update(BrandDto brand)
        {
            if (!brand.IsValid())
            {
                return "Invalid Brand";
            }

            bool isUpdated = brandService.Update(brand);

            return isUpdated ? "Brand updated successfully." : "Failed to update the Brand";
        }

        public string Delete(int brandId)
        {
            bool isDeleted = brandService.Delete(brandId);

            return isDeleted ? "Brand deleted successfully." : "Failed to delete the Brand.";
        }
    }
}
