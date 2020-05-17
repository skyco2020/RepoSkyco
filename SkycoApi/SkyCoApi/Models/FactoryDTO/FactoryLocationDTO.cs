using BusinessEntities.BE;
using SkyCoApi.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.FactoryDTO
{
    public class FactoryLocationDTO
    {
        #region Single
        private static FactoryLocationDTO _factory;
        public static FactoryLocationDTO GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryLocationDTO();
            return _factory;
        }
        #endregion      

        #region Create DTO
        public LocationDTO CreateDTO(LocationBE be)
        {
            LocationDTO entity;
            if (be != null)
            {
                entity = new LocationDTO()
                {
                    AddressName = be.AddressName,
                    AddressNumber = be.AddressNumber,
                    Appartement = be.Appartement,
                    CityId = be.CityId,
                    CountryId = be.CountryId,
                    CreatedAt = be.CreatedAt,
                    CreatedBy = be.CreatedBy,
                    Latitude = be.Latitude,
                    LocationId = be.LocationId,
                    Longitude = be.Longitude,
                    ProvinceId = be.ProvinceId,
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