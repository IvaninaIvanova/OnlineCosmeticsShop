using OC.Business.DTOs;
using OC.Data;
using OC.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OC.Business.Services
{
    public class CategoryService
    {
        public IEnumerable<CategoryDto> GetAllByCategoryName(string name) 
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var categories = unitOfWork.CategoryRepository.GetAll(g => g.NameCategory == name);

                return categories.Select(category => new CategoryDto
                {
                    Id = category.Id,
                    NameCategory = category.NameCategory,
                    Subcategory = category.Subcategory,
                    Stars = category.Stars,
                    Delivery = category.Delivery
                });
            }
        }

        public IEnumerable<CategoryDto> GetAll()
        {

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var categories = unitOfWork.CategoryRepository.GetAll();

                return categories.Select(category => new CategoryDto
                {
                    Id = category.Id,
                    NameCategory = category.NameCategory,
                    Subcategory = category.Subcategory,
                    Stars = category.Stars,
                    Delivery = category.Delivery
                });
            }
        }

        public CategoryDto GetById(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var category = unitOfWork.CategoryRepository.GetById(id);

                return category == null ? null : new CategoryDto
                {
                    Id = category.Id,
                    NameCategory = category.NameCategory,
                    Subcategory = category.Subcategory,
                    Stars = category.Stars,
                    Delivery = category.Delivery
                };
            }
        }

        public bool Create(CategoryDto categoryDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var category = new Category()
                {
                    NameCategory = categoryDto.NameCategory,
                    Subcategory = categoryDto.Subcategory,
                    Stars = categoryDto.Stars,
                    Delivery = categoryDto.Delivery,
                    CreatedOn = DateTime.Now
                };

                unitOfWork.CategoryRepository.Create(category);

                return unitOfWork.Save();
            }
        }

        public bool Update(CategoryDto categoryDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var result = unitOfWork.CategoryRepository.GetById(categoryDto.Id);

                if (result == null)
                {
                    return false;
                }

                result.NameCategory = categoryDto.NameCategory;
                result.Subcategory = categoryDto.Subcategory;
                result.Stars = categoryDto.Stars;
                result.Delivery = categoryDto.Delivery;
                result.UpdatedOn = DateTime.Now;

                unitOfWork.CategoryRepository.Update(result);

                return unitOfWork.Save();
            }
        }

        public bool Delete(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Category result = unitOfWork.CategoryRepository.GetById(id);
                
                if (result == null)
                {
                    return false;
                }

                unitOfWork.CategoryRepository.Delete(result);

                return unitOfWork.Save();
            }
        }
    }
}
