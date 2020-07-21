using Resolver.Exceptions;
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
        public static dynamic PayAsync(PaymentIntent payment, ref Boolean iscompleted, ref String idStripeCustomer, ref String idSubscribe)
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
                var subscriptionservice = new SubscriptionService();
                Subscription subscription = subscriptionservice.Create(subscriptioncreateoption);
                #endregion
                iscompleted = true;
                idStripeCustomer = custom.Id;
                idSubscribe = subscription.Id;
                return subscription;
            }
            catch (Exception ex)
            {
                iscompleted = false;
                return ex;
            }           
        }
        #endregion

        #region UpdateSubscription
        public static dynamic Update(PaymentIntent payment, ref Boolean iscompleted)
        {
            try
            {
                #region Secret Key
                Key.SecretKey();
                #endregion

                #region Customer
                CustomerUpdateOptions customerption = new CustomerUpdateOptions
                {
                    Name = payment.fullname,
                    Email = payment.Email,
                    Description = payment.Description,
                    Source = payment.stripeTokenId
                };
                CustomerService customerservice = new CustomerService();
                Customer custom = customerservice.Update(payment.Customerid,customerption);
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
                iscompleted = true;

                return custom;

            }
            catch (Exception ex)
            {
                iscompleted = false;
                return ex;
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
     
        public static async Task<dynamic> DeleteSub(String subcripId)
        {
            #region Secret Key
            Key.SecretKey();
            #endregion
            SubscriptionItemService service = new SubscriptionItemService();
            SubscriptionItemDeleteOptions option = new SubscriptionItemDeleteOptions()
            {
                ClearUsage = true                
            };
            var del =  service.Delete(subcripId, option);
            return null;
        }
        #endregion
    }
}
