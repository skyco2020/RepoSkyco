using BusinessEntities.BE;
using BusinessServices.Interfaces;
using DataModal.DataClasses;
using DataModal.UnitOfWork;
using Resolver.Enumerations;
using Resolver.Exceptions;
using Resolver.QueryableExtensions;
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
    public class PlanServices : IPlanServices
    {
        #region Constructor
        private readonly UnitOfWork _unitOfWork;
        public PlanServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        public long Create(PlanBE Be)
        {
            try
            {
                Plans entity = Patterns.Factories.FactoryPlan.GetInstance().CreateEntity(Be);
                _unitOfWork.PlanRepository.Create(entity);
                _unitOfWork.Commit();
                return entity.PlanId;

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
                Expression<Func<DataModal.DataClasses.Plans, Boolean>> predicate = u => u.PlanId == Id && u.state == (Int32)StateEnum.Activated;
                Plans entity = _unitOfWork.PlanRepository.GetOneByFilters(predicate, null);
                if (entity == null)
                    throw new ApiBusinessException(1000, "Entity not found", System.Net.HttpStatusCode.NotFound, "Http");

                entity.state = (byte)StateEnum.Deleted;
                entity.PlanDate = DateTime.Now;
                _unitOfWork.PlanRepository.Delete(entity, new List<string>() { "state", "PlanDate"});
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
           
        }

        public List<PlanBE> GetAll(int state, int page, int top, string orderBy, string ascending, ref int count)
        {
            Expression<Func<DataModal.DataClasses.Plans, Boolean>> predicate = u => u.state == state;
            IQueryable<DataModal.DataClasses.Plans> entities = _unitOfWork.PlanRepository.GetAllByFilters(predicate, new string[] { "Accounts" });

            count = entities.Count();
            var skipAmount = 0;
            if (page > 0)
                skipAmount = top * (page - 1);

            entities = entities
                .OrderByPropertyOrField(orderBy, ascending)
                .Skip(skipAmount)
                .Take(top);
            List<PlanBE> listbe = new List<PlanBE>();
           
            foreach (Plans item in entities)
            {
                listbe.Add(Patterns.Factories.FactoryPlan.GetInstance().CreateBusiness(item));
            }
            return listbe;
        }      

        public PlanBE GetById(long Id)
        {
            Expression<Func<DataModal.DataClasses.Plans, Boolean>> predicate = u => u.PlanId == Id && u.state == (Int32)StateEnum.Activated;
            Plans entity = _unitOfWork.PlanRepository.GetOneByFilters(predicate, new string[] { "Accounts" });
            PlanBE be = null;
            if (entity != null)
            {
                be = new PlanBE();
                be = Patterns.Factories.FactoryPlan.GetInstance().CreateBusiness(entity);
            }
            return be;
        }

        public bool Update(PlanBE Be)
        {
            try
            {
                if (Be == null)
                    throw new ApiBusinessException(1001, "Entity is not complete", System.Net.HttpStatusCode.NotFound, "Http");

                Plans entity = Patterns.Factories.FactoryPlan.GetInstance().CreateEntity(Be);
                _unitOfWork.PlanRepository.Update(entity, new List<String> { "TypePlan", "Price", "Description" });
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
