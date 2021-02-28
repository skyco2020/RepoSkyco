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
    public class FactoryHit
    {
        #region Single
        private static FactoryHit _factory;
        public static FactoryHit GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryHit();
            return _factory;
        }
        #endregion

        #region Create Business
        public HitBE CreateBusiness(Hits entity)
        {
            HitBE be;
            if (entity != null)
            {
                be = new HitBE()
                {
                    duration = entity.duration,
                    id = entity.id,
                    pageURL = entity.pageURL,
                    picture_id = entity.picture_id,
                    tags = entity.tags,
                    type = entity.type,
                    userImageURL = entity.userImageURL,
                    views = entity.views,
                    videos = entity.videos != null ? FactoryVideo.GetInstance().CreateBusiness(entity.videos): null
                };
                return be;
            }
            return null;
        }
        #endregion

        #region Create Entity
        public Hits CreateEntity(HitBE be)
        {
            Hits entity;
            if (be != null)
            {
                entity = new Hits()
                {
                    duration = be.duration,
                    id = be.id,
                    pageURL = be.pageURL,
                    picture_id = be.picture_id,
                    tags = be.tags,
                    type = be.type,
                    userImageURL = be.userImageURL,
                    views = be.views,
                    videos = be.videos != null ? FactoryVideo.GetInstance().CreateEntity(be.videos) : null
                };
                return entity;
            }
            return null;
        }
        #endregion
    }
}
