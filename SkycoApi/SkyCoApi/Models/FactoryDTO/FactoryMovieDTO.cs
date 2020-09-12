using BusinessEntities.BE;
using SkyCoApi.Models.DTO.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.FactoryDTO
{
    public class FactoryMovieDTO
    {
        private static FactoryMovieDTO _factory;
        public static FactoryMovieDTO GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryMovieDTO();
            return _factory;
        }      

        #region Entity
        public MovieDTO CreateDTO(SearchBE be)
        {
            MovieDTO entity;
            if (be != null)
            {
                entity = new MovieDTO()
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