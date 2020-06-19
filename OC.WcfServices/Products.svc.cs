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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Products" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Products.svc or Products.svc.cs at the Solution Explorer and start debugging.
    public class Products : IProducts
    {
        private readonly ProductService productService = new ProductService();

        public IEnumerable<ProductDto> GetAllByProductName(string productName) 
        {
            return productService.GetAllByProductName(productName);
        }

        public IEnumerable<ProductDto> GetAll()
        {
            return productService.GetAll();
        }

        public ProductDto GetById(int productId)
        {
            return productService.GetById(productId);
        }

        public string Create(ProductDto product)
        {
            bool isCreated = productService.Create(product);

            return isCreated ? "Product added successfully." : "Failed to create the product.";
        }

        public string Update(ProductDto product)
        {
            bool isUpdated = productService.Update(product);

            return isUpdated ? "Product updated successfully." : "Failed to update the product";
        }

        public string Delete(int productId)
        {
            bool isDeleted = productService.Delete(productId);

            return isDeleted ? "Product deleted successfully." : "Failed to delete the product.";
        }
    }
}
