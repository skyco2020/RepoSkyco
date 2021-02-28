using BusinessEntities.BE;
using StreamingVideo.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactoryTiny
    {
        #region Single
        private static FactoryTiny _factory;
        public static FactoryTiny GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryTiny();
            return _factory;
        }
        #endregion

        #region Create Business
        public TinyBE CreateBusiness(Tiny entity)
        {
            TinyBE be;
            if (entity != null)
            {
                be = new TinyBE()
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
        public Tiny CreateEntity(TinyBE be)
        {
            Tiny entity;
            if (be != null)
            {
                entity = new Tiny()
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
