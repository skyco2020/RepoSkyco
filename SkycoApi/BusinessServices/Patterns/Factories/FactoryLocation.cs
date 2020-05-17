using BusinessEntities.BE;
using DataModal.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactoryLocation
    {
        #region Single
        private static FactoryLocation _factory;
        public static FactoryLocation GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryLocation();
            return _factory;
        }
        #endregion

        #region Create Business
        public LocationBE CreateBusiness(Locations entity)
        {
            LocationBE be;
            if (entity != null)
            {
                be = new LocationBE()
                {
                   VoidedBy = entity.VoidedBy,
                   VoidedAt = entity.VoidedAt,
                   Voided = entity.Voided,
                   ProvinceId = entity.ProvinceId,
                   CreatedBy = entity.CreatedBy,
                   CreatedAt = entity.CreatedAt,
                   CityId = entity.CityId,
                   AddressName = entity.AddressName,
                   AddressNumber = entity.AddressNumber,
                   Appartement = entity.Appartement,
                   CountryId = entity.CountryId,
                   Latitude = entity.Latitude,
                   LocationId = entity.LocationId,
                   Longitude = entity.Longitude,
                   UpdatedAt = entity.UpdatedAt,
                   UpdatedBy = entity.UpdatedBy,
                   Country = entity.Country != null ? Factories.FactoryCountry.GetInstance().CreateBusiness(entity.Country) : null,
                   City = entity.City != null ? Factories.FactoryCity.GetInstance().CreateBusiness(entity.City) : null,
                   Provinces = entity.Provinces != null ? Factories.FactoryProvince.GetInstance().CreateBusiness(entity.Provinces) : null
                };
                return be;
            }
            return be = new LocationBE();
        }
        #endregion

        #region Create Business
        public Locations CreateEntity(LocationBE be)
        {
            Locations entity;
            if (be != null)
            {
                entity = new Locations()
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
            return entity = new Locations();
        }
        #endregion
    }
}
