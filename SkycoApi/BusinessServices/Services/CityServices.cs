using BusinessEntities.BE;
using BusinessServices.Interfaces;
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
    public class CityServices : ICityServices
    {
        #region Constructor
        private readonly UnitOfWork _unitOfWork;
        public CityServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        public long Create(CityBE Be)
        {
            try
            {
                Cities entity = Patterns.Factories.FactoryCity.GetInstance().CreateEntity(Be);
                _unitOfWork.CityRepository.Create(entity);
                _unitOfWork.Commit();
                return entity.CityId;

            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        public bool Delete(long Id)
        {
            Expression<Func<DataModal.DataClasses.Cities, Boolean>> predicate = u => u.CityId == Id && u.Voided == (byte)StateEnum.Activated;
            Cities entity = _unitOfWork.CityRepository.GetOneByFilters(predicate, null);
            if (entity == null)
                throw new ApiBusinessException(1000, "Entity not found", System.Net.HttpStatusCode.NotFound, "Http");
            
            entity.Voided = (byte)StateEnum.Deleted;
            entity.VoidedAt = DateTime.Now;
            _unitOfWork.CityRepository.Delete(entity, new List<string>() { "Voided", "VoidedAt" });
            _unitOfWork.Commit();
            return true;
        }

        public List<CityBE> GetAll(int state, int page, int pageSize, string orderBy, string ascending, ref int count)
        {
            Expression<Func<DataModal.DataClasses.Cities, Boolean>> predicate = u => u.Voided == (byte)state;
            IQueryable<DataModal.DataClasses.Cities> entities = _unitOfWork.CityRepository.GetAllByFilters(predicate,null);

            count = entities.Count();
            var skipAmount = 0;
            if (page > 0)
                skipAmount = pageSize * (page - 1);

            entities = entities
                .OrderByPropertyOrField(orderBy, ascending)
                .Skip(skipAmount)
                .Take(pageSize);
            List<CityBE> listbe = new List<CityBE>();
            foreach (Cities item in entities)
            {
                listbe.Add(Patterns.Factories.FactoryCity.GetInstance().CreateBusiness(item));
            }
            return listbe;
        }

        public CityBE GetById(long Id)
        {
            Expression<Func<DataModal.DataClasses.Cities, Boolean>> predicate = u => u.CityId == Id && u.Voided == (byte)StateEnum.Activated;
            Cities entity = _unitOfWork.CityRepository.GetOneByFilters(predicate, null);
            CityBE be = null;
            if (entity != null)
            {
                be = new CityBE();
                be = Patterns.Factories.FactoryCity.GetInstance().CreateBusiness(entity);
            }
            return be;
        }

        public bool Update(long Id, CityBE Be)
        {
            try
            {
                if (Be == null)
                    throw new ApiBusinessException(1001, "Entity is not complete", System.Net.HttpStatusCode.NotFound, "Http");

                Cities entity = Patterns.Factories.FactoryCity.GetInstance().CreateEntity(Be);
                _unitOfWork.CityRepository.Update(entity, new List<String> { "CityName", "UpdatedAt", "UpdatedBy" });
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
