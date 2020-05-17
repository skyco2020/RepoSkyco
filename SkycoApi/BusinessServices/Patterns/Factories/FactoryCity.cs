using BusinessEntities.BE;
using DataModal.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactoryCity
    {
        private static FactoryCity _factory;
        public static FactoryCity GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryCity();
            return _factory;
        }

        #region Business
        public CityBE CreateBusiness(Cities entity)
        {
            CityBE be;
            if (entity != null)
            {
                be = new CityBE()
                {
                   UpdatedAt = entity.UpdatedAt,
                   UpdatedBy = entity.UpdatedBy,
                   CityId = entity.CityId,
                   CityName = entity.CityName,
                   CreatedAt = entity.CreatedAt,
                   CreatedBy = entity.CreatedBy,
                   ProvinceId = entity.ProvinceId,
                   Voided = entity.Voided,
                   VoidedAt = entity.VoidedAt,
                   VoidedBy = entity.VoidedBy
                };               

                return be;
            }
            return be = new CityBE();
        }
        #endregion

        #region Entity
        public Cities CreateEntity(CityBE be)
        {
            Cities entity;
            if (be != null)
            {
                entity = new Cities()
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
                return entity;

            }
            return entity = new Cities();
        }
        #endregion
    }
}
