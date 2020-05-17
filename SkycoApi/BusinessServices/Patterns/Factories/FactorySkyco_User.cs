using BusinessEntities.BE;
using DataModal.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactorySkyco_User
    {
        #region single
        private static FactorySkyco_User _Factory;

        public static FactorySkyco_User GetInstance()
        {
            if (_Factory == null)
                _Factory = new FactorySkyco_User();
            return _Factory;
        }

        #endregion

        #region BusinessEntities
        public Skyco_UserBE CreateBusiness (Skyco_Users entity)
        {
            Skyco_UserBE BE;
            if (entity != null)
            {
                BE = new Skyco_UserBE()
                {
                    UserId = entity.UserId,
                    Address = entity.Address,
                    CreatedAt = entity.CreatedAt,
                    CreatedBy = entity.CreatedBy,
                    DateOfBirth = entity.DateOfBirth,
                    Firstname = entity.Firstname,
                    Gender = entity.Gender,
                    Lastname = entity.Lastname,
                    NumberAddress = entity.NumberAddress,
                    UpdatedAt = entity.UpdatedAt,
                    UpdatedBy = entity.UpdatedBy,
                    Voided = entity.Voided,
                    VoidedAt = entity.VoidedAt,
                    VoidedBy = entity.VoidedBy
                };

                if (entity.Skyco_Account != null)
                {
                    BE.Skyco_Account = new List<Skyco_AccountBE>();
                    foreach (Skyco_Accounts item in entity.Skyco_Account)
                    {
                        FactorySkyco_Account.GetInstance().CreateBusiness(item);
                    }
                }

                if (entity.Skyco_Address != null)
                {
                    BE.Skyco_Address = new List<Skyco_AddressBE>();
                    foreach (Skyco_Addresses item in entity.Skyco_Address)
                    {
                        Factories.FactorySkyco_Address.GetInstance().CreateBusiness(item);
                    }
                }
                if (entity.Skyco_Phone != null)
                {
                    BE.Skyco_Phone = new List<Skyco_PhoneBE>();
                    foreach (Skyco_Phones item in entity.Skyco_Phone)
                    {
                        FactorySkyco_Phone.GetInstance().CreateBusiness(item);
                    }
                }
                return BE;
            }
            return BE = new Skyco_UserBE();
        }
        #endregion

        #region CreateEntities
        public Skyco_Users CreateEntity(Skyco_UserBE BE)
        {
            Skyco_Users entity;
            if (BE != null)
            {
                entity = new Skyco_Users()
                {
                   UserId = BE.UserId,
                   Address = BE.Address,
                   CreatedAt = BE.CreatedAt,
                   CreatedBy = BE.CreatedBy,
                   DateOfBirth = BE.DateOfBirth,
                   Firstname = BE.Firstname,
                   Gender = BE.Gender,
                   Lastname = BE.Lastname,
                   NumberAddress = BE.NumberAddress,
                   UpdatedAt = BE.UpdatedAt,
                   UpdatedBy = BE.UpdatedBy,
                   Voided = BE.Voided,
                   VoidedAt  = BE.VoidedAt,
                   VoidedBy = BE.VoidedBy
                };

                if (BE.Skyco_Account != null)
                {
                    entity.Skyco_Account = new List<Skyco_Accounts>();
                    foreach (Skyco_AccountBE item in BE.Skyco_Account)
                    {
                        entity.Skyco_Account.Add(FactorySkyco_Account.GetInstance().CreateEntity(item));
                    }
                }

                if (BE.Skyco_Address != null)
                {
                    entity.Skyco_Address = new List<Skyco_Addresses>();
                    foreach (Skyco_AddressBE item in BE.Skyco_Address)
                    {
                        entity.Skyco_Address.Add(Factories.FactorySkyco_Address.GetInstance().CreateEntity(item));
                    }
                }
                if (BE.Skyco_Phone != null)
                {
                    entity.Skyco_Phone = new List<Skyco_Phones>();
                    foreach (Skyco_PhoneBE item in BE.Skyco_Phone)
                    {
                        entity.Skyco_Phone.Add(FactorySkyco_Phone.GetInstance().CreateEntity(item));
                    }
                }
                return entity;
            };
            return entity = new Skyco_Users();
        }
        #endregion
    }
}
