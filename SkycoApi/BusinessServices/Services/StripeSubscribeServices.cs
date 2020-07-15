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
    public class StripeSubscribeServices: IStripeSubscribeServices
    {
        #region Constructor
        private readonly UnitOfWork _unitOfWork;
        public StripeSubscribeServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
       
        public long Create(StripeSubscribeBE Be)
        {
            try
            {
                StripeSubscribes entity = Patterns.Factories.FactoryStripeSubscribe.GetInstance().CreateEntity(Be);
                _unitOfWork.StripeSubscribeRepository.Create(entity);
                _unitOfWork.Commit();
                return entity.idStripeSubscribe;

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
                Expression<Func<DataModal.DataClasses.StripeSubscribes, Boolean>> predicate = u => u.idStripeSubscribe == Id && u.state == (Int32)StateEnum.Activated;
                StripeSubscribes entity = _unitOfWork.StripeSubscribeRepository.GetOneByFilters(predicate, null);
                if (entity == null)
                    throw new ApiBusinessException(1000, "Entity not found", System.Net.HttpStatusCode.NotFound, "Http");               
                entity.state = (Int32)StateEnum.Deleted;
                _unitOfWork.StripeSubscribeRepository.Delete(entity, new List<string>() { "state", "SubscribeDate" });
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        public List<StripeSubscribeBE> GetAll(int state, int page, int top, string orderBy, string ascending, ref int count)
        {
            Expression<Func<DataModal.DataClasses.StripeSubscribes, Boolean>> predicate = u => u.state == state;
            IQueryable<DataModal.DataClasses.StripeSubscribes> entities = _unitOfWork.StripeSubscribeRepository.GetAllByFilters(predicate, null);

            count = entities.Count();
            var skipAmount = 0;
            if (page > 0)
                skipAmount = top * (page - 1);

            entities = entities
                .OrderByPropertyOrField(orderBy, ascending)
                .Skip(skipAmount)
                .Take(top);
            List<StripeSubscribeBE> listbe = new List<StripeSubscribeBE>();
            foreach (StripeSubscribes item in entities)
            {
                listbe.Add(Patterns.Factories.FactoryStripeSubscribe.GetInstance().CreateBusiness(item));
            }
            return listbe;
        }

        public StripeSubscribeBE GetById(long Id)
        {
            Expression<Func<DataModal.DataClasses.StripeSubscribes, Boolean>> predicate = u => u.idStripeSubscribe == Id && u.state == (Int32)StateEnum.Activated;
            StripeSubscribes entity = _unitOfWork.StripeSubscribeRepository.GetOneByFilters(predicate,null);
            StripeSubscribeBE be = null;
            if (entity != null)
            {
                be = new StripeSubscribeBE();
                be = Patterns.Factories.FactoryStripeSubscribe.GetInstance().CreateBusiness(entity);
            }
            return be;
        }

        public bool Update(StripeSubscribeBE Be)
        {
            try
            {
                if (Be == null)
                    throw new ApiBusinessException(1001, "Entity is not complete", System.Net.HttpStatusCode.NotFound, "Http");

                StripeSubscribes entity = Patterns.Factories.FactoryStripeSubscribe.GetInstance().CreateEntity(Be);
                
                _unitOfWork.StripeSubscribeRepository.Update(entity, new List<String> { "AccountId", "idPlanPriceStripe"});
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
