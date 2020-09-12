using BusinessEntities.BE;
using StreamingVideo.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Patterns.Factories
{
    public class FactoryMovies
    {
        private static FactoryMovies _factory;
        public static FactoryMovies GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryMovies();
            return _factory;
        }

        #region Business
        public SearchBE CreateBusiness(Search entity)
        {
            SearchBE be;
            if (entity != null)
            {
                be = new SearchBE()
                {
                    imdbID = entity.imdbID,
                    Poster = entity.Poster,
                    Title = entity.Title,
                    Type = entity.Type,
                    Year = entity.Year
                };
                return be;
            }
            return null;
        }
        #endregion

        #region Entity
        public Search CreateEntity(SearchBE be)
        {
            Search entity;
            if (be != null)
            {
                entity = new Search()
                {
                    imdbID = be.imdbID,
                    Poster = be.Poster,
                    Title = be.Title,
                    Type = be.Type,
                    Year = be.Year
                };
                
                return entity;
            }
            return null;
        }

        #endregion
    }
}
