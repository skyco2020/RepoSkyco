using BusinessEntities.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeServices.Patterns.Factories
{
    public class FactoryPaymentIntent
    {
        private static FactoryPaymentIntent _factory;
        public static FactoryPaymentIntent GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryPaymentIntent();
            return _factory;
        }

        #region Business
        public PaymentIntentBE CreateBusiness(PaymentIntent entity)
        {
            PaymentIntentBE be;
            if (entity != null)
            {
                be = new PaymentIntentBE()
                {
                    iDPlanPrice = entity.iDPlanPrice,
                    AccountId = entity.AccountId,
                    email = entity.Email,
                    idPaymentIntent = entity.idPaymentIntent,
                    description = entity.Description,
                    fullname = entity.fullname,
                    state = entity.state,
                    cardnumber = entity.cardnumber,
                    cvc = entity.cvc,
                    month = entity.month,
                    value = entity.value,
                    year = entity.year
                };
                return be;
            }
            return null;
        }
        #endregion

        #region Entity
        public PaymentIntent CreateEntity(PaymentIntentBE be)
        {
            PaymentIntent entity;
            if (be != null)
            {
                entity = new PaymentIntent()
                {
                    iDPlanPrice = be.iDPlanPrice,
                    AccountId = be.AccountId,
                    Email = be.email,
                    idPaymentIntent = be.idPaymentIntent,
                    Description = be.description,
                    fullname = be.fullname,
                    state = be.state,
                    cardnumber = be.cardnumber,
                    cvc = be.cvc,
                    month = be.month,
                    value = be.value,
                    year = be.year
                };
                return entity;

            }
            return null;
        }
        #endregion
    }
}
