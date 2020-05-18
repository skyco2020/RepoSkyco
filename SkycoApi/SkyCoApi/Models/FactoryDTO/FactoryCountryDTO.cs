using BusinessEntities.BE;
using SkyCoApi.Models.DTO.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.FactoryDTO
{
    public class FactoryCountryDTO
    {
        private static FactoryCountryDTO _factory;
        public static FactoryCountryDTO GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryCountryDTO();
            return _factory;
        }

        #region Business
        public CountryDTO CreateDTO(CountryBE be)
        {
            CountryDTO dto;
            if (be != null)
            {
                dto = new CountryDTO()
                {
                    CountryCode = be.CountryCode,
                    CountryId = be.CountryId,
                    CountryName = be.CountryName,
                    CreatedAt = be.CreatedAt,
                    CreatedBy = be.CreatedBy,
                    UpdatedAt = be.UpdatedAt,
                    UpdatedBy = be.UpdatedBy,
                    Voided = be.Voided,
                    VoidedAt = be.VoidedAt,
                    VoidedBy = be.VoidedBy
                };                
                return dto;
            }
            return dto = new CountryDTO();
        }
        #endregion
    }
}