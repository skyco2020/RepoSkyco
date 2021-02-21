using BusinessEntities.BE;
using DataModal.DataClasses;
using DataModal.UnitOfWork;
using Resolver.Enumerations;
using Resolver.Exceptions;
using Resolver.QueryableExtensions;
using Stripe;
using StripeServices.Interfaces;
using StripeServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StripeServices.Services
{
    public class PlanServiceStripe: IPlanServiceStripe
    {
        #region Constructor
        private readonly UnitOfWork _unitOfWork;
        public PlanServiceStripe(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Retrieve All plan

        public List<PlanBE> RetrieveAllplan(int state, int page, int top, string orderBy, string ascending, ref int count)
        {

            
            Expression<Func<DataModal.DataClasses.Plans, Boolean>> predicate = u => u.state == state;
            IQueryable<DataModal.DataClasses.Plans> entities = _unitOfWork.PlanRepository.GetAllByFilters(predicate, new string[] { "Accounts","Products" });

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
        #endregion

        #region Retrieve a plan

        public async Task<dynamic> Retrieveplan(Int64 accoutId)
        {
            #region Secret Key
            Key.SecretKey();
            #endregion
            Expression<Func<DataModal.DataClasses.StripeSubscribes, Boolean>> predicate = u => u.AccountId == accoutId;
            DataModal.DataClasses.StripeSubscribes entities = _unitOfWork.StripeSubscribeRepository.GetOneByFilters(predicate, null);
            if (entities == null)
                return null;
            PlanService service = new PlanService();
            Plan plan = service.Get(entities.idPlanPriceStripe);
            return plan;
        }
        #endregion

        #region Update Plan

        public async Task<dynamic> UpdatePlan(String priceid, Int64 order_id)
        {
            #region Secret Key
            Key.SecretKey();
            #endregion

            var options = new PlanUpdateOptions
            {
                Metadata = new Dictionary<string, string>
                {
                    { "order_id", order_id.ToString() },
                },
                
            };
            PlanService service = new PlanService();
            Plan uodpaln = service.Update(
              priceid,
              options
            );
            return uodpaln;
        }
        #endregion

        #region Create a Plan

        public async Task<dynamic> CreatePlan(PlanBE plan )
        {
            #region Secret Key
            Key.SecretKey();
            #endregion
            try
            {             
                Plans entity = Patterns.Factories.FactoryPlan.GetInstance().CreateEntity(plan);
                PlanCreateOptions options = new PlanCreateOptions
                {
                    Amount = (Int64)entity.Price,
                    Currency = "usd",
                    Interval = "month",
                    Product = _unitOfWork.ProductRepository.GetById(entity.idProduct).idproductStripe,
                    Metadata = new Dictionary<string, string>
                    {
                        { "Type Plan", plan.TypePlan },
                    },
                };
                var service = new PlanService();
                Plan plancreate = service.Create(options);
                entity.idplanstripe = plancreate.Id;
                this.Insert(entity);
                return plancreate;
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
           
           
        }
        #endregion

        #region Delete Plan

        public async Task<dynamic> DeletePlan(String idplanstripe, String motive)
        {
            #region Secret Key
            Key.SecretKey();
            #endregion
            try
            {              
                PlanService service = new PlanService();
                Plan delplan = service.Delete(idplanstripe);

                this.DeletePlanBD(idplanstripe, motive);
                return delplan;
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
           
        }
        #endregion

        #region Private Method
        private void Insert(Plans plan)
        {
            try
            {
                _unitOfWork.PlanRepository.Create(plan);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }

        }

        private void DeletePlanBD(String PlanId, String motive)
        {
            try
            {
                Expression<Func<DataModal.DataClasses.Plans, Boolean>> predicate = u => u.idplanstripe == PlanId && u.state == (Int32)StateEnum.Activated;
                Plans entity = _unitOfWork.PlanRepository.GetOneByFilters(predicate, null);
                if (entity == null)
                    throw new ApiBusinessException(1000, "Entity not found", System.Net.HttpStatusCode.NotFound, "Http");

                entity.state = (byte)StateEnum.Deleted;
                entity.PlanDate = DateTime.Now;
                entity.Motive = motive;
                _unitOfWork.PlanRepository.Delete(entity, new List<string>() { "state", "PlanDate", "Motive" });
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }

        }
        #endregion
    }
}
