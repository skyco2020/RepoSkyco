using BusinessEntities.BE;
using SkyCoApi.Models.DTO.Single;
using StripeServices.Interfaces;
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
    public class ProductsController : ApiController
    {
        #region Single
        private IProductServiceStripe _services;

        public ProductsController(IProductServiceStripe services)
        {
            _services = services;
        }
        #endregion

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetAll(Boolean active = true, int page = 1, Int32 top = 12, String orderby = nameof(ProductDTO.idProduct), String ascending = "asc")
        {
            var count = 0;
            IQueryable<ProductBE> query = _services.RetrieveAllProduct(active, page, top, orderby, ascending, ref count).AsQueryable();
            List<ProductDTO> listdoto = new List<ProductDTO>();
            foreach (ProductBE item in query)
            {
                listdoto.Add(Models.FactoryDTO.FactoryProductDTO.GetInstance().CreateDTO(item));
            }
            //System.Collections.Specialized.HybridDictionary myfilters = new System.Collections.Specialized.HybridDictionary();
            //myfilters.Add("state", active);
            //PlanDTOCollectionRepresentation dt = new PlanDTOCollectionRepresentation(listdoto.ToList(), FilterHelper.GenerateFilter(myfilters, top, orderby, ascending), page, count, top);
            return Ok(listdoto);
        }

        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> Post(ProductBE be)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _services.CreateProduct(be);
            return Created(new Uri(Url.Link("DefaultApi", new { Id = be.Id })), be);
        }

        [AllowAnonymous]
        [System.Web.Http.HttpDelete]
        public async Task<IHttpActionResult> Delete(Int64 id)
        {
            _services.DeleteProduct(id);
            return Ok();
        }
    }
}
