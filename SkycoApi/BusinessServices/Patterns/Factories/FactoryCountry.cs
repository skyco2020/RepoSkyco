using BusinessEntities.BE;
using DataModal.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
   public class FactoryCountry
    {
        private static FactoryCountry _factory;
        public static FactoryCountry GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryCountry();
            return _factory;
        }

        #region Business
        public CountryBE CreateBusiness(Countries entity)
        {
            CountryBE be;
            if (entity != null)
            {
                be = new CountryBE()
                {
                    CountryCode = entity.CountryCode,
                    CountryId = entity.CountryId,
                    CountryName = entity.CountryName,
                    CreatedAt = entity.CreatedAt,
                    CreatedBy = entity.CreatedBy,
                    UpdatedAt = entity.UpdatedAt,
                    UpdatedBy = entity.UpdatedBy,
                    Voided = entity.Voided,
                    VoidedAt = entity.VoidedAt,
                    VoidedBy = entity.VoidedBy
                };
                be.Province = new List<ProvinceBE>();
                if (entity.Provinces.Count > 0)
                {
                    foreach (var item in entity.Provinces)
                    {
                        be.Province.Add(FactoryProvince.GetInstance().CreateBusiness(item));
                    }
                }
                return be;
            }
            return be = new CountryBE();
        }
        #endregion

        #region Entity
        public Countries CreateEntity(CountryBE be)
        {
            Countries entity;
            if (be != null)
            {
                entity = new Countries()
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
                if (be.Province != null)
                {
                    entity.Provinces = new List<Provinces>();
                    foreach (ProvinceBE item in be.Province)
                    {
                        entity.Provinces.Add(FactoryProvince.GetInstance().CreateEntity(item));
                    }
                }
                return entity;
            }
            return entity = new Countries();
        }

        #endregion
    }
}
