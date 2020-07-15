using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Configuration;
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
                Key.SecretKey();
                #endregion

                #region Customer
                CustomerCreateOptions customerption = new CustomerCreateOptions
                {
                    Name = payment.fullname,
                    Email = payment.Email,
                    Description = payment.Description,
                    Source = payment.stripeTokenId
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
                  payment.CardId,
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
                        Price = payment.IDStripePrice,
                        Quantity = 1,

                    },
                },
                };
                var service3 = new SubscriptionService();
                Subscription subscription = service3.Create(subscriptioncreateoption);
                #endregion
                return subscription;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }           
        }
        #endregion

        #region UpdateSubscription
        public static async Task<dynamic> Update(PaymentIntent payment)
        {
            try
            {
                #region Secret Key
                Key.SecretKey();
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

        public static async Task<dynamic> GetAllProduct(int count)
        {
            try
            {
                #region Secret Key
                Key.SecretKey();
                #endregion

                PriceListOptions options = new PriceListOptions
                {
                    Limit = count
                };
                PriceService service = new PriceService();
                StripeList<Price> prices = service.List(options);
                return prices;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
           
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
