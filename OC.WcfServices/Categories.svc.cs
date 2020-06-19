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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Categories" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Categories.svc or Categories.svc.cs at the Solution Explorer and start debugging.
    public class Categories : ICategories
    {
        private readonly CategoryService categoryService = new CategoryService();

        public IEnumerable<CategoryDto> GetAllByCategoryName(string nameCategory)
        {
           // var isAuthenticated = OperationContext.Current.ServiceSecurityContext.WindowsIdentity.IsAnonymous;

            return categoryService.GetAllByCategoryName(nameCategory);
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            return categoryService.GetAll();
        }

        public CategoryDto GetById(int categoryId)
        {
            return categoryService.GetById(categoryId);
        }

        public string Create(CategoryDto category)
        {
            if (!category.IsValid())
            {
                return "Invalid category";
            }

            bool isCreated = categoryService.Create(category);

            return isCreated ? "Category added successfully." : "Failed to create the category.";
        }

        public string Update(CategoryDto category)
        {
            if (!category.IsValid())
            {
                return "Invalid category";
            }

            bool isUpdated = categoryService.Update(category);

            return isUpdated ? "Category updated successfully." : "Failed to update the category";
        }

        public string Delete(int categoryId)
        {
            bool isDeleted = categoryService.Delete(categoryId);

            return isDeleted ? "Category deleted successfully." : "Failed to delete the category.";
        }
    }
}
