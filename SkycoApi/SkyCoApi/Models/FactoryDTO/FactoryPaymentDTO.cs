using BusinessEntities.BE;
using SkyCoApi.Models.DTO.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.FactoryDTO
{
    public class FactoryPaymentDTO
    {
        private static FactoryPaymentDTO _factory;
        public static FactoryPaymentDTO GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryPaymentDTO();
            return _factory;
        }

        #region Create DTO
        public PaymentDTO CreateDTO(PaymentBE be)
        {
            PaymentDTO dto;
            if (be != null)
            {
                dto = new PaymentDTO()
                {
                    Currency = be.Currency.ToString(),
                    Description = be.Description,
                    idpayment = be.Id,
                    idcard = be.idcard,
                    name = be.name,
                    Quantity = be.Quantity,
                    state = be.state
                };
                if (be.Payment_Skyco_Accounts != null)
                {
                    dto.Payment_Skyco_Accounts = new List<Payment_Skyco_AccountDTO>();
                    foreach (Payment_Skyco_AccountBE item in be.Payment_Skyco_Accounts)
                    {
                        dto.Payment_Skyco_Accounts.Add(FactoryDTO.FactoryPayment_Skyco_AccountDTO.GetInstance().CreateDTO(item));
                    }
                }
                return dto;

            }
            return null;
        }
        #endregion
    }
}