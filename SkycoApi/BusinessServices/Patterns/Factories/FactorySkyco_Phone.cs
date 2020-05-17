using BusinessEntities.BE;
using DataModal.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactorySkyco_Phone
    {
        private static FactorySkyco_Phone _factory;
        public static FactorySkyco_Phone GetInstance()
        {
            if (_factory == null)
                _factory = new FactorySkyco_Phone();
            return _factory;
        }

        #region Business
        public Skyco_PhoneBE CreateBusiness(Skyco_Phones entity)
        {
            Skyco_PhoneBE be;
            if (entity != null)
            {
                be = new Skyco_PhoneBE()
                {
                    CreatedAt = entity.CreatedAt,
                    CreatedBy = entity.CreatedBy,
                    IdPhone = entity.IdPhone,
                    PhoneNumber = entity.PhoneNumber,
                    Preferred = entity.Preferred,
                    UpdatedAt = entity.UpdatedAt,
                    UpdatedBy = entity.UpdatedBy,
                    UserId = entity.UserId,
                    Voided = entity.Voided,
                    VoidedAt = entity.VoidedAt,
                    VoidedBy = entity.VoidedBy
                };
                return be;
            }
            return be = new Skyco_PhoneBE();
        }
        #endregion

        #region Entity
        public Skyco_Phones CreateEntity(Skyco_PhoneBE be)
        {
            Skyco_Phones entity;
            if (be != null)
            {
                entity = new Skyco_Phones()
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
            return entity = new Skyco_Phones();
        }
        #endregion
    }
}
