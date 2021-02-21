using BusinessEntities.BE;
using DataModal.DataClasses;
using StripeServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeServices.Patterns.Factories
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
                    idProduct = entity.idProduct,
                    idplanstripe = entity.idplanstripe,
                    Description = entity.Description,
                    Id = entity.PlanId,
                    Motive = entity.Motive,
                    TypePlan = entity.TypePlan,
                    PlanDate = entity.PlanDate,
                    Price = (Int64)entity.Price,
                    state = entity.state,
                    Products = entity.Products != null ? FactoryProduct.GetInstance().CreateBusiness(entity.Products) : null,
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
                    AccountId = be.AccountId,
                    idProduct = be.idProduct,
                    idplanstripe = be.idplanstripe,
                    Description = be.Description,
                    PlanId = be.Id,
                    Motive = be.Motive,
                    TypePlan = be.TypePlan,
                    PlanDate = be.PlanDate,
                    Price = be.Price,
                    state = be.state,
                };
                return entity;

            }
            return entity = null;
        }
        #endregion
    }
}
