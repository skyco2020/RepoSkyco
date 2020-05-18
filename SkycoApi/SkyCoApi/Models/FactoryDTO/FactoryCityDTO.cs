using BusinessEntities.BE;
using SkyCoApi.Models.DTO.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.FactoryDTO
{
    public class FactoryCityDTO
    {
        private static FactoryCityDTO _factory;
        public static FactoryCityDTO GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryCityDTO();
            return _factory;
        }

      #region CreateDTO
        public CityDTO CreateDTO(CityBE be)
        {
            CityDTO dto;
            if (be != null)
            {
                dto = new CityDTO()
                {
                    UpdatedAt = be.UpdatedAt,
                    UpdatedBy = be.UpdatedBy,
                    CityId = be.CityId,
                    CityName = be.CityName,
                    CreatedAt = be.CreatedAt,
                    CreatedBy = be.CreatedBy,
                    ProvinceId = be.ProvinceId,
                    Voided = be.Voided,
                    VoidedAt = be.VoidedAt,
                    VoidedBy = be.VoidedBy
                };
                return dto;

            }
            return null;
        }
        #endregion
    }
}