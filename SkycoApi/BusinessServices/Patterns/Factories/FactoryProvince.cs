using BusinessEntities.BE;
using DataModal.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactoryProvince
    {
        #region Single
        private static FactoryProvince _factory;
        public static FactoryProvince GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryProvince();
            return _factory;
        }
        #endregion

        #region Create Business
        public ProvinceBE CreateBusiness(Provinces entity)
        {
            ProvinceBE be;
            if (entity != null)
            {
                be = new ProvinceBE()
                {
                   CountryId = entity.CountryId,
                   CreatedAt = entity.CreatedAt,
                   CreatedBy = entity.CreatedBy,
                   ProvinceId = entity.ProvinceId,
                   ProvinceName = entity.ProvinceName,
                   UpdatedAt = entity.UpdatedAt,
                   UpdatedBy = entity.UpdatedBy,
                   Voided = entity.Voided,
                   VoidedAt = entity.VoidedAt,
                   VoidedBy = entity.VoidedBy
                };
                be.City = new List<CityBE>();
                if (entity.City.Count > 0)
                {
                    foreach (var item in entity.City)
                    {
                        be.City.Add(FactoryCity.GetInstance().CreateBusiness(item));
                    }
                }
                return be;
            }
            return be = new ProvinceBE();
        }
        #endregion

        #region Create Entity
        public Provinces CreateEntity(ProvinceBE be)
        {
            Provinces entity;
            if (be != null)
            {
                entity = new Provinces()
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
                if (be.City != null)
                {
                    entity.City = new List<Cities>();
                    foreach (CityBE item in be.City)
                    {
                        entity.City.Add(FactoryCity.GetInstance().CreateEntity(item));
                    }
                }
                return entity;
            }
            return entity = new Provinces();
        }
        #endregion
    }
}
