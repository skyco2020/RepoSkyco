using BusinessEntities.BE;
using DataModal.DataClasses;
using StreamingVideo.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactoryLarge
    {
        #region Single
        private static FactoryLarge _factory;
        public static FactoryLarge GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryLarge();
            return _factory;
        }
        #endregion

        #region Create Business
        public LargeBE CreateBusiness(Large entity)
        {
            LargeBE be;
            if (entity != null)
            {
                be = new LargeBE()
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
        public Large CreateEntity(LargeBE be)
        {
            Large entity;
            if (be != null)
            {
                entity = new Large()
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
