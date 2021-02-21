using BusinessEntities.BE;
using BusinessServices.Interfaces;
using Resolver.Enumerations;
using SkyCoApi.Helpers;
using SkyCoApi.Models.DTO.Collections;
using SkyCoApi.Models.DTO.Single;
using StripeServices.Interfaces;
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
    public class PlansController : ApiController
    {
        #region Single
        private IPlanServiceStripe _services;

        public PlansController(IPlanServiceStripe services)
        {
            _services = services;
        }
        #endregion

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(PlanDTOCollectionRepresentation))]
        public async Task<IHttpActionResult> GetById(long id)
        {
            //PlanBE be = _services.Retrieveplan(id);
            //PlanDTO dto = new PlanDTO();
            //if (be == null)
            //    return NotFound();
            //dto = Models.FactoryDTO.FactoryPlanDTO.GetInstance().CreateDTO(be);
            //dto.CreatesMySelfLinks();
            return Ok(_services.Retrieveplan(id));
        }

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(PlanDTOCollectionRepresentation))]
        public async Task<IHttpActionResult> GetAll(Int32 state = (Int32)StateEnum.Activated, int page = 1, Int32 top = 12, String orderby = nameof(PlanDTO.PlanId), String ascending = "asc")
        {
            var count = 0;
            IQueryable<PlanBE> query = _services.RetrieveAllplan(state, page, top, orderby, ascending, ref count).AsQueryable();
            List<PlanDTO> listdoto = new List<PlanDTO>();
            foreach (PlanBE item in query)
            {
                listdoto.Add(Models.FactoryDTO.FactoryPlanDTO.GetInstance().CreateDTO(item));
            }
            System.Collections.Specialized.HybridDictionary myfilters = new System.Collections.Specialized.HybridDictionary();
            myfilters.Add("state", state);
            PlanDTOCollectionRepresentation dt = new PlanDTOCollectionRepresentation(listdoto.ToList(), FilterHelper.GenerateFilter(myfilters, top, orderby, ascending), page, count, top);
            return Ok(dt);
        }
       
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> Post(PlanBE be)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _services.CreatePlan(be);
            return Created(new Uri(Url.Link("DefaultApi", new { Id = be.Id })), be);
        }

        //[AllowAnonymous]
        //[System.Web.Http.HttpPut]
        //public async Task<IHttpActionResult> Put(PlanBE bE)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    _services.Update(bE);
        //    return Ok();
        //}
        [AllowAnonymous]
        [Route("api/Plans/Delete")]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> Delete(PlanBE data)
        {
            //ClaimsIdentity identityClaims = (ClaimsIdentity)User.Identity;
            _services.DeletePlan(data.idplanstripe, data.Motive);
            return Ok();
        }
    }
}
