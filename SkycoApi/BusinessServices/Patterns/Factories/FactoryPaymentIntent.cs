using BusinessEntities.BE;
using StripeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
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
                    Price = entity.Price,
                    AccountId = entity.AccountId,
                    amount = entity.amount,
                    cardnumber = entity.cardnumber,
                    cvc = entity.cvc,
                    Email = entity.Email,
                    idPaymentIntent = entity.idPaymentIntent,
                    month = entity.month,
                    PlanId = entity.PlanId,
                    year = entity.year,
                    Description = entity.Description,
                    fullname = entity.fullname,
                    state = entity.state
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
                    Price = be.Price,
                    AccountId = be.AccountId,
                    amount = be.amount,
                    cardnumber = be.cardnumber,
                    cvc = be.cvc,
                    Email = be.Email,
                    idPaymentIntent = be.idPaymentIntent,
                    month = be.month,
                    PlanId = be.PlanId,
                    year = be.year,
                    Description = be.Description,
                    fullname = be.fullname,
                    state = be.state
                };                
                return entity;

            }
            return null;
        }
        #endregion
    }
}
