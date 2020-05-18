using BusinessEntities.BE;
using SkyCoApi.Models.DTO.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.FactoryDTO
{
    public class FactorySkyco_UserDTO
    {
        #region single
        private static FactorySkyco_UserDTO _Factory;

        public static FactorySkyco_UserDTO GetInstance()
        {
            if (_Factory == null)
                _Factory = new FactorySkyco_UserDTO();
            return _Factory;
        }

        #endregion

        #region CreateEntities
        public Skyco_UserDTO CreateDTO(Skyco_UserBE BE)
        {
            Skyco_UserDTO dto;
            if (BE != null)
            {
                dto = new Skyco_UserDTO()
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
                    VoidedAt = BE.VoidedAt,
                    VoidedBy = BE.VoidedBy
                };

                if (BE.Skyco_Account != null)
                {
                    dto.Skyco_Account = new List<Skyco_AccountDTO>();
                    foreach (Skyco_AccountBE item in BE.Skyco_Account)
                    {
                        FactorySkyco_AccountDTO.GetInstance().CreateDTO(item);
                    }
                }

                if (BE.Skyco_Address != null)
                {
                    dto.Skyco_Address = new List<Skyco_AddressDTO>();
                    foreach (Skyco_AddressBE item in BE.Skyco_Address)
                    {
                        FactorySkyco_AddressDTO.GetInstance().CreateDTO(item);
                    }
                }
                if (BE.Skyco_Phone != null)
                {
                    dto.Skyco_Phone = new List<Skyco_PhoneDTO>();
                    foreach (Skyco_PhoneBE item in BE.Skyco_Phone)
                    {
                        FactorySkyco_PhoneDTO.GetInstance().CreateDTO(item);
                    }
                }
                return dto;
            };
            return dto = null;
        }
        #endregion

    }
}