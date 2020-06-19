using OC.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace OC.WcfServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProducts" in both code and config file together.
    [ServiceContract]
    public interface IProducts
    {
        [OperationContract]
        IEnumerable<ProductDto> GetAllByProductName(string productName);

        [OperationContract]
        IEnumerable<ProductDto> GetAll();

        [OperationContract]
        ProductDto GetById(int movieId);

        [OperationContract]
        string Create(ProductDto movie);

        [OperationContract]
        string Update(ProductDto movie);

        [OperationContract]
        string Delete(int movieId);
    }
}
