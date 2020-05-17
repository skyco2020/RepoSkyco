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
    public class ProvinceServices : IProvinceServices
    {
        #region Constructor
        private readonly UnitOfWork _unitOfWork;
        public ProvinceServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        public long Create(ProvinceBE Be)
        {
            try
            {
                Provinces entity = Patterns.Factories.FactoryProvince.GetInstance().CreateEntity(Be);
                _unitOfWork.ProvinceRepository.Create(entity);
                _unitOfWork.Commit();
                return entity.ProvinceId;

            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        public bool Delete(long Id)
        {
            Expression<Func<DataModal.DataClasses.Provinces, Boolean>> predicate = u => u.ProvinceId == Id && u.Voided == (byte)StateEnum.Activated;
            Provinces entity = _unitOfWork.ProvinceRepository.GetOneByFilters(predicate, new string[] {"City" });
            if (entity == null)
                throw new ApiBusinessException(1000, "Entity not found", System.Net.HttpStatusCode.NotFound, "Http");
            if (entity.City != null)
            {                
                foreach (Cities city in entity.City)
                {
                    city.Voided = (byte)StateEnum.Deleted;
                    city.VoidedAt = DateTime.Now;
                    _unitOfWork.CityRepository.Delete(city, new List<string>() { "Voided", "VoidedAt" });
                }
            }
            entity.Voided = (byte) StateEnum.Deleted;
            entity.VoidedAt = DateTime.Now;
            _unitOfWork.ProvinceRepository.Delete(entity, new List<string>() { "Voided", "VoidedAt" });
            _unitOfWork.Commit();
            return true;
        }

        public List<ProvinceBE> GetAll(int state, int page, int pageSize, string orderBy, string ascending, ref int count, long idcountry)
        {
            Expression<Func<DataModal.DataClasses.Provinces, Boolean>> predicate = u => u.Voided == (byte)state;
            IQueryable<DataModal.DataClasses.Provinces> entities = _unitOfWork.ProvinceRepository.GetAllByFilters(predicate, new string[] { "City" });

            count = entities.Count();
            var skipAmount = 0;
            if (page > 0)
                skipAmount = pageSize * (page - 1);

            entities = entities
                .OrderByPropertyOrField(orderBy, ascending)
                .Skip(skipAmount)
                .Take(pageSize);
            List<ProvinceBE> listbe = new List<ProvinceBE>();
            foreach (Provinces item in entities)
            {
                listbe.Add(Patterns.Factories.FactoryProvince.GetInstance().CreateBusiness(item));
            }
            return listbe;
        }

        public ProvinceBE GetById(long Id)
        {
            Expression<Func<DataModal.DataClasses.Provinces, Boolean>> predicate = u => u.CountryId == Id && u.Voided == (byte)StateEnum.Activated;
            Provinces entity = _unitOfWork.ProvinceRepository.GetOneByFilters(predicate, new string[] { "City" });
            ProvinceBE be = null;
            if (entity != null)
            {
                be = new ProvinceBE();
                be = Patterns.Factories.FactoryProvince.GetInstance().CreateBusiness(entity);
            }
            return be;
        }

        public bool Update(ProvinceBE Be)
        {
            try
            {
                if (Be == null)
                    throw new ApiBusinessException(1001, "Entity is not complete", System.Net.HttpStatusCode.NotFound, "Http");

                Provinces entity = Patterns.Factories.FactoryProvince.GetInstance().CreateEntity(Be);
                _unitOfWork.ProvinceRepository.Update(entity, new List<String> { "ProvinceName","UpdatedAt", "UpdatedBy" });
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
