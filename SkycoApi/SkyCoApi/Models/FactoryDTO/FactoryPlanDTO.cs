using BusinessEntities.BE;
using SkyCoApi.Models.DTO.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.FactoryDTO
{
    public class FactoryPlanDTO
    {
        private static FactoryPlanDTO _factory;
        public static FactoryPlanDTO GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryPlanDTO();
            return _factory;
        }       
        #region CreateDTO
        public PlanDTO CreateDTO(PlanBE be)
        {
            PlanDTO dto;
            if (be != null)
            {
                dto = new PlanDTO()
                {
                    AccountId = be.AccountId,
                    Description = be.Description,
                    PlanId = be.Id,
                    TypePlan = be.TypePlan,
                    PlanDate = be.PlanDate,
                    Price = be.Price,
                    state = be.state,
                    Accounts = be.Accounts != null ? FactorySkyco_AccountDTO.GetInstance().CreateDTO(be.Accounts) : null
                };
                return dto;

            }
            return dto = null;
        }
        #endregion
    }
}