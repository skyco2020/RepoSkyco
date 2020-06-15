using BusinessEntities.BE;
using SkyCoApi.Models.DTO.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.FactoryDTO
{
    public class FactoryCardDTO
    {
        private static FactoryCardDTO _factory;
        public static FactoryCardDTO GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryCardDTO();
            return _factory;
        }

        #region Create DTO
        public CardDTO CreateDTO(CardBE be)
        {
            CardDTO entity;
            if (be != null)
            {
                entity = new CardDTO()
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
                    idcard = be.Id,
                    idtoken = be.idtoken,
                    last4 = be.last4,
                    name = be.name,
                    objectcard = be.objectcard,
                    state = be.state,
                    tokenization_method = be.tokenization_method
                };
                return entity;

            }
            return null;
        }
        #endregion
    }
}