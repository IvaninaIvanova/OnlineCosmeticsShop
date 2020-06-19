using OC.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace OC.WcfServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBrands" in both code and config file together.
    [ServiceContract]
    public interface IBrands
    {
        [OperationContract]
         IEnumerable<BrandDto> GetAllByBrandName(string brandName);

         [OperationContract]
         IEnumerable<BrandDto> GetAll();

       

        [OperationContract]
        BrandDto GetById(int directorId);

        [OperationContract]
        string Create(BrandDto director);

        [OperationContract]
        string Update(BrandDto director);

        [OperationContract]
        string Delete(int directorId);
    }
}
