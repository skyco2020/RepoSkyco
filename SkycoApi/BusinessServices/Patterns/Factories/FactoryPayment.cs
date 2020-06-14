using BusinessEntities.BE;
using DataModal.DataClasses;
using Resolver.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactoryPayment
    {
        private static FactoryPayment _factory;
        public static FactoryPayment GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryPayment();
            return _factory;
        }

        #region Business
        public PaymentBE CreateBusiness(Payments entity)
        {
            PaymentBE be;
            if (entity != null)
            {
                be = new PaymentBE()
                {
                   Currency = entity.Currency,
                   Description = entity.Description,
                   Id = entity.idpayment,
                   idcard = entity.idcard,
                   name = entity.name,
                   Quantity = entity.Quantity,
                   state = entity.state
                };
                if (entity.Payment_Skyco_Accounts != null)
                {
                    be.Payment_Skyco_Accounts = new List<Payment_Skyco_AccountBE>();
                    foreach (Payment_Skyco_Accounts item in entity.Payment_Skyco_Accounts)
                    {
                        be.Payment_Skyco_Accounts.Add(Factories.FactoryPayment_Skyco_Account.GetInstance().CreateBusiness(item));
                    }
                }
                return be;
            }
            return be = null;
        }
        #endregion

        #region Entity
        public Payments CreateEntity(PaymentBE be)
        {
            Payments entity;
            if (be != null)
            {
                entity = new Payments()
                {
                    Currency = be.Currency.ToString(),
                    Description = be.Description,
                    idpayment = be.Id ,
                    idcard = be.idcard,
                    name = be.name,
                    Quantity = be.Quantity,
                    state = be.state
                };
                if (be.Payment_Skyco_Accounts != null)
                {
                    entity.Payment_Skyco_Accounts = new List<Payment_Skyco_Accounts>();
                    foreach (Payment_Skyco_AccountBE item in be.Payment_Skyco_Accounts)
                    {
                        entity.Payment_Skyco_Accounts.Add(Factories.FactoryPayment_Skyco_Account.GetInstance().CreateEntity(item));
                    }
                }
                return entity;

            }
            return entity = null;
        }
        #endregion
    }
}
