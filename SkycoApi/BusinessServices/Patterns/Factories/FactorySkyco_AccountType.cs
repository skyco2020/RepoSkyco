using BusinessEntities.BE;
using DataModal.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactorySkyco_AccountType
    {
        #region Single
        private static FactorySkyco_AccountType _factory;
        public static FactorySkyco_AccountType GetInstance()
        {
            if (_factory == null)
                _factory = new FactorySkyco_AccountType();
            return _factory;
        }
        #endregion

        #region Create Business
        public Skyco_AccountTypeBE CreateBusiness(Skyco_AccountTypes entity)
        {
            Skyco_AccountTypeBE be;
            if (entity != null)
            {
                be = new Skyco_AccountTypeBE()
                {
                    VoidedBy = entity.VoidedBy,
                    VoidedAt = entity.VoidedAt,
                    Voided = entity.Voided,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedAt = entity.UpdatedAt,
                    AccountTypeId = entity.AccountTypeId,
                    AccountTypeName = entity.AccountTypeName,
                    CreatedAt = entity.CreatedAt,
                    CreatedBy = entity.CreatedBy
                };
                
                return be;
            }
            return null;
        }
        #endregion

        #region Create Entity
        public Skyco_AccountTypes CreateEntity(Skyco_AccountTypeBE be)
        {
            Skyco_AccountTypes entity;
            if (be != null)
            {
                entity = new Skyco_AccountTypes()
                {
                    VoidedBy = be.VoidedBy,
                    VoidedAt = be.VoidedAt,
                    Voided = be.Voided,
                    UpdatedBy = be.UpdatedBy,
                    UpdatedAt = be.UpdatedAt,
                    AccountTypeId = be.AccountTypeId,
                    AccountTypeName = be.AccountTypeName,
                    CreatedAt = be.CreatedAt,
                    CreatedBy = be.CreatedBy
                };
                return entity;
            }
            return entity = new Skyco_AccountTypes();
        }
        #endregion
    }
}
