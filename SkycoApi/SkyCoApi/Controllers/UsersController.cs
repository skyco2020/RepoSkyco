using BusinessEntities.BE;
using BusinessServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Resolver.Enumerations;
using Resolver.Exceptions;
using Resolver.Exceptions.Handlers;
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
    /// <summary>
    /// customer controller class for testing security token
    /// </summary>
    //[Authorize]
    //[RoutePrefix("api/customers")]
    public class UsersController : ApiController
    {
        #region Single
        private ISkyco_UsersServices _services;

        public UsersController(ISkyco_UsersServices services)
        {
            _services = services;
        }
        #endregion
        [Authorize]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(Skyco_UserDTOCollectionRepresentation))]
        public async Task<IHttpActionResult> GetById(long id)
        {
            Skyco_UserBE be = _services.GetById(id);
            Skyco_UserDTO dto = new Skyco_UserDTO();
            if (be == null)
                return NotFound();
            dto = Models.FactoryDTO.FactorySkyco_UserDTO.GetInstance().CreateDTO(be);
            dto.CreatesMySelfLinks();
            return Ok(dto);
        }

        [Authorize]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(Skyco_UserDTOCollectionRepresentation))]
        public async Task<IHttpActionResult> GetAll(Int32 state = (Int32)StateEnum.Activated, int page = 1, Int32 top = 12, String orderby = nameof(Skyco_UserDTO.UserId), String ascending = "asc")
        {
            var count = 0;
            IQueryable<Skyco_UserBE> query = _services.GetAll(state, page, top, orderby, ascending, ref count).AsQueryable();
            List<Skyco_UserDTO> listdoto = new List<Skyco_UserDTO>();
            foreach (Skyco_UserBE item in query)
            {
                listdoto.Add(Models.FactoryDTO.FactorySkyco_UserDTO.GetInstance().CreateDTO(item));
            }
            System.Collections.Specialized.HybridDictionary myfilters = new System.Collections.Specialized.HybridDictionary();
            myfilters.Add("state", state);
            Skyco_UserDTOCollectionRepresentation dt = new Skyco_UserDTOCollectionRepresentation(listdoto.ToList(), FilterHelper.GenerateFilter(myfilters, top, orderby, ascending), page, count, top);
            return Ok(dt);
        }
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> Post(Skyco_UserBE be)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _services.Create(be);
            return Created(new Uri(Url.Link("DefaultApi", new { Id = be.UserId })), be); 
        }

        [AllowAnonymous]
        [System.Web.Http.HttpPut]
        public async Task<IHttpActionResult> Put(Skyco_UserBE bE)
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
            ClaimsIdentity identityClaims = (ClaimsIdentity)User.Identity;
            _services.Delete(id, identityClaims.FindFirst("username").Value);
            return Ok();                   
        }
    }
}
