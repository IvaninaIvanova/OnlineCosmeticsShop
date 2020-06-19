using OC.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace OC.WcfServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICategories" in both code and config file together.
    [ServiceContract]
    public interface ICategories
    {
        [OperationContract]
        IEnumerable<CategoryDto> GetAllByCategoryName(string nameCategory);

        [OperationContract]
        IEnumerable<CategoryDto> GetAll();

        [OperationContract]
        CategoryDto GetById(int genreId);

        [OperationContract]
        string Create(CategoryDto genre);

        [OperationContract]
        string Update(CategoryDto genre);

        [OperationContract]
        string Delete(int genreId);
    }
}
