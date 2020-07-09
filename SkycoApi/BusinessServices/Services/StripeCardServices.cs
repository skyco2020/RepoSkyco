using BusinessEntities.BE;
using Resolver.Exceptions;
using StripeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Services
{
    public class StripeCardServices
    {
        public async Task<dynamic> Create(PaymentIntentBE Be)
        {
            try
            {
                return await StripeCardPayment.PayAsync(Be.amount, Be.name);              
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }
    }
}
