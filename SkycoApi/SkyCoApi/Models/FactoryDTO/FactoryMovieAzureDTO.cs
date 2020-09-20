using BusinessEntities.BE;
using SkyCoApi.Models.DTO.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.FactoryDTO
{
    public class FactoryMovieAzureDTO
    {
        private static FactoryMovieAzureDTO _factory;
        public static FactoryMovieAzureDTO GetInstance()
        {
            if (_factory == null)
                _factory = new FactoryMovieAzureDTO();
            return _factory;
        }

        #region Entity
        public MovieAzureDTO CreateDTO(MovieBE be)
        {
            MovieAzureDTO entity;
            if (be != null)
            {
                entity = new MovieAzureDTO()
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