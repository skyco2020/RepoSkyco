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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StripeServices.Services
{
    public class SubscribrStripeCardPayment : ISubscribrStripeCardPaymentServices
    {
        #region Constructor
        private readonly UnitOfWork _unitOfWork;
        public SubscribrStripeCardPayment(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Create Subscription 
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
                        Currency = "usd"
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
                dynamic stripe = SubscribrStripeCardPayment.Update(entity, ref iscompleted);
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
                countscreen = ListGeneric.GetInstance().GetScreen(Be.TypePlan)
            };


            return cust;
        }
        #endregion

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
                Customer custom = customerservice.Update(payment.Customerid, customerption);
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

            SubscriptionService service = new SubscriptionService();
            Subscription del = service.Cancel(subcripId);
            return del;
        }
        #endregion

        #region Retrieve a card

        public async Task<dynamic> Retrievecard(Int64 accoutId)
        {
            #region Secret Key
            Key.SecretKey();
            #endregion
            Expression<Func<DataModal.DataClasses.StripeSubscribes, Boolean>> predicate = u => u.AccountId == accoutId;
            DataModal.DataClasses.StripeSubscribes entities = _unitOfWork.StripeSubscribeRepository.GetOneByFilters(predicate, null);
            if (entities == null)
                return null;
           CardService service = new CardService();
           Card card = service.Get(
              entities.idStripeCustomer,
              entities.idCardStripe
            );
            return card;
        }
        #endregion

        #region Retrieve a subscription

        public async Task<dynamic> Retrievesubscription(Int64 accoutId)
        {
            #region Secret Key
            Key.SecretKey();
            #endregion
            Expression<Func<DataModal.DataClasses.StripeSubscribes, Boolean>> predicate = u => u.AccountId == accoutId;
            DataModal.DataClasses.StripeSubscribes entities = _unitOfWork.StripeSubscribeRepository.GetOneByFilters(predicate, null);
            if (entities == null)
                return null;
            SubscriptionService service = new SubscriptionService();
            Subscription subscription = service.Get(
               entities.idSubscribe
             );
            return subscription;
        }
        #endregion

    }
}
