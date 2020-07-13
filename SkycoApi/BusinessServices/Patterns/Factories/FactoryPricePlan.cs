using BusinessEntities.BE;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactoryPricePlan
    {
        private static FactoryPricePlan _factory;
        public static FactoryPricePlan GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryPricePlan();
            return _factory;
        }

        #region Business
        public PricePlanBE CreateBusiness(Price entity)
        {
            PricePlanBE be;
            if (entity != null)
            {
                be = new PricePlanBE()
                {
                    Active = entity.Active,
                    BillingScheme = entity.BillingScheme,
                    Created = entity.Created,
                    Currency = entity.Currency,
                    Deleted = entity.Deleted,
                    Id = entity.Id,
                    Livemode = entity.Livemode,
                    LookupKey = entity.LookupKey,
                    Metadata = entity.Metadata,
                    Nickname  = entity.Nickname,
                    ProductId = entity.ProductId,
                    TiersMode = entity.TiersMode,
                    Type = entity.Type,
                    Object = entity.Object,
                    UnitAmount = entity.UnitAmount,
                    UnitAmountDecimal = entity.UnitAmountDecimal
                };

                return be;
            }
            return be = null;
        }
        #endregion

        #region Entity
        public Price CreateEntity(PricePlanBE be)
        {
            Price entity;
            if (be != null)
            {
                entity = new Price()
                {
                    Active = be.Active,
                    BillingScheme = be.BillingScheme,
                    Created = be.Created,
                    Currency = be.Currency,
                    Deleted = be.Deleted,
                    Id = be.Id,
                    Livemode = be.Livemode,
                    LookupKey = be.LookupKey,
                    Metadata = be.Metadata,
                    Nickname = be.Nickname,
                    ProductId = be.ProductId,
                    TiersMode = be.TiersMode,
                    Type = be.Type,
                    Object = be.Object,
                    UnitAmount = be.UnitAmount,
                    UnitAmountDecimal = be.UnitAmountDecimal
                };               
                return entity;

            }
            return entity = null;
        }
        #endregion
    }
}
