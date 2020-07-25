using BusinessEntities.BE;
using BusinessServices.Interfaces;
using BusinessServices.Patterns.Factories;
using DataModal.DataClasses;
using DataModal.UnitOfWork;
using Resolver.Cryptography;
using Resolver.Exceptions;
using StripeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Services
{
    public class Skyco_AccountServices : ISkyco_AccountServices
    {
        #region Single
        private readonly UnitOfWork _unitOfWork;

        public Skyco_AccountServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        public Skyco_AccountBE GetById(int Id)
        {
            try
            {
                Expression<Func<DataModal.DataClasses.Skyco_Accounts, Boolean>> predicate = u => u.UserId == Id;
                DataModal.DataClasses.Skyco_Accounts entities = _unitOfWork.SkycoAccountRepository.GetOneByFilters(predicate, new string[] { "Skyco_AccountType", "Location"});
                if (entities != null)
                    return FactorySkyco_Account.GetInstance().CreateBusiness(entities);
                return null;
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        public Skyco_AccountBE GetLogin(string username, string userpass)
        {
            try
            {
                String Passhash = MD5Base.GetInstance().Encypt(userpass);
                Expression<Func<DataModal.DataClasses.Skyco_Accounts, Boolean>> predicate = u => u.Username == username && u.PasswordHash == Passhash;
                DataModal.DataClasses.Skyco_Accounts entities = _unitOfWork.SkycoAccountRepository.GetOneByFilters(predicate, new string[] { "Skyco_AccountType", "Location" });
                if (entities == null)
                    throw new ApiBusinessException((Int32)(entities.AccountId), "Wrong username or password", System.Net.HttpStatusCode.NotFound, "Http");
               
                StripeSubscribes stripeentity = _unitOfWork.StripeSubscribeRepository.GetOneByFilters(u => u.AccountId == entities.AccountId);
                if (stripeentity == null)
                    throw new ApiBusinessException((Int32)(entities.AccountId), "You need tu complete payment", System.Net.HttpStatusCode.NotFound, "Http");
                
                Boolean stripe = StripeCardPayment.CheckPayMent(stripeentity.idStripeCustomer, stripeentity.idSubscribe, stripeentity.idPlanPriceStripe);
                if (stripe == false)
                    throw new ApiBusinessException((Int32)(entities.AccountId), "Payment is missing for this month", System.Net.HttpStatusCode.NotFound, "Http");

                return FactorySkyco_Account.GetInstance().CreateBusiness(entities);
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        public bool ModifyPassword(string userpass, long iduser)
        {
            throw new NotImplementedException();
        }

        public bool Update(Skyco_AccountBE Be)
        {
            throw new NotImplementedException();
        }
    }
}
