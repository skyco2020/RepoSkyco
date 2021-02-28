using BusinessEntities.BE;
using StreamingVideo.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactorySmall
    {
        #region Single
        private static FactorySmall _factory;
        public static FactorySmall GetInstance()
        {
            if (_factory == null)
                _factory = new FactorySmall();
            return _factory;
        }
        #endregion

        #region Create Business
        public SmallBE CreateBusiness(Small entity)
        {
            SmallBE be;
            if (entity != null)
            {
                be = new SmallBE()
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
        public Small CreateEntity(SmallBE be)
        {
            Small entity;
            if (be != null)
            {
                entity = new Small()
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
