using BusinessEntities.BE;
using BusinessServices.Interfaces;
using BusinessServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SkyCoApi.Controllers
{
    public class StripeCardController : ApiController
    {
        #region Single
        private IStripeCardServices _services;

        public StripeCardController(IStripeCardServices services)
        {
            _services = services;
        }
        #endregion

        public async Task<IHttpActionResult> Post(PaymentIntentBE Be)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             dynamic plan =_services.Create(Be);
            return  Created(new Uri(Url.Link("DefaultApi", new { Id = Be.idPaymentIntent })), plan);           
        }

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        public async Task<dynamic> GetAll(int count = 3)
        {           
            return Ok(_services.GetAllPricePlan(count));
        }
    }
}
