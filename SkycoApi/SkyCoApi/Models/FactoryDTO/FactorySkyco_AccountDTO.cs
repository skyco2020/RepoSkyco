using BusinessEntities.BE;
using SkyCoApi.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.FactoryDTO
{
    public class FactorySkyco_AccountDTO
    {
        #region single
        private static FactorySkyco_AccountDTO _factory;

        public static FactorySkyco_AccountDTO GetInstance()
        {
            if (_factory == null)
                _factory = new FactorySkyco_AccountDTO();
            return _factory;
        }
        #endregion      

        #region CreateDTO
        public Skyco_AccountDTO CreateDTO(Skyco_AccountBE BE)
        {
            Skyco_AccountDTO dto;
            if (BE != null)
            {
                dto = new Skyco_AccountDTO()
                {
                    AccountId = BE.AccountId,
                    AccountImage = BE.AccountImage,
                    AccountImageUrl = BE.AccountImageUrl,
                    AccountState = BE.AccountState,
                    AccountType = BE.AccountType,
                    AccountTypeId = BE.AccountTypeId,
                    CreatedAt = BE.CreatedAt,
                    CreatedBy = BE.CreatedBy,
                    EmailAddress = BE.EmailAddress,
                    IsLoggedIn = BE.IsLoggedIn,
                    LastLoginDate = BE.LastLoginDate,
                    LocationId = BE.LocationId,
                    Location = BE.Location != null ? FactoryLocationDTO.GetInstance().CreateDTO(BE.Location) : null,
                    PasswordHash = BE.PasswordHash,
                    PhoneNumber = BE.PhoneNumber,
                    UpdatedAt = BE.UpdatedAt,
                    UpdatedBy = BE.UpdatedBy,
                    UserId = BE.UserId,
                    Username = BE.Username,
                    Voided = BE.Voided,
                    VoidedAt = BE.VoidedAt,
                    VoidedBy = BE.VoidedBy
                };
                return dto;
            }
            return null;
        }
        #endregion
    }
}