using BusinessEntities.BE;
using SkyCoApi.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.FactoryDTO
{
    public class FactorySkyco_AddressDTO
    {
        private static FactorySkyco_AddressDTO _factory;
        public static FactorySkyco_AddressDTO GetInstance()
        {
            if (_factory == null)
                _factory = new FactorySkyco_AddressDTO();
            return _factory;
        }     

        #region Create DTO
        public Skyco_AddressDTO CreateDTO(Skyco_AddressBE be)
        {
            Skyco_AddressDTO entity;
            if (be != null)
            {
                entity = new Skyco_AddressDTO()
                {
                    VoidedBy = be.VoidedBy,
                    VoidedAt = be.VoidedAt,
                    Voided = be.Voided,
                    UpdatedBy = be.UpdatedBy,
                    UpdatedAt = be.UpdatedAt,
                    AddressId = be.AddressId,
                    CreatedAt = be.CreatedAt,
                    CreatedBy = be.CreatedBy,
                    UserId = be.UserId

                };
                return entity;
            }
            return null;
        }
        #endregion
    }
}