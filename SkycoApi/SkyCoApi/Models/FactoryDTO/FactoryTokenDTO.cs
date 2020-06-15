using BusinessEntities.BE;
using SkyCoApi.Models.DTO.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.FactoryDTO
{
    public class FactoryTokenDTO
    {
        private static FactoryTokenDTO _factory;
        public static FactoryTokenDTO GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryTokenDTO();
            return _factory;
        }

        #region CreateDTO
        public TokenDTO CreateDTO(TokenBE be)
        {
            TokenDTO entity;
            if (be != null)
            {
                entity = new TokenDTO()
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
                    entity.cards = new List<CardDTO>();

                    foreach (CardBE item in be.cards)
                    {
                        entity.cards.Add(FactoryCardDTO.GetInstance().CreateDTO(item));
                    }
                }
                return entity;

            }
            return null;
        }
        #endregion
    }
}