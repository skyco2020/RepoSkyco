using BusinessEntities.BE;
using DataModal.DataClasses;
using DataModal.UnitOfWork;
using Resolver.Enumerations;
using Resolver.Exceptions;
using Stripe;
using Stripe.Checkout;
using StripeServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeServices
{
    public class StripeCardPayment: IStripeCardServices
    {
        #region Constructor
        private readonly UnitOfWork _unitOfWork;
        public StripeCardPayment(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        #region Create Subscription old
        //public dynamic PayAsync(PaymentIntent payment, ref Boolean iscompleted, ref String idStripeCustomer, ref String idSubscribe)
        //{            
        //    try
        //    {
        //        #region Secret Key
        //        Key.SecretKey();
        //        #endregion

        //        #region Customer
        //        CustomerCreateOptions customerption = new CustomerCreateOptions
        //        {
        //            Name = payment.fullname,
        //            Email = payment.Email,
        //            Description = payment.Description,
        //            Source = payment.stripeTokenId
        //        };
        //        CustomerService customerservice = new CustomerService();
        //        Customer custom = customerservice.Create(customerption);
        //        #endregion

        //        #region Payment Atach Method
        //        PaymentMethodAttachOptions paymentatch = new PaymentMethodAttachOptions
        //        {
        //            Customer = custom.Id,
        //        };
        //        PaymentMethodService paymentmethodservice = new PaymentMethodService();
        //        PaymentMethod paymentMethod = paymentmethodservice.Attach(
        //          payment.CardId,
        //          paymentatch
        //        );
        //        #endregion

        //        #region Subscription Create
        //        SubscriptionCreateOptions subscriptioncreateoption = new SubscriptionCreateOptions
        //        {
        //            Customer = custom.Id,
        //            Items = new List<SubscriptionItemOptions>
        //        {
        //            new SubscriptionItemOptions
        //            {
        //                //Price = "price_1H3XxiCoU1sl4udJRQZm1Y1P",
        //                Price = payment.iDPlanPrice,
        //                Quantity = 1,

        //            },
        //        },
        //        };
        //        var subscriptionservice = new SubscriptionService();
        //        Subscription subscription = subscriptionservice.Create(subscriptioncreateoption);
        //        #endregion
        //        iscompleted = true;
        //        idStripeCustomer = custom.Id;
        //        idSubscribe = subscription.Id;
        //        return subscription;
        //    }
        //    catch (Exception ex)
        //    {
        //        iscompleted = false;
        //        return ex;
        //    }           
        //}
        #endregion

        #region Create Subscription New
        public async Task<dynamic> PayAsync(PaymentIntentBE Be)
        {
            try
            {
                PaymentIntent payment = Patterns.Factories.FactoryPaymentIntent.GetInstance().CreateEntity(Be);
                #region Secret Key
                Key.SecretKey();
                #endregion

                #region Tokens Card
                TokenCreateOptions options = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = payment.cardnumber,
                        ExpMonth = payment.month,
                        ExpYear = payment.year,
                        Cvc = payment.cvc,                        
                    },
                };
                TokenService service = new TokenService();
                Token strimptoken = service.Create(options);
                #endregion

                #region Customer
                CustomerCreateOptions customerption = new CustomerCreateOptions
                {
                    Name = payment.fullname,
                    Email = payment.Email,
                    Description = payment.Description,
                    Source = strimptoken.Id
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
                  strimptoken.Card.Id,
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
                        Price = payment.iDPlanPrice,
                        Quantity = 1,

                    },
                },
                };
                SubscriptionService subscriptionservice = new SubscriptionService();
                Subscription subscription = subscriptionservice.Create(subscriptioncreateoption);
                #endregion

                #region Insert DB 
                this.Insert(subscription, Be, strimptoken.Card.Id);
                #endregion
                return subscription;
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
                #region Secret Key
                Key.SecretKey();
                #endregion

                PriceListOptions options = new PriceListOptions
                {
                    Limit = count,
                    Active = true,


                };
                PriceService service = new PriceService();
                StripeList<Price> prices = service.List(options);
                return prices;
                //return StripeCardPayment.GetAllProduct(count);
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        #region MyRegion
        private StripeSubscribes Transform(dynamic custom, PaymentIntentBE Be, String CardId)
        {
            StripeSubscribes cust = new StripeSubscribes()
            {
                AccountId = _unitOfWork.SkycoAccountRepository.GetOneByFilters(u => u.UserId == Be.AccountId).AccountId,
                //idCardStripe = Be.CardId,
                idPlanPriceStripe = Be.iDPlanPrice,
                idStripeCustomer = custom.CustomerId,
                idSubscribe = custom.Id,
                idCardStripe = CardId,
                SubscribeDate = Convert.ToDateTime(DateTime.Now),
                state = (Int32)Resolver.Enumerations.StateEnum.Activated,
                countscreen = ListGeneric.GetInstance().GetScreen(custom.Items.Data[0].Plan.Metadata["Type Plan"])
            };

            
            return cust;
        }
        #endregion

        #endregion


        #region Create Subscription no
        public static async Task<dynamic> PayAsync2(PaymentIntent payment)
        {
            try
            {
                #region Secret Key
                Key.SecretKey();
                #endregion
                #region Create option
                var optionstoken = new TokenCreateOptions
                {
                    //Card = new CreditCardOptions
                    //{
                    //    Number = payment.cardnumber,
                    //    ExpMonth = payment.month,
                    //    ExpYear = payment.year,
                    //    Cvc = payment.cvc
                    //}
                };
                Token stripetoken = new Token();
                try
                {
                    var servicetoken = new TokenService();
                     stripetoken = await servicetoken.CreateAsync(optionstoken);
                }
                catch (Exception ex)
                {

                    throw;
                }
                #endregion

                #region Customer
                CustomerCreateOptions customerption = new CustomerCreateOptions
                {
                    Name = payment.fullname,
                    Email = payment.Email,
                    Description = payment.Description,
                    Source = stripetoken.Id
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
                  stripetoken.Card.Id,
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
                        Price = payment.iDPlanPrice,
                        Quantity = 1,

                    },
                },
                };
                var subscriptionservice = new SubscriptionService();
                Subscription subscription = subscriptionservice.Create(subscriptioncreateoption);
                #endregion
                return subscription;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        #endregion

        #region UpdateSubscription
        private static dynamic Update(PaymentIntent payment, ref Boolean iscompleted)
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

        private void Insert(dynamic subscription, PaymentIntentBE Be, String CardId)
        {
            try
            {
                dynamic str = subscription;
                StripeSubscribes entitystripe = Transform(str, Be, CardId);
                _unitOfWork.StripeSubscribeRepository.Create(entitystripe);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
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
            SubscriptionItem del =  service.Delete(subcripId, option);
            return del;
        }
        #endregion

        public static Boolean CheckPayMent(String customerId, String subId, string price)
        {
            #region Secret Key
            Key.SecretKey();
            #endregion          

            var service1 = new SubscriptionService();
            Subscription Subs = service1.Get(subId);
            if (Subs.Items.Count() > 0)
            {
                DateTime date = Subs.Items.Data.LastOrDefault().Created;
                if (date.AddMonths(1) >= DateTime.Today)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}
