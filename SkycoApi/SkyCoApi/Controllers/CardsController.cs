﻿using BusinessEntities.BE;
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
    public class CardsController : ApiController
    {
        #region Single
        private ICardServices _services;

        public CardsController(ICardServices services)
        {
            _services = services;
        }
        #endregion

        [Authorize]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(CardDTOCollectionRepresentation))]
        public async Task<IHttpActionResult> GetById(long id)
        {
            CardBE be = _services.GetById(id);
            CardDTO dto = new CardDTO();
            if (be == null)
                return NotFound();
            dto = Models.FactoryDTO.FactoryCardDTO.GetInstance().CreateDTO(be);
            dto.CreatesMySelfLinks();
            return Ok(dto);
        }

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(CardDTOCollectionRepresentation))]
        public async Task<IHttpActionResult> GetAll(Int32 state = (Int32)StateEnum.Activated, int page = 1, Int32 top = 12, String orderby = nameof(CardDTO.idcard), String ascending = "asc")
        {
            var count = 0;
            IQueryable<CardBE> query = _services.GetAll(state, page, top, orderby, ascending, ref count).AsQueryable();
            List<CardDTO> listdoto = new List<CardDTO>();
            foreach (CardBE item in query)
            {
                listdoto.Add(Models.FactoryDTO.FactoryCardDTO.GetInstance().CreateDTO(item));
            }
            System.Collections.Specialized.HybridDictionary myfilters = new System.Collections.Specialized.HybridDictionary();
            myfilters.Add("state", state);
            CardDTOCollectionRepresentation dt = new CardDTOCollectionRepresentation(listdoto.ToList(), FilterHelper.GenerateFilter(myfilters, top, orderby, ascending), page, count, top);
            return Ok(dt);
        }
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> Post(CardBE be)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _services.Create(be);
            return Created(new Uri(Url.Link("DefaultApi", new { Id = be.Id })), be);
        }

        [Authorize]
        [System.Web.Http.HttpPut]
        public async Task<IHttpActionResult> Put(CardBE bE)
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
