using BusinessEntities.BE;
using BusinessServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Resolver.Exceptions;
using Resolver.Exceptions.Handlers;
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
        public async Task<IHttpActionResult> GetById(long id)
        {
            Skyco_UserBE be = _services.GetById(id);
            Skyco_UserDTO doto = new Skyco_UserDTO();
            if (be == null)
                return NotFound();
            doto = Models.FactoryDTO.FactorySkyco_UserDTO.GetInstance().CreateDTO(be);
            return Ok(doto);
        }

        [Authorize]
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetAll(Int32 state = 1, int page = 1, Int32 top = 12, String orderby = "UserId", String ascending = "asc")
        {
            var count = 0;
            IQueryable<Skyco_UserBE> query = _services.GetAll(state, page, top, orderby, ascending, ref count).AsQueryable();
            List<Skyco_UserDTO> listdoto = new List<Skyco_UserDTO>();
            foreach (Skyco_UserBE item in query)
            {
                listdoto.Add(Models.FactoryDTO.FactorySkyco_UserDTO.GetInstance().CreateDTO(item));
            }
            return Ok(listdoto);
        }
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> Post(Skyco_UserBE be)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _services.Create(be);
                return Created(new Uri(Url.Link("DefaultApi", new { Id = be.UserId })), be);
            }
            catch (Exception ex)
            {
                var except = (ApiBusinessException)HandlerErrorExceptions.GetInstance().RunCustomExceptions(ex);
                var resp = BadRequest(Convert.ToString(except.ErrorDescription));
                return resp;
            }
           
        }

        [AllowAnonymous]
        [System.Web.Http.HttpPut]
        public async Task<IHttpActionResult> Put(Skyco_UserBE bE)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _services.Update(bE);
                return Ok();
            }
            catch (Exception ex)
            {
                var except = (ApiBusinessException)HandlerErrorExceptions.GetInstance().RunCustomExceptions(ex);
                var resp = BadRequest(Convert.ToString(except.ErrorDescription));
                return resp;
            }         
        }
        [Authorize]
        [System.Web.Http.HttpDelete]
        public async Task<IHttpActionResult> Delete(Int64 id)
        {
            try
            {
                ClaimsIdentity identityClaims = (ClaimsIdentity)User.Identity;
                _services.Delete(id, identityClaims.FindFirst("username").Value);
                return Ok();
            }
            catch (Exception ex)
            {
                var except = (ApiBusinessException)HandlerErrorExceptions.GetInstance().RunCustomExceptions(ex);
                var resp = BadRequest(Convert.ToString(except.ErrorDescription));
                return resp;
            }           
        }
    }
}
