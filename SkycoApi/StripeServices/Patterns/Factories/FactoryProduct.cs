using BusinessEntities.BE;
using DataModal.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeServices.Patterns.Factories
{
    public class FactoryProduct
    {
        private static FactoryProduct _factory;
        public static FactoryProduct GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryProduct();
            return _factory;
        }

        #region Business
        public ProductBE CreateBusiness(Products entity)
        {
            ProductBE be;
            if (entity != null)
            {
                be = new ProductBE()
                {
                    AccountId = entity.AccountId,
                    Description = entity.Description,
                    idproductStripe = entity.idproductStripe,
                    Id = entity.idProduct,
                    name = entity.name,
                    urlimg = entity.urlimg,
                    active = entity.active,
                    Accounts = entity.Accounts != null ? FactorySkyco_Account.GetInstance().CreateBusiness(entity.Accounts) : null
                };

                return be;
            }
            return be = null;
        }
        #endregion

        #region Entity
        public Products CreateEntity(ProductBE be)
        {
            Products entity;
            if (be != null)
            {
                entity = new Products()
                {
                    AccountId = be.AccountId,
                    Description = be.Description,
                    idproductStripe = be.idproductStripe,
                    idProduct = be.Id,
                    name = be.name,
                    urlimg = be.urlimg,
                    active = be.active,
                };
                return entity;

            }
            return entity = null;
        }
        #endregion
    }
}
