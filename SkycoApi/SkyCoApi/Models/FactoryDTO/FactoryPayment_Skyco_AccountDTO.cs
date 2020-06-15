using BusinessEntities.BE;
using SkyCoApi.Models.DTO.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.FactoryDTO
{
    public class FactoryPayment_Skyco_AccountDTO
    {
        private static FactoryPayment_Skyco_AccountDTO _factory;
        public static FactoryPayment_Skyco_AccountDTO GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryPayment_Skyco_AccountDTO();
            return _factory;
        }

        #region CreateDTO
        public Payment_Skyco_AccountDTO CreateDTO(Payment_Skyco_AccountBE be)
        {
            Payment_Skyco_AccountDTO entity;
            if (be != null)
            {
                entity = new Payment_Skyco_AccountDTO()
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