using BusinessEntities.BE;
using SkyCoApi.Models.DTO.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.FactoryDTO
{
    public class FactoryPerfilDTO
    {
        private static FactoryPerfilDTO _factory;
        public static FactoryPerfilDTO GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryPerfilDTO();
            return _factory;
        }


        #region Entity
        public PerfilDTO CreateDTO(PerfilBE be)
        {
            PerfilDTO entity;
            if (be != null)
            {
                entity = new PerfilDTO()
                {
                    idPerfil = be.idPerfil,
                    AccountId = be.AccountId,
                    complete = be.complete,
                    name = be.name,
                    passperfil = be.passperfil,
                    typeperfil = be.typeperfil,
                    state = be.state
                };

                return entity;
            }
            return null;
        }

        #endregion
    }
}