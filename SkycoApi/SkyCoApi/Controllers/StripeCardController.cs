using BusinessEntities.BE;
using BusinessServices.Interfaces;
using BusinessServices.Services;
using StripeServices.Interfaces;
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
        private ISubscribrStripeCardPaymentServices _services;

        public StripeCardController(ISubscribrStripeCardPaymentServices services)
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
             dynamic plan =_services.PayAsync(Be);
            return  Created(new Uri(Url.Link("DefaultApi", new { Id = Be.idPaymentIntent })), plan);           
        }

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        public async Task<dynamic> GetAll(int count = 3)
        {           
            return Ok(_services.GetAllPricePlan(count));
        }

        [System.Web.Http.Route("api/StripeCard/Retrievecard")]
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> Retrievecard(Int64 accoutId)
        {
            return Ok(_services.Retrievecard(accoutId));
        }

        [System.Web.Http.Route("api/StripeCard/Retrievesubscription")]
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> Retrievesubscription(Int64 accoutId)
        {
            return Ok(_services.Retrievesubscription(accoutId));

        }
    }
}
