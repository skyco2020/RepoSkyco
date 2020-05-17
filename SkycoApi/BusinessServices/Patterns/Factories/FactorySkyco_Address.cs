using BusinessEntities.BE;
using DataModal.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactorySkyco_Address
    {
        private static FactorySkyco_Address _factory;
        public static FactorySkyco_Address GetInstance()
        {
            if (_factory == null)
                _factory = new FactorySkyco_Address();
            return _factory;
        }

        #region Business
        public Skyco_AddressBE CreateBusiness(Skyco_Addresses entity)
        {
            Skyco_AddressBE be;
            if (entity != null)
            {
                be = new Skyco_AddressBE()
                {
                    VoidedBy = entity.VoidedBy,
                    VoidedAt = entity.VoidedAt,
                    Voided = entity.Voided,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedAt = entity.UpdatedAt,
                    AddressId = entity.AddressId,
                    CreatedAt = entity.CreatedAt,
                    CreatedBy = entity.CreatedBy,
                    UserId = entity.UserId
                };
                return be;
            }
            return null;
        }
        #endregion

        #region Entity
        public Skyco_Addresses CreateEntity(Skyco_AddressBE be)
        {
            Skyco_Addresses entity;
            if (be != null)
            {
                entity = new Skyco_Addresses()
                {
                    VoidedBy = be.VoidedBy,
                    VoidedAt = be.VoidedAt,
                    Voided = be.Voided,
                    UpdatedBy = be.UpdatedBy,
                    UpdatedAt = be.UpdatedAt,
                    AddressId = be.AddressId,
                    CreatedAt = be.CreatedAt,
                    CreatedBy = be.CreatedBy,
                    UserId = be.UserId

                };
                return entity;
            }
            return entity = new Skyco_Addresses();
        }
        #endregion
    }
}
