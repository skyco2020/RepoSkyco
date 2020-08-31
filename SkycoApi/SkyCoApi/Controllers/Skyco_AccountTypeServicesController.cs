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
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SkyCoApi.Controllers
{
    public class Skyco_AccountTypeServicesController : ApiController
    {
        #region Single
        private ISkyco_AccountTypeServices _services;

        public Skyco_AccountTypeServicesController(ISkyco_AccountTypeServices services)
        {
            _services = services;
        }
        #endregion

        [Authorize]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(Skyco_AccountTypeDTOCollectionRepresentation))]
        public async Task<IHttpActionResult> GetById(long id)
        {
            Skyco_AccountTypeBE be = _services.GetById(id);
            Skyco_AccountTypeDTO dto = new Skyco_AccountTypeDTO();
            if (be == null)
                return NotFound();
            dto = Models.FactoryDTO.FactorySkyco_AccountTypeDTO.GetInstance().CreateDTO(be);
            dto.CreatesMySelfLinks();
            return Ok(dto);
        }

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [ResponseType(typeof(Skyco_AccountTypeDTOCollectionRepresentation))]
        public async Task<IHttpActionResult> GetAll(Int32 state = (Int32)StateEnum.Activated, int page = 1, Int32 top = 12, String orderby = nameof(Skyco_AccountTypeBE.AccountTypeId), String ascending = "asc")
        {
            var count = 0;
            IQueryable<Skyco_AccountTypeBE> query = _services.GetAll(state, page, top, orderby, ascending, ref count).AsQueryable();
            List<Skyco_AccountTypeDTO> listdoto = new List<Skyco_AccountTypeDTO>();
            foreach (Skyco_AccountTypeBE item in query)
            {
                listdoto.Add(Models.FactoryDTO.FactorySkyco_AccountTypeDTO.GetInstance().CreateDTO(item));
            }
            System.Collections.Specialized.HybridDictionary myfilters = new System.Collections.Specialized.HybridDictionary();
            myfilters.Add("state", state);
            Skyco_AccountTypeDTOCollectionRepresentation dt = new Skyco_AccountTypeDTOCollectionRepresentation(listdoto.ToList(), FilterHelper.GenerateFilter(myfilters, top, orderby, ascending), page, count, top);
            return Ok(dt);
        }
    }
}
