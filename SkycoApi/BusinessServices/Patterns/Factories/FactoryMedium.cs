using BusinessEntities.BE;
using StreamingVideo.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactoryMedium
    {
        #region Single
        private static FactoryMedium _factory;
        public static FactoryMedium GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryMedium();
            return _factory;
        }
        #endregion

        #region Create Business
        public MediumBE CreateBusiness(Medium entity)
        {
            MediumBE be;
            if (entity != null)
            {
                be = new MediumBE()
                {
                    height = entity.height,
                    profile_id = entity.profile_id,
                    size = entity.size,
                    url = entity.url,
                    width = entity.width
                };
                return be;
            }
            return null;
        }
        #endregion

        #region Create Entity
        public Medium CreateEntity(MediumBE be)
        {
            Medium entity;
            if (be != null)
            {
                entity = new Medium()
                {
                    height = be.height,
                    profile_id = be.profile_id,
                    size = be.size,
                    url = be.url,
                    width = be.width
                };
                return entity;
            }
            return null;
        }
        #endregion
    }
}
