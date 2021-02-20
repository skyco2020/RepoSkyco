using BusinessEntities.BE;
using SkyCoApi.Models.DTO.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.FactoryDTO
{
    public class FactoryProductDTO
    {
        private static FactoryProductDTO _factory;
        public static FactoryProductDTO GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryProductDTO();
            return _factory;
        }       
        #region CreateDTO
        public ProductDTO CreateDTO(ProductBE be)
        {
            ProductDTO dto;
            if (be != null)
            {
                dto = new ProductDTO()
                {
                    AccountId = be.AccountId,
                    Description = be.Description,
                    idproductStripe = be.idproductStripe,
                    idProduct = be.Id,
                    name = be.name,
                    urlimg = be.urlimg,
                    active = be.active,                    
                    Accounts = be.Accounts != null ? FactorySkyco_AccountDTO.GetInstance().CreateDTO(be.Accounts) : null
                };
                return dto;

            }
            return dto = null;
        }
        #endregion
    }
}