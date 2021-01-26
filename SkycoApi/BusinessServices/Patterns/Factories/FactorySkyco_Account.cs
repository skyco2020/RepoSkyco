using BusinessEntities.BE;
using BusinessServices.Patterns.Factories;
using DataModal.DataClasses;
using Resolver.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
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
        public Skyco_AccountBE CreateBusiness (Skyco_Accounts entity)
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
                    Location = entity.Location != null ? FactoryLocation.GetInstance().CreateBusiness(entity.Location) : null,
                    PasswordHash = entity.PasswordHash,
                    PhoneNumber = entity.PhoneNumber,
                    UpdatedAt = entity.UpdatedAt,
                    UpdatedBy = entity.UpdatedBy,
                    UserId = entity.UserId,
                    Username = entity.Username,
                    Voided = entity.Voided,
                    VoidedAt = entity.VoidedAt,
                    VoidedBy = entity.VoidedBy,
                    Skyco_AccountType = entity.Skyco_AccountType != null ? FactorySkyco_AccountType.GetInstance().CreateBusiness(entity.Skyco_AccountType) : null,
                };
                if (entity.Perfils != null)
                {
                    BE.Perfils = new List<PerfilBE>();
                    foreach (Perfils item in entity.Perfils)
                    {
                        BE.Perfils.Add(FactoryPerfil.GetInstance().CreateBusiness(item));
                    }
                }
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
                    Location = BE.Location != null ? FactoryLocation.GetInstance().CreateEntity(BE.Location) : null,
                    PasswordHash = MD5Base.GetInstance().Encypt(BE.PasswordHash),
                    PhoneNumber = BE.PhoneNumber,
                    UpdatedAt = BE.UpdatedAt,
                    UpdatedBy = BE.UpdatedBy,
                    UserId = BE.UserId,
                    Username = BE.Username,
                    Voided = BE.Voided,
                    VoidedAt = BE.VoidedAt,
                    VoidedBy = BE.VoidedBy
                };

                if (BE.Perfils != null)
                {
                    entity.Perfils = new List<Perfils>();
                    foreach (PerfilBE item in BE.Perfils)
                    {
                        entity.Perfils.Add(FactoryPerfil.GetInstance().CreateEntity(item));
                    }
                }
                return entity;
            }
            return entity = new Skyco_Accounts();
        }
        #endregion
    }
}
