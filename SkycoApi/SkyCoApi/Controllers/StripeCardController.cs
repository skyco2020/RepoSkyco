using BusinessEntities.BE;
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
        private StripeCardServices _services;

        public StripeCardController(StripeCardServices services)
        {
            _services = services;
        }
        #endregion

        public async Task<dynamic> Post(PaymentIntentBE Be)
        {
            return _services.Create(Be);
        }

        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        public async Task<dynamic> GetAll(int count = 3)
        {           
            return Ok(_services.GetAllPricePlan(count));
        }
    }
}
