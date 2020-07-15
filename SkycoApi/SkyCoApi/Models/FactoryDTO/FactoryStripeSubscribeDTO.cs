using BusinessEntities.BE;
using SkyCoApi.Models.DTO.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.FactoryDTO
{
    public class FactoryStripeSubscribeDTO
    {
        private static FactoryStripeSubscribeDTO _factory;
        public static FactoryStripeSubscribeDTO GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryStripeSubscribeDTO();
            return _factory;
        }

        #region CreateDTO
        public StripeSubscribeDTO CreateDTO(StripeSubscribeBE be)
        {
            StripeSubscribeDTO entity;
            if (be != null)
            {
                entity = new StripeSubscribeDTO()
                {
                    idStripeSubscribe = be.Id,
                    AccountId = be.AccountId,
                    idSubscribe = be.idSubscribe,
                    idCardStripe = be.idCardStripe,
                    idStripeCustomer = be.idStripeCustomer,
                    idPlanPriceStripe = be.idPlanPriceStripe,
                    idTokenStripe = be.idTokenStripe,
                    SubscribeDate = be.SubscribeDate,
                    state = be.state
                };
                return entity;

            }
            return entity = null;
        }
        #endregion
    }
}