using BusinessEntities.BE;
using BusinessServices.Interfaces;
using BusinessServices.Patterns.Factories;
using DataModal.DataClasses;
using DataModal.UnitOfWork;
using Resolver.Enumerations;
using Resolver.Exceptions;
using Resolver.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Services
{
    public class Skyco_AccountTypeServices : ISkyco_AccountTypeServices
    {
        #region Single
        private readonly UnitOfWork _unitOfWork;

        public Skyco_AccountTypeServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        public long Create(Skyco_AccountTypeBE Be)
        {
            try
            {
                Skyco_AccountTypes entity = Patterns.Factories.FactorySkyco_AccountType.GetInstance().CreateEntity(Be);

                Expression<Func<DataModal.DataClasses.Skyco_AccountTypes, Boolean>> predicate = u => u.AccountTypeName == entity.AccountTypeName;
                List<DataModal.DataClasses.Skyco_AccountTypes> entity1 = _unitOfWork.Skyco_AccountTypeRepository.GetAllByFilters(predicate, null).ToList();

                if (entity1.Count > 0)
                    throw new ApiBusinessException(100, "There is already an account type with that name", System.Net.HttpStatusCode.NotFound, "Http");

                _unitOfWork.Skyco_AccountTypeRepository.Create(entity);
                _unitOfWork.Commit();

                return entity.AccountTypeId;

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
                if (Id > 0)
                {
                    Expression<Func<DataModal.DataClasses.Skyco_AccountTypes, Boolean>> predicate = u => u.AccountTypeId == Id;
                    DataModal.DataClasses.Skyco_AccountTypes entity = _unitOfWork.Skyco_AccountTypeRepository.GetOneByFilters(predicate, null);

                    if (entity == null)
                        throw new ApiBusinessException(40, "The entity is not available", System.Net.HttpStatusCode.NotFound, "Http");

                    entity.Voided = (Int32)StateEnum.Deleted;
                    entity.VoidedBy = UserName;
                    entity.VoidedAt = DateTime.Now;

                    _unitOfWork.Skyco_AccountTypeRepository.Delete(entity, new List<string> { "Voided", "VoidedBy", "VoidedAt" });
                    _unitOfWork.Commit();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        public List<Skyco_AccountTypeBE> GetAll(int state, int page, int pageSize, string orderBy, string ascending, ref int count)
        {
            try
            {
                Expression<Func<DataModal.DataClasses.Skyco_AccountTypes, Boolean>> predicate = u => u.Voided == state;
                IQueryable<DataModal.DataClasses.Skyco_AccountTypes> entities = _unitOfWork.Skyco_AccountTypeRepository.GetAllByFilters(predicate, new string[] { "Skyco_Account" });

                count = entities.Count();
                var skipAmount = 0;
                if (page > 0)
                    skipAmount = pageSize * (page - 1);
                entities = entities
                   .OrderByPropertyOrField(orderBy, ascending)
                    .Skip(skipAmount)
                    .Take(pageSize);

                List<Skyco_AccountTypeBE> ListBe = new List<Skyco_AccountTypeBE>();
                foreach (Skyco_AccountTypes item in entities)
                {
                    ListBe.Add(Patterns.Factories.FactorySkyco_AccountType.GetInstance().CreateBusiness(item));
                }

                return ListBe;
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        public Skyco_AccountTypeBE GetById(long Id)
        {
            try
            {
                Expression<Func<DataModal.DataClasses.Skyco_AccountTypes, Boolean>> predicate = u => u.AccountTypeId == Id;
                DataModal.DataClasses.Skyco_AccountTypes entities = _unitOfWork.Skyco_AccountTypeRepository.GetOneByFilters(predicate, new string[] { "Skyco_Account"});
                if (entities != null)
                    return FactorySkyco_AccountType.GetInstance().CreateBusiness(entities);
                return null;
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        public bool Update(Skyco_AccountTypeBE Be)
        {
            try
            {
                DataModal.DataClasses.Skyco_AccountTypes entity = FactorySkyco_AccountType.GetInstance().CreateEntity(Be);
           
                _unitOfWork.Skyco_AccountTypeRepository.Update(entity, new List<string> { "UpdatedAt", "UpdatedBy", "AccountTypeName"});
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
