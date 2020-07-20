using BusinessEntities.BE;
using DataModal.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactoryStripeSubscribe
    {
        private static FactoryStripeSubscribe _factory;
        public static FactoryStripeSubscribe GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryStripeSubscribe();
            return _factory;
        }

        #region Business
        public StripeSubscribeBE CreateBusiness(StripeSubscribes entity)
        {
            StripeSubscribeBE be;
            if (entity != null)
            {
                be = new StripeSubscribeBE()
                {
                    Id = entity.idStripeSubscribe,
                    AccountId = entity.AccountId,
                    idSubscribe = entity.idSubscribe,
                    idCardStripe = entity.idCardStripe,
                    idStripeCustomer = entity.idStripeCustomer,
                    idPlanPriceStripe = entity.idPlanPriceStripe,
                    //idTokenStripe = entity.idTokenStripe,
                    SubscribeDate = entity.SubscribeDate,
                    state = entity.state
                };

                return be;
            }
            return be = null;
        }
        #endregion

        #region Entity
        public StripeSubscribes CreateEntity(StripeSubscribeBE be)
        {
            StripeSubscribes entity;
            if (be != null)
            {
                entity = new StripeSubscribes()
                {
                    idStripeSubscribe = be.Id,
                    AccountId = be.AccountId,
                    idSubscribe = be.idSubscribe,
                    idCardStripe = be.idCardStripe,
                    idStripeCustomer = be.idStripeCustomer,
                    idPlanPriceStripe = be.idPlanPriceStripe,
                    //idTokenStripe = be.idTokenStripe,
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
