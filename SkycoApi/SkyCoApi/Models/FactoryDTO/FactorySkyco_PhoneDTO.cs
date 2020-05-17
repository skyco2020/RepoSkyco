using BusinessEntities.BE;
using SkyCoApi.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.FactoryDTO
{
    public class FactorySkyco_PhoneDTO
    {
        private static FactorySkyco_PhoneDTO _factory;
        public static FactorySkyco_PhoneDTO GetInstance()
        {
            if (_factory == null)
                _factory = new FactorySkyco_PhoneDTO();
            return _factory;
        }    

        #region Create DTO
        public Skyco_PhoneDTO CreateDTO(Skyco_PhoneBE be)
        {
            Skyco_PhoneDTO entity;
            if (be != null)
            {
                entity = new Skyco_PhoneDTO()
                {
                    CreatedAt = be.CreatedAt,
                    CreatedBy = be.CreatedBy,
                    IdPhone = be.IdPhone,
                    PhoneNumber = be.PhoneNumber,
                    Preferred = be.Preferred,
                    UpdatedAt = be.UpdatedAt,
                    UpdatedBy = be.UpdatedBy,
                    UserId = be.UserId,
                    Voided = be.Voided,
                    VoidedAt = be.VoidedAt,
                    VoidedBy = be.VoidedBy
                };
                return entity;
            }
            return null;
        }
        #endregion
    }
}