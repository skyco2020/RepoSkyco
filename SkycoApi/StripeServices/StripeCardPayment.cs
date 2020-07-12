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

        #region Create Subscription
        public static async Task<dynamic> PayAsync(PaymentIntent payment)
        {
            try
            {
                #region Secret Key
                Key();
                #endregion

                #region TOken Credit Card
                TokenCreateOptions tokenoption = new TokenCreateOptions
                {
                    Card = new CreditCardOptions
                    {
                        Number = payment.cardnumber,
                        ExpYear = payment.year,
                        ExpMonth = payment.month,
                        Cvc = payment.cvc
                    }
                };

                TokenService tokenservice = new TokenService();
                Token stripeToken = tokenservice.Create(tokenoption);
                #endregion

                #region Customer
                CustomerCreateOptions customerption = new CustomerCreateOptions
                {
                    Name = payment.fullname,
                    Email = payment.Email,
                    Description = payment.Description,
                    Source = stripeToken.Id
                };
                CustomerService customerservice = new CustomerService();
                Customer custom = customerservice.Create(customerption);
                #endregion

                #region Payment Atach Method
                PaymentMethodAttachOptions paymentatch = new PaymentMethodAttachOptions
                {
                    Customer = custom.Id,
                };
                PaymentMethodService paymentmethodservice = new PaymentMethodService();
                PaymentMethod paymentMethod = paymentmethodservice.Attach(
                  stripeToken.Card.Id,
                  paymentatch
                );
                #endregion

                #region Subscription Create
                SubscriptionCreateOptions subscriptioncreateoption = new SubscriptionCreateOptions
                {
                    Customer = custom.Id,
                    Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions
                    {
                        //Price = "price_1H3XxiCoU1sl4udJRQZm1Y1P",
                        Price = payment.Price,
                        Quantity = 1,

                    },
                },
                };
                var service3 = new SubscriptionService();
                Subscription subscription = service3.Create(subscriptioncreateoption);
                #endregion

                #region Detail subscription
                SubscriptionItemService subscriptionservice = new SubscriptionItemService();
                return subscriptionservice.Get(subscription.Id);
                #endregion

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            //var options = new SessionCreateOptions
            //{
            //    SuccessUrl = "https://localhost:8080/success?id={CHECKOUT_SESSION_ID}",
            //    CancelUrl = "https://localhost:8080/cancel",
            //    PaymentMethodTypes = new List<string> {
            //      "card",
            //    },
            //            LineItems = new List<SessionLineItemOptions> {
            //      new SessionLineItemOptions {
            //        Name = "T-shirt",
            //        Description = "Comfortable cotton t-shirt",
            //        Amount = value,
            //        Currency = "usd",
            //        Quantity = 4,
            //      },
            //    },
            //};

            //var service = new SessionService();
            //Session session = service.Create(options);
            //return session.Id;
        }
        #endregion

        #region Secret Key
        private static void Key()
        {
            // Set your secret key. Remember to switch to your live secret key in production!
            // See your keys here: https://dashboard.stripe.com/account/apikeys
            StripeConfiguration.ApiKey = "sk_test_pJiL43vnUyaJT9xOyyG80W4s0096SCKG0c";
        }
        #endregion

        #region UpdateSubscription
        public static async Task<dynamic> Update(PaymentIntent payment)
        {
            try
            {
                #region Secret Key
                Key();
                #endregion
                var options = new CardUpdateOptions
                {
                    Name = "Jenny Rosen",                  
                    
                };
                CardService service = new CardService();
                 Card updatecard =service.Update(
                  "cus_HdANU9WSjrJgan",
                  "card_1H3u2cCoU1sl4udJZFipXGPy",
                  options
                );
                return updatecard;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        #endregion

        public static async Task<dynamic> GetAllProduct()
        {
            #region Secret Key
            Key();
            #endregion

            PriceListOptions options = new PriceListOptions 
            { 
                Limit = 3
            };
            PriceService service = new PriceService();
            StripeList<Price> prices = service.List(options);
            return prices;
        }
        #region Delete Subscription
        //public static async Task<dynamic> DeleteSub(String subcripId)
        //{
        //    #region Secret Key
        //    Key();
        //    #endregion
        //    var service = new SubscriptionService();
        //    service.Cancel(subcripId);
        //}
        #endregion
    }
}
