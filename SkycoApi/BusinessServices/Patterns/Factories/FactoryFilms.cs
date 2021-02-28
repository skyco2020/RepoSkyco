using BusinessEntities.BE;
using StreamingVideo.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactoryFilms
    {
        private static FactoryFilms _factory;
        public static FactoryFilms GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryFilms();
            return _factory;
        }

        #region Business
        public MovieBE CreateBusiness(Movie entity)
        {
            MovieBE be;
            if (entity != null)
            {
                be = new MovieBE()
                {
                    urlmovie = entity.urlmovie,
                    total = entity.total,
                    totalHits = entity.totalHits
                };
                if (entity.hits.Count > 0)
                {
                    be.hits = new List<HitBE>();
                    foreach (var item in entity.hits)
                    {
                        be.hits.Add(FactoryHit.GetInstance().CreateBusiness(item));
                    }
                }
                return be;
            }
            return null;
        }
        #endregion

        #region Entity
        public Movie CreateEntity(MovieBE be)
        {
            Movie entity;
            if (be != null)
            {
                entity = new Movie()
                {
                    urlmovie = be.urlmovie,
                };

                return entity;
            }
            return null;
        }

        #endregion
    }
}
