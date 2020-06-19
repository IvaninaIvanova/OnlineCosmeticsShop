using OC.Business.DTOs;
using OC.Data;
using OC.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OC.Business.Services
{
    public class BrandService 
    {
        public IEnumerable<BrandDto> GetAllByBrandName(string brandName) 
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var brands = unitOfWork.BrandRepository.GetAll(b => b.BrandName == brandName);

                return brands.Select(brand => new BrandDto
                {
                    Id = brand.Id,
                    BrandName = brand.BrandName,
                    ManufacturerCountry = brand.ManufacturerCountry,
                    ProductClass = brand.ProductClass,
                    Rating = brand.Rating
                });
            }
        }

        public IEnumerable<BrandDto> GetAll()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var brands = unitOfWork.BrandRepository.GetAll();

                return brands.Select(brand => new BrandDto
                {
                    Id = brand.Id,
                    BrandName = brand.BrandName,
                    ManufacturerCountry = brand.ManufacturerCountry,
                    Rating = brand.Rating,
                    ProductClass = brand.ProductClass
                });
            }
        }


        /*   public IEnumerable<BrandDto> GetAll(string brandName = null)
           {
               using (UnitOfWork unitOfWork = new UnitOfWork())
               {
                   var brands = brandName != null
                       ? unitOfWork.BrandRepository.GetAll(d => d.BrandName == brandName)
                       : unitOfWork.BrandRepository.GetAll();

                   return brands.Select(brand => new BrandDto
                   {
                       Id = brand.Id,
                       BrandName = brand.BrandName,
                       ManufacturerCountry = brand.ManufacturerCountry,
                       Rating = brand.Rating,
                       ProductClass = brand.ProductClass
                   });
               }
           }*/

        public BrandDto GetById(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var brand = unitOfWork.BrandRepository.GetById(id);

                return brand == null ? null : new BrandDto
                {
                    Id = brand.Id,
                    BrandName = brand.BrandName,
                    ManufacturerCountry = brand.ManufacturerCountry,
                    Rating = brand.Rating,
                    ProductClass = brand.ProductClass
                };
            }
        }

        public bool Create(BrandDto brandDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var brand = new Brand()
                {
                    BrandName = brandDto.BrandName,
                    ManufacturerCountry = brandDto.ManufacturerCountry,
                    Rating = brandDto.Rating,
                    ProductClass = brandDto.ProductClass,
                    CreatedOn = DateTime.Now
                };

                unitOfWork.BrandRepository.Create(brand);

                return unitOfWork.Save();
            }
        }

        public bool Update(BrandDto brandDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var result = unitOfWork.BrandRepository.GetById(brandDto.Id);

                if (result == null)
                {
                    return false;
                }

                result.BrandName = brandDto.BrandName;
                result.ManufacturerCountry = brandDto.ManufacturerCountry;
                result.Rating = brandDto.Rating;
                result.ProductClass = brandDto.ProductClass;
                result.UpdatedOn = DateTime.Now;

                unitOfWork.BrandRepository.Update(result);

                return unitOfWork.Save();
            }
        }

        public bool Delete(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Brand result = unitOfWork.BrandRepository.GetById(id);

                if (result == null)
                {
                    return false;
                }

                unitOfWork.BrandRepository.Delete(result);

                return unitOfWork.Save();
            }
        }
    }
}
