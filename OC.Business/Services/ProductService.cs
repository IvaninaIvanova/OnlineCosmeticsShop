using OC.Business.DTOs;
using OC.Data;
using OC.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OC.Business.Services
{
    public class ProductService
    {
        public IEnumerable<ProductDto> GetAllByProductName(string productName)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var products = unitOfWork.ProductRepository.GetAll(m => m.ProductName.Contains(productName));

                var result = products.Select(product => new ProductDto
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Stars = product.Stars,            
                    Stock = product.Stock,
                    Brand = new BrandDto
                    {
                        Id = product.BrandId,
                        BrandName = product.Brand.BrandName,
                        ManufacturerCountry = product.Brand.ManufacturerCountry,
                        ProductClass = product.Brand.ProductClass,
                        Rating = product.Brand.Rating
                    },
                    Category = new CategoryDto
                    {
                        Id = product.CategoryId,
                        NameCategory = product.Category.NameCategory,
                        Subcategory = product.Category.Subcategory,
                        Stars = product.Category.Stars,
                        Delivery = product.Category.Delivery
                    }
                }).ToList();

                return result;
            }
        }

        public IEnumerable<ProductDto> GetAll()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var products = unitOfWork.ProductRepository.GetAll();

                var result = products.Select(product => new ProductDto
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Stars = product.Stars,             
                    Stock = product.Stock,
                    Brand = new BrandDto
                    {
                        Id = product.BrandId,
                        BrandName = product.Brand.BrandName,
                        ManufacturerCountry = product.Brand.ManufacturerCountry,
                        ProductClass = product.Brand.ProductClass,
                        Rating = product.Brand.Rating
                    },
                    Category = new CategoryDto
                    {
                        Id = product.CategoryId,
                        NameCategory = product.Category.NameCategory,
                        Subcategory = product.Category.Subcategory,
                        Stars = product.Category.Stars,
                        Delivery = product.Category.Delivery
                    }
                }).ToList();

                return result;
            }
        }

        public ProductDto GetById(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var product = unitOfWork.ProductRepository.GetById(id);

                return product == null ? null : new ProductDto
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Stars = product.Stars,             
                    Stock = product.Stock,
                    BrandId = product.BrandId,
                    Brand = new BrandDto
                    {
                        Id = product.BrandId,
                        BrandName = product.Brand.BrandName,
                        ManufacturerCountry = product.Brand.ManufacturerCountry,
                        ProductClass = product.Brand.ProductClass,
                        Rating = product.Brand.Rating
                    },
                    CategoryId = product.CategoryId,
                    Category = new CategoryDto
                    {
                        Id = product.CategoryId,
                        NameCategory = product.Category.NameCategory,
                        Subcategory = product.Category.Subcategory,
                        Stars = product.Category.Stars,
                        Delivery = product.Category.Delivery
                    }
                };
            }
        }

        public bool Create(ProductDto productDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var product = new Product()
                {
                    ProductName = productDto.ProductName,
                    CategoryId = productDto.CategoryId,
                    BrandId = productDto.BrandId,
                    Description = productDto.Description,
                    Stars = productDto.Stars,
                    Stock = productDto.Stock,
                    CreatedOn = DateTime.Now
                };

                unitOfWork.ProductRepository.Create(product);

                return unitOfWork.Save();
            }
        }

        public bool Update(ProductDto productDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var result = unitOfWork.ProductRepository.GetById(productDto.Id);

                if (result == null)
                {
                    return false;
                }

                result.CategoryId = productDto.CategoryId;
                result.BrandId = productDto.BrandId;
                result.ProductName = productDto.ProductName;
                result.Description = productDto.Description;
                result.Stars = productDto.Stars;
                result.Stock = productDto.Stock;
                result.UpdatedOn = DateTime.Now;

                unitOfWork.ProductRepository.Update(result);

                return unitOfWork.Save();
            }
        }

        public bool Delete(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Product result = unitOfWork.ProductRepository.GetById(id);

                if (result == null)
                {
                    return false;
                }

                unitOfWork.ProductRepository.Delete(result);

                return unitOfWork.Save();
            }
        }
    }
}
