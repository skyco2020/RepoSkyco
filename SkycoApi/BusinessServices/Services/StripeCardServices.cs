using BusinessEntities.BE;
using BusinessServices.Interfaces;
using Resolver.Exceptions;
using StripeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Services
{
    public class StripeCardServices: IStripeCardServices
    {
        public dynamic Create(PaymentIntentBE Be)
        {
            try
            {
                Boolean iscompleted = false;
                PaymentIntent entity = Patterns.Factories.FactoryPaymentIntent.GetInstance().CreateEntity(Be);
                dynamic stripe = StripeCardPayment.PayAsync(entity, ref  iscompleted);
                if (iscompleted)
                    return stripe;
                else
                    throw new ApiBusinessException(64, stripe.Message, System.Net.HttpStatusCode.NotFound, "Http");
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        public dynamic Update(PaymentIntentBE Be)
        {
            try
            {
                Boolean iscompleted = false;
                PaymentIntent entity = Patterns.Factories.FactoryPaymentIntent.GetInstance().CreateEntity(Be);
                dynamic stripe = StripeCardPayment.Update(entity, ref iscompleted);
                if (iscompleted)
                    return stripe;
                else
                    throw new ApiBusinessException(65, stripe.Message, System.Net.HttpStatusCode.NotFound, "Http");
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }
        public async Task<dynamic> GetAllPricePlan(int count)
        {
            try
            {
                return StripeCardPayment.GetAllProduct(count);
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }
    }
}
