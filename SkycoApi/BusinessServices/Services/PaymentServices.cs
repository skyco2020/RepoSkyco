using Autofac.Core;
using BusinessEntities.BE;
using BusinessServices.Interfaces;
using BusinessServices.Stripe;
using DataModal.DataClasses;
using DataModal.UnitOfWork;
using NUnit.Framework;
using Resolver.Enumerations;
using Resolver.Exceptions;
using Resolver.QueryableExtensions;
using ServiceStack.Stripe.Types;
using Stripe;
using StripeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace BusinessServices.Services
{
    public class PaymentServices: IPaymentServices
    {
        #region Constructor
        private readonly UnitOfWork _unitOfWork;
        public PaymentServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
       
        public long Create(PaymentBE Be)
        {
            try
            {
                var charge = StripeInfo.gateway.Post(new ChargeStripeCustomer
                {
                    Amount = (Int32)Be.Payment_Skyco_Accounts.FirstOrDefault().Amount,
                    Customer = Be.Payment_Skyco_Accounts.FirstOrDefault().idstripecard,
                    Currency = Be.Currency.ToString(),
                    Description = Be.Description
                });

                Payments entity = Patterns.Factories.FactoryPayment.GetInstance().CreateEntity(Be);
                _unitOfWork.PaymentRepository.Create(entity);
                _unitOfWork.Commit();
                return entity.idpayment;

            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        public bool Delete(long Id, string UserName)
        {
            try
            {
                Expression<Func<DataModal.DataClasses.Payments, Boolean>> predicate = u => u.idpayment == Id && u.state == (Int32)StateEnum.Activated;
                Payments entity = _unitOfWork.PaymentRepository.GetOneByFilters(predicate, new string[] { "Payment_Skyco_Accounts" });
                if (entity == null)
                    throw new ApiBusinessException(1000, "Entity not found", System.Net.HttpStatusCode.NotFound, "Http");


                if (entity.Payment_Skyco_Accounts != null)
                {
                    foreach (Payment_Skyco_Accounts item in entity.Payment_Skyco_Accounts)
                    {
                        item.state = (Int32)StateEnum.Deleted;
                        item.paymentdate = DateTime.Now;
                        _unitOfWork.PaymentRepository.Delete(entity, new List<string>() { "state", "paymentdate" });
                    }
                }
                entity.state = (Int32)StateEnum.Deleted;
                _unitOfWork.PaymentRepository.Delete(entity, new List<string>() { "state", "PlanDate" });
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        public List<PaymentBE> GetAll(int state, int page, int top, string orderBy, string ascending, ref int count)
        {
            Expression<Func<DataModal.DataClasses.Payments, Boolean>> predicate = u => u.state == state;
            IQueryable<DataModal.DataClasses.Payments> entities = _unitOfWork.PaymentRepository.GetAllByFilters(predicate, new string[] { "Payment_Skyco_Accounts" });

            count = entities.Count();
            var skipAmount = 0;
            if (page > 0)
                skipAmount = top * (page - 1);

            entities = entities
                .OrderByPropertyOrField(orderBy, ascending)
                .Skip(skipAmount)
                .Take(top);
            List<PaymentBE> listbe = new List<PaymentBE>();
            foreach (Payments item in entities)
            {
                listbe.Add(Patterns.Factories.FactoryPayment.GetInstance().CreateBusiness(item));
            }
            return listbe;
        }

        public PaymentBE GetById(long Id)
        {
            Expression<Func<DataModal.DataClasses.Payments, Boolean>> predicate = u => u.idpayment == Id && u.state == (Int32)StateEnum.Activated;
            Payments entity = _unitOfWork.PaymentRepository.GetOneByFilters(predicate, new string[] { "Payment_Skyco_Accounts" });
            PaymentBE be = null;
            if (entity != null)
            {
                be = new PaymentBE();
                be = Patterns.Factories.FactoryPayment.GetInstance().CreateBusiness(entity);
            }
            return be;
        }

        public bool Update(PaymentBE Be)
        {
            try
            {
                if (Be == null)
                    throw new ApiBusinessException(1001, "Entity is not complete", System.Net.HttpStatusCode.NotFound, "Http");

                Payments entity = Patterns.Factories.FactoryPayment.GetInstance().CreateEntity(Be);
                if (entity.Payment_Skyco_Accounts != null)
                {
                    foreach (Payment_Skyco_Accounts item in entity.Payment_Skyco_Accounts)
                    {
                        _unitOfWork.Payment_Skyco_AccountRepository.Update(item, new List<String> { "AccountId", "Amount", "idstripecard", "paymentdate"});
                    }
                }
                _unitOfWork.PaymentRepository.Update(entity, new List<String> { "idcard", "PlanId", "Quantity", "Currency", "Description", "name" });
                _unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }
    }
}
