using Resolver.Cryptography;
using SkyCoApi.Infraestructure;
using SkyCoApi.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.FactoryDTO
{
    public class FactoryAccountDTO
    {
        private static FactoryAccountDTO myfactory;

        public static FactoryAccountDTO GetInstance()
        {
            if (myfactory == null)
                myfactory = new FactoryAccountDTO();
            return myfactory;
        }

        //public AccountDTO CreateDTO(Skyco_Account entity)
        //{
        //    AccountDTO dto = new AccountDTO()
        //    {
        //        Username = entity.Username,
        //        PasswordHash = MD5Base.GetInstance().Decrypt(entity.PasswordHash),
        //        EmailAddress = entity.EmailAddress,
        //        PhoneNumber = entity.PhoneNumber,
        //        AccountImage = entity.AccountImage,
        //        AccountImageUrl = entity.AccountImageUrl,
        //        AccountState = entity.AccountState,
        //        VoidedBy = entity.VoidedBy,
        //        VoidedAt = entity.VoidedAt,
        //        UpdatedBy = entity.UpdatedBy,
        //        Voided = entity.Voided,
        //        CreatedAt = entity.CreatedAt,
        //        CreatedBy = entity.CreatedBy,
        //        IsLoggedIn = entity.IsLoggedIn,
        //        LastLoginDate = entity.LastLoginDate,
        //        UpdatedAt = entity.UpdatedAt
        //    };
           
        //    return dto;
        //}
    }
}