using BusinessEntities.BE;
using BusinessServices.Interfaces;
using DataModal.DataClasses;
using DataModal.UnitOfWork;
using Resolver.Exceptions;
using StripeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Services
{
    public class StripeCardServices: IStripeCardServices
    {
        #region Constructor
        private readonly UnitOfWork _unitOfWork;
        public StripeCardServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        public dynamic Create(PaymentIntentBE Be)
        {
            try
            {
                Boolean iscompleted = false;
                String idStripeCustomer = "";
                String idSubscribe = "";

                PaymentIntent entity = Patterns.Factories.FactoryPaymentIntent.GetInstance().CreateEntity(Be);
                dynamic stripe = StripeCardPayment.PayAsync(entity, ref  iscompleted, ref idStripeCustomer, ref idSubscribe);
                if (iscompleted)
                {
                    dynamic str = stripe;
                    var typeplan = stripe.Plan;
                    StripeSubscribes entitystripe = Transform(str, Be);
                    _unitOfWork.StripeSubscribeRepository.Create(entitystripe);
                    _unitOfWork.Commit();
                    return typeplan;
                }
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

        #region MyRegion
        private StripeSubscribes Transform (dynamic custom, PaymentIntentBE Be)
        {
            StripeSubscribes cust = new StripeSubscribes()
            {
                AccountId = _unitOfWork.SkycoAccountRepository.GetOneByFilters(u => u.UserId == Be.AccountId).AccountId,
                idCardStripe = Be.CardId,
                idPlanPriceStripe = Be.IDStripePrice,
                idStripeCustomer = custom.CustomerId,
                idSubscribe = custom.Id,
                SubscribeDate = Convert.ToDateTime(DateTime.Now),
                state = (Int32)Resolver.Enumerations.StateEnum.Activated
            };

            return cust;
        }
        #endregion
    }
}
