using BusinessEntities.BE;
using SkyCoApi.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.FactoryDTO
{
    public class FactoryProvinceDTO
    {
        #region Single
        private static FactoryProvinceDTO _factory;
        public static FactoryProvinceDTO GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryProvinceDTO();
            return _factory;
        }
        #endregion      

        #region Create DTO
        public ProvinceDTO CreateEntity(ProvinceBE be)
        {
            ProvinceDTO entity;
            if (be != null)
            {
                entity = new ProvinceDTO()
                {
                    CountryId = be.CountryId,
                    CreatedAt = be.CreatedAt,
                    CreatedBy = be.CreatedBy,
                    ProvinceId = be.ProvinceId,
                    ProvinceName = be.ProvinceName,
                    UpdatedAt = be.UpdatedAt,
                    UpdatedBy = be.UpdatedBy,
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