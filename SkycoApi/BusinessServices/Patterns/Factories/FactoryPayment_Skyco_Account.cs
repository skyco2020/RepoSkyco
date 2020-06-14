using BusinessEntities.BE;
using DataModal.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactoryPayment_Skyco_Account
    {
        private static FactoryPayment_Skyco_Account _factory;
        public static FactoryPayment_Skyco_Account GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryPayment_Skyco_Account();
            return _factory;
        }

        #region Business
        public Payment_Skyco_AccountBE CreateBusiness(Payment_Skyco_Accounts entity)
        {
            Payment_Skyco_AccountBE be;
            if (entity != null)
            {
                be = new Payment_Skyco_AccountBE()
                {
                    AccountId = entity.AccountId,
                    Amount = entity.Amount,
                    Id = entity.IdPaymentUser,
                    idPayment = entity.idPayment,
                    idstripecard = entity.idstripecard,
                    paymentdate = entity.paymentdate,
                    state = entity.state
                };

                return be;
            }
            return be = null;
        }
        #endregion

        #region Entity
        public Payment_Skyco_Accounts CreateEntity(Payment_Skyco_AccountBE be)
        {
            Payment_Skyco_Accounts entity;
            if (be != null)
            {
                entity = new Payment_Skyco_Accounts()
                {
                    AccountId = be.AccountId,
                    Amount = be.Amount,
                    IdPaymentUser = be.Id,
                    idPayment = be.idPayment,
                    idstripecard = be.idstripecard,
                    paymentdate = be.paymentdate,
                    state = be.state
                };
                return entity;

            }
            return entity = null;
        }
        #endregion
    }
}
