using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeServices
{
    public static class StripeCardPayment
    {
        

        public static async Task<dynamic> PayAsync(/*String cardnumber, Int32 month, Int32 year, String cvc,  String name,*/long value, String tokenID)
        {
            //try
            //{
                Key();
            //TokenCreateOptions optionstoken = new TokenCreateOptions
            //{
            //    Card = new CreditCardOptions
            //    {
            //        Number = cardnumber,
            //        ExpMonth = month,
            //        ExpYear = year,
            //        Cvc = cvc
            //    }
            //};
            //TokenService servetoken = new TokenService();
            //Token stripetoken = await servetoken.CreateAsync(optionstoken);

            //    ChargeCreateOptions options = new ChargeCreateOptions
            //    {
            //        Amount = value,
            //        Currency = "usd",
            //        Description = "Contracted plan",
            //        Source = tokenID
            //    };

            //    ChargeService service = new ChargeService();
            //    Charge charge = await service.CreateAsync(options);

            //    if (charge.Paid)
            //    {
            //        return "Success";
            //    }
            //    else
            //    {
            //        return "failed";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return ex.Message;
            //}
            //PaymentIntentCreateOptions options = new PaymentIntentCreateOptions
            //{
            //    Amount = value,
            //    Currency = "usd",
            //    Source =  tokenID,
            //    //Verify your integration in this guide by including this parameter
            //    Metadata = new Dictionary<string, string>
            //    {
            //      { "integration_check", "accept_a_payment" },
            //    },
            //};

            //var service = new PaymentIntentService();
            //var paymentIntent = service.Create(options);

            //return "Success";
            var options = new SessionCreateOptions
            {
                SuccessUrl = "https://localhost:8080/success?id={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://localhost:8080/cancel",
                PaymentMethodTypes = new List<string> {
                  "card",
                },
                        LineItems = new List<SessionLineItemOptions> {
                  new SessionLineItemOptions {
                    Name = "T-shirt",
                    Description = "Comfortable cotton t-shirt",
                    Amount = value,
                    Currency = "usd",
                    Quantity = 4,
                  },
                },
            };

            var service = new SessionService();
            Session session = service.Create(options);
            return session.Id;
        }

        private static void Key()
        {
            // Set your secret key. Remember to switch to your live secret key in production!
            // See your keys here: https://dashboard.stripe.com/account/apikeys
            StripeConfiguration.ApiKey = "sk_test_pJiL43vnUyaJT9xOyyG80W4s0096SCKG0c";
        }
    }
}
