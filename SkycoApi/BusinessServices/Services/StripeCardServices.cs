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
                PaymentIntent entity = Patterns.Factories.FactoryPaymentIntent.GetInstance().CreateEntity(Be);
                return await StripeCardPayment.PayAsync(entity);              
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }
        public async Task<dynamic> GetAllPricePlan(int count)
        {
            return StripeCardPayment.GetAllProduct(count);
        }
    }
}
