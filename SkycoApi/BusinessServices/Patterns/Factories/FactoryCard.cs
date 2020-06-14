using BusinessEntities.BE;
using DataModal.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactoryCard
    {
        private static FactoryCard _factory;
        public static FactoryCard GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryCard();
            return _factory;
        }

        #region Business
        public CardBE CreateBusiness(Cards entity)
        {
            CardBE be;
            if (entity != null)
            {
                be = new CardBE()
                {
                    address_city = entity.address_city,
                    address_country = entity.address_country,
                    address_line1 = entity.address_line1,
                    address_line1_check = entity.address_line1_check,
                    address_line2 = entity.address_line2,
                    address_state = entity.address_state,
                    address_zip = entity.address_zip,
                    address_zip_check =entity.address_zip_check,
                    brand = entity.brand,
                    country = entity.country,
                    cvc_check = entity.cvc_check,
                    dynamic_last4 = entity.dynamic_last4,
                    exp_month =entity.exp_month,
                    exp_year = entity.exp_year,
                    funding = entity.funding,
                    id = entity.id,
                    Id = entity.idcard,
                    idtoken = entity.idtoken,
                    last4 = entity.last4,
                    name = entity.name,
                    objectcard = entity.objectcard,
                    state = entity.state,
                    tokenization_method = entity.tokenization_method,
                    Tokens = entity.Tokens != null ? FactoryToken.GetInstance().CreateBusiness(entity.Tokens) : null
                };

                return be;
            }
            return be = null;
        }
        #endregion

        #region Entity
        public Cards CreateEntity(CardBE be)
        {
            Cards entity;
            if (be != null)
            {
                entity = new Cards()
                {
                    address_city = be.address_city,
                    address_country = be.address_country,
                    address_line1 = be.address_line1,
                    address_line1_check = be.address_line1_check,
                    address_line2 = be.address_line2,
                    address_state = be.address_state,
                    address_zip = be.address_zip,
                    address_zip_check = be.address_zip_check,
                    brand = be.brand,
                    country = be.country,
                    cvc_check = be.cvc_check,
                    dynamic_last4 = be.dynamic_last4,
                    exp_month = be.exp_month,
                    exp_year = be.exp_year,
                    funding = be.funding,
                    id = be.id,
                    idcard = be.Id ,
                    idtoken = be.idtoken,
                    last4 = be.last4,
                    name = be.name,
                    objectcard = be.objectcard,
                    state = be.state,
                    tokenization_method = be.tokenization_method
                };
                return entity;

            }
            return entity = null;
        }
        #endregion
    }
}
