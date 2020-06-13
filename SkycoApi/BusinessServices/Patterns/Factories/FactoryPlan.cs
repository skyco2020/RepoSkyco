using BusinessEntities.BE;
using DataModal.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactoryPlan
    {
        private static FactoryPlan _factory;
        public static FactoryPlan GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryPlan();
            return _factory;
        }

        #region Business
        public PlanBE CreateBusiness(Plans entity)
        {
            PlanBE be;
            if (entity != null)
            {
                be = new PlanBE()
                {
                    AccountId = entity.AccountId,
                    Description = entity.Description,
                    Id = entity.PlanId,
                    PlanDate = entity.PlanDate,
                    Price = entity.Price,
                    state = entity.state,
                    Accounts = entity.Accounts != null ? FactorySkyco_Account.GetInstance().CreateBusiness(entity.Accounts) : null
                };

                return be;
            }
            return be = null;
        }
        #endregion

        #region Entity
        public Plans CreateEntity(PlanBE be)
        {
            Plans entity;
            if (be != null)
            {
                entity = new Plans()
                {
                    
                };
                return entity;

            }
            return entity = null;
        }
        #endregion
    }
}
