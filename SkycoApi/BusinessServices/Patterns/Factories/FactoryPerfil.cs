using BusinessEntities.BE;
using DataModal.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactoryPerfil
    {
        private static FactoryPerfil _factory;
        public static FactoryPerfil GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryPerfil();
            return _factory;
        }

        #region Business
        public PerfilBE CreateBusiness(Perfils entity)
        {
            PerfilBE be;
            if (entity != null)
            {
                be = new PerfilBE()
                {
                    idPerfil = entity.idPerfil,
                    AccountId = entity.AccountId,
                    complete = entity.complete,
                    name = entity.name,
                    typeperfil = entity.typeperfil,
                    state = entity.state
                };
               
                return be;
            }
            return null;
        }
        #endregion

        #region Entity
        public Perfils CreateEntity(PerfilBE be)
        {
            Perfils entity;
            if (be != null)
            {
                entity = new Perfils()
                {
                    idPerfil = be.idPerfil,
                    AccountId = be.AccountId,
                    complete = be.complete,
                    name = be.name,
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
