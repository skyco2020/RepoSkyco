using BusinessEntities.BE;
using BusinessServices.Interfaces;
using SkyCoApi.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace SkyCoApi.Controllers
{
    public class CountriesController : ApiController
    {
        #region Single
        private ICountryServices _services;
        public CountriesController(ICountryServices services)
        {
            _services = services;
        }
        #endregion

        public async Task<IHttpActionResult> GetCountry(Int32 state = 1,int page = 1, Int32 top = 5, String orderby = "CountryId", String ascending = "asc")
        {
            var count = 0;
            IQueryable<CountryBE> query = _services.GetAll(state,page, top, orderby, ascending, ref count).AsQueryable();
            List<CountryDTO> listdoto = new List<CountryDTO>();
            foreach (CountryBE item in query)
            {
                listdoto.Add(Models.FactoryDTO.FactoryCountryDTO.GetInstance().CreateDTO(item));
            }
            return Ok(listdoto);
        }

        public async Task<IHttpActionResult> GetById(long id)
        {
            CountryBE be = _services.GetById(id);
            CountryDTO doto = new CountryDTO();
            if (be == null)
                return NotFound();
            doto = Models.FactoryDTO.FactoryCountryDTO.GetInstance().CreateDTO(be);
            return Ok(doto);
        }

        public async Task<IHttpActionResult> PostCountry(CountryBE be)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _services.Create(be);
            return Created(new Uri(Url.Link("DefaultApi", new { Id = be.CountryId })), be);
        }


        public async Task<IHttpActionResult> PutCountry(CountryBE bE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _services.Update(bE);
            return Ok();
        }

        public async Task<IHttpActionResult> DeleteCountry(Int64 id)
        {
            ClaimsIdentity identityClaims = (ClaimsIdentity)User.Identity;
            _services.Delete(id, identityClaims.FindFirst("username").Value);
            return Ok();
        }

        #region Route
        [AllowAnonymous]
        [Route("api/Countries/CountryAuto")]
        public async Task<IHttpActionResult> PostCountryAuto(CountryBE be)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Int64 id = _services.CreateCountryAuto(be);
            if (id != 0)
                be = _services.GetById(id);
            return Ok(be);
            //return Created(new Uri(Url.Link("DefaultApi", new { Id = be.CountryId })), be);
        }
        #endregion
    }
}
