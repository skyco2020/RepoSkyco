using BusinessEntities.BE;
using SkyCoApi.Models.DTO.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.FactoryDTO
{
    public class FactorySkyco_AccountTypeDTO
    {
        #region Single
        private static FactorySkyco_AccountTypeDTO _factory;
        public static FactorySkyco_AccountTypeDTO GetInstance()
        {
            if (_factory == null)
                _factory = new FactorySkyco_AccountTypeDTO();
            return _factory;
        }
        #endregion
       
        #region Create DTO
        public Skyco_AccountTypeDTO CreateDTO(Skyco_AccountTypeBE be)
        {
            Skyco_AccountTypeDTO entity;
            if (be != null)
            {
                entity = new Skyco_AccountTypeDTO()
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
            return null;
        }
        #endregion
    }
}