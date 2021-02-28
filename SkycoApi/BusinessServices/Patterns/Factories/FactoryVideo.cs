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
    public class FactoryVideo
    {
        #region Single
        private static FactoryVideo _factory;
        public static FactoryVideo GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryVideo();
            return _factory;
        }
        #endregion

        #region Create Business
        public VideoBE CreateBusiness(Videos entity)
        {
            VideoBE be;
            if (entity != null)
            {
                be = new VideoBE()
                {
                    large = FactoryLarge.GetInstance().CreateBusiness(entity.large),
                    small = FactorySmall.GetInstance().CreateBusiness(entity.small),
                    medium = FactoryMedium.GetInstance().CreateBusiness(entity.medium),
                    tiny = FactoryTiny.GetInstance().CreateBusiness(entity.tiny)
                };
                return be;
            }
            return null;
        }
        #endregion

        #region Create Entity
        public Videos CreateEntity(VideoBE be)
        {
            Videos entity;
            if (be != null)
            {
                entity = new Videos()
                {
                    large = FactoryLarge.GetInstance().CreateEntity(be.large),
                    small = FactorySmall.GetInstance().CreateEntity(be.small),
                    medium = FactoryMedium.GetInstance().CreateEntity(be.medium),
                    tiny = FactoryTiny.GetInstance().CreateEntity(be.tiny)
                };
                return entity;
            }
            return null;
        }
        #endregion
    }
}
