using BusinessEntities.BE;
using DataModal.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeServices.Patterns.Factories
{
    public class FactorySkyco_Account
    {
        #region single
        private static FactorySkyco_Account _factory;

        public static FactorySkyco_Account GetInstance()
        {
            if (_factory == null)
                _factory = new FactorySkyco_Account();
            return _factory;
        }
        #endregion

        #region BusinessEntities
        public Skyco_AccountBE CreateBusiness(Skyco_Accounts entity)
        {
            Skyco_AccountBE BE;
            if (entity != null)
            {
                BE = new Skyco_AccountBE()
                {
                    AccountId = entity.AccountId,
                    AccountImage = entity.AccountImage,
                    AccountImageUrl = entity.AccountImageUrl,
                    AccountState = entity.AccountState,
                    AccountTypeId = entity.AccountTypeId,
                    CreatedAt = entity.CreatedAt,
                    CreatedBy = entity.CreatedBy,
                    EmailAddress = entity.EmailAddress,
                    IsLoggedIn = entity.IsLoggedIn,
                    LastLoginDate = entity.LastLoginDate,
                    LocationId = entity.LocationId,
                    PasswordHash = entity.PasswordHash,
                    PhoneNumber = entity.PhoneNumber,
                    UpdatedAt = entity.UpdatedAt,
                    UpdatedBy = entity.UpdatedBy,
                    UserId = entity.UserId,
                    Username = entity.Username,
                    Voided = entity.Voided,
                    VoidedAt = entity.VoidedAt,
                    VoidedBy = entity.VoidedBy,
                };
                return BE;
            };
            return BE = new Skyco_AccountBE();
        }
        #endregion

        #region CreateEntities
        public Skyco_Accounts CreateEntity(Skyco_AccountBE BE)
        {
            Skyco_Accounts entity;
            if (BE != null)
            {
                entity = new Skyco_Accounts()
                {
                    AccountId = BE.AccountId,
                    AccountImage = BE.AccountImage,
                    AccountImageUrl = BE.AccountImageUrl,
                    AccountState = BE.AccountState,
                    AccountTypeId = BE.AccountTypeId,
                    CreatedAt = BE.CreatedAt,
                    CreatedBy = BE.CreatedBy,
                    EmailAddress = BE.EmailAddress,
                    IsLoggedIn = BE.IsLoggedIn,
                    LastLoginDate = BE.LastLoginDate,
                    LocationId = BE.LocationId,
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
                return entity;
            }
            return entity = new Skyco_Accounts();
        }
        #endregion
    }
}
