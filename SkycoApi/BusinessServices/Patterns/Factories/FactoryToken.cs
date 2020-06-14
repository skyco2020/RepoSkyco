using BusinessEntities.BE;
using DataModal.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactoryToken
    {
        private static FactoryToken _factory;
        public static FactoryToken GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryToken();
            return _factory;
        }

        #region Business
        public TokenBE CreateBusiness(Tokens entity)
        {
            TokenBE be;
            if (entity != null)
            {
                be = new TokenBE()
                {
                   client_ip = entity.client_ip,
                   created = entity.created,
                   id = entity.id,
                   Id = entity.idtoken,
                   livemode = entity.livemode,
                   objectcart = entity.objectcart,
                   state = entity.state,
                   type = entity.type,
                   used = entity.used
                };

                return be;
            }
            return be = null;
        }
        #endregion

        #region Entity
        public Tokens CreateEntity(TokenBE be)
        {
            Tokens entity;
            if (be != null)
            {
                entity = new Tokens()
                {
                    client_ip = be.client_ip,
                    created = be.created,
                    id = be.id,
                    idtoken = be.Id,
                    livemode = be.livemode,
                    objectcart = be.objectcart,
                    state = be.state,
                    type = be.type,
                    used = be.used
                };

                if (be.cards != null)
                {
                    entity.cards = new List<Cards>();

                    foreach (CardBE item in be.cards)
                    {
                        entity.cards.Add(FactoryCard.GetInstance().CreateEntity(item));
                    }
                }
                return entity;

            }
            return entity = null;
        }
        #endregion
    }
}
