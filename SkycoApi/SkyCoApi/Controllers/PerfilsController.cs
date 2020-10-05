using BusinessEntities.BE;
using BusinessServices.Interfaces;
using Resolver.Enumerations;
using SkyCoApi.Helpers;
using SkyCoApi.Models.DTO.Collections;
using SkyCoApi.Models.DTO.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SkyCoApi.Controllers
{
    public class PerfilsController : ApiController
    {
        #region Single
        private IPerfilServices _services;

        public PerfilsController(IPerfilServices services)
        {
            _services = services;
        }
        #endregion

        [Authorize]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(PerfilDTOCollectionRepresentation))]
        public async Task<IHttpActionResult> GetById(long id)
        {
            PerfilBE be = _services.GetById(id);
            PerfilDTO dto = new PerfilDTO();
            if (be == null)
                return NotFound();
            dto = Models.FactoryDTO.FactoryPerfilDTO.GetInstance().CreateDTO(be);
            dto.CreatesMySelfLinks();
            return Ok(dto);
        }

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(PerfilDTOCollectionRepresentation))]
        public async Task<IHttpActionResult> GetAll(Int64 AccountId,Int32 state = (Int32)StateEnum.Activated, int page = 1, Int32 top = 12, String orderby = nameof(PerfilDTO.idPerfil), String ascending = "asc")
        {
            var count = 0;
            IQueryable<PerfilBE> query = _services.GetAll(state, page, top, orderby, ascending,AccountId, ref count).AsQueryable();
            List<PerfilDTO> listdoto = new List<PerfilDTO>();
            foreach (PerfilBE item in query)
            {
                listdoto.Add(Models.FactoryDTO.FactoryPerfilDTO.GetInstance().CreateDTO(item));
            }
            System.Collections.Specialized.HybridDictionary myfilters = new System.Collections.Specialized.HybridDictionary();
            myfilters.Add("state", state);
            PerfilDTOCollectionRepresentation dt = new PerfilDTOCollectionRepresentation(listdoto.ToList(), FilterHelper.GenerateFilter(myfilters, top, orderby, ascending), page, count, top);
            return Ok(dt);
        }
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> Post(PerfilBE be)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            be.idPerfil = _services.Create(be);
            return Created(new Uri(Url.Link("DefaultApi", new { Id = be.idPerfil })), be);
        }

        [AllowAnonymous]
        [System.Web.Http.HttpPut]
        public async Task<IHttpActionResult> Put(PerfilBE bE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _services.Update(bE);
            return Ok();
        }
        [Authorize]
        [System.Web.Http.HttpDelete]
        public async Task<IHttpActionResult> Delete(Int64 id)
        {
            //ClaimsIdentity identityClaims = (ClaimsIdentity)User.Identity;
            _services.Delete(id);
            return Ok();
        }

    }
}
