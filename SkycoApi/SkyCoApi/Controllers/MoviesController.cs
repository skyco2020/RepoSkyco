using BusinessEntities.BE;
using BusinessServices.Interfaces;
using Resolver.Enumerations;
using SkyCoApi.Models.DTO.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SkyCoApi.Controllers
{
    public class MoviesController : ApiController
    {
        #region Single
        private IMovieServices _services;
        public MoviesController(IMovieServices services)
        {
            _services = services;
        }
        #endregion

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetAll(Int32 state = (Int32)StateEnum.Activated)
        {
            var count = 0;
            IQueryable<SearchBE> query = _services.GetAll(state).AsQueryable();
            List<MovieDTO> listdoto = new List<MovieDTO>();
            foreach (SearchBE item in query)
            {
                listdoto.Add(Models.FactoryDTO.FactoryMovieDTO.GetInstance().CreateDTO(item));
            }           
            return Ok(listdoto);
        }

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [Route("api/GetAllMovie")]
        public async Task<IHttpActionResult> GetAllMovie()
        {
            var count = 0;
            IQueryable<MovieBE> query = _services.GetListMovie().AsQueryable();
            List<MovieAzureDTO> listdoto = new List<MovieAzureDTO>();
            foreach (MovieBE item in query)
            {
                listdoto.Add(Models.FactoryDTO.FactoryMovieAzureDTO.GetInstance().CreateDTO(item));
            }
            return Ok(listdoto);
        }

    }
}
