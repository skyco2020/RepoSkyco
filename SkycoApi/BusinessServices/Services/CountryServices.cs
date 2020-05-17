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
    public class CountryServices : ICountryServices
    {
        #region Constructor
        private readonly UnitOfWork _unitOfWork;
        public CountryServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        public long Create(CountryBE Be)
        {
            try
            {
                Countries entity = Patterns.Factories.FactoryCountry.GetInstance().CreateEntity(Be);
                _unitOfWork.Countryrepository.Create(entity);
                _unitOfWork.Commit();
                return entity.CountryId;

            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        public long CreateCountryAuto(CountryBE Be)
        {
            try
            {
                Countries entity = Patterns.Factories.FactoryCountry.GetInstance().CreateEntity(Be);
                Countries country = _unitOfWork.Countryrepository.GetOneByFilters(u => u.CountryName == entity.CountryName);
                if (country != null)
                {
                    if (entity.Provinces != null)
                    {
                        foreach (Provinces prov in entity.Provinces)
                        {
                            Provinces province = _unitOfWork.ProvinceRepository.GetOneByFilters(u => u.ProvinceName == prov.ProvinceName);
                            if (province != null)
                            {
                                if (prov.City != null)
                                {
                                    foreach (Cities city in prov.City)
                                    {
                                        Cities cit = _unitOfWork.CityRepository.GetOneByFilters(u => u.CityName == city.CityName);
                                        if (cit == null)
                                        {
                                            city.ProvinceId = prov.ProvinceId;
                                            _unitOfWork.CityRepository.Create(city);
                                        }
                                        else
                                            entity.CountryId = country.CountryId;
                                    }
                                }                                
                            }
                            else
                            {
                                prov.CountryId = country.CountryId;
                                entity.CountryId = country.CountryId;
                                _unitOfWork.ProvinceRepository.Create(prov);
                                _unitOfWork.Commit();
                            }
                        }
                    }
                   
                }
                else
                {
                    _unitOfWork.Countryrepository.Create(entity);
                    _unitOfWork.Commit();
                }
                return entity.CountryId;

            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        public bool Delete(Int64 Id, String UserName)
        {
            Expression<Func<DataModal.DataClasses.Countries, Boolean>> predicate = u => u.CountryId == Id && u.Voided == (byte)StateEnum.Activated;
            Countries entity = _unitOfWork.Countryrepository.GetOneByFilters(predicate, new string[] { "Provinces","Provinces.City" });
            if (entity == null)
                throw new ApiBusinessException(1000, "Entity not found", System.Net.HttpStatusCode.NotFound, "Http");
            if (entity.Provinces != null)
            {
                foreach (Provinces item in entity.Provinces)
                {
                    if (item.City != null)
                    {
                        foreach (Cities city in item.City)
                        {
                            city.Voided = (byte)StateEnum.Deleted;
                            city.VoidedAt = DateTime.Now;
                            city.VoidedBy = UserName;
                            _unitOfWork.CityRepository.Delete(city, new List<string>() { "Voided", "VoidedAt", "VoidedBy" });
                        }
                    }
                    item.Voided = (byte)StateEnum.Deleted;
                    item.VoidedAt = DateTime.Now;
                    item.VoidedBy = UserName;
                    _unitOfWork.ProvinceRepository.Delete(item, new List<string>() { "Voided", "VoidedAt", "VoidedBy" });
                }
            }
            entity.Voided = (byte)StateEnum.Deleted;
            entity.VoidedAt = DateTime.Now;
            entity.VoidedBy = UserName;
            _unitOfWork.Countryrepository.Delete(entity, new List<string>() { "Voided", "VoidedAt", "VoidedBy" });
            _unitOfWork.Commit();
            return true;
        }

        public List<CountryBE> GetAll(int state, int page, int top, string orderBy, string ascending, ref int count)
        {
            Expression<Func<DataModal.DataClasses.Countries, Boolean>> predicate = u =>u.Voided == (byte)state;
            IQueryable<DataModal.DataClasses.Countries> entities = _unitOfWork.Countryrepository.GetAllByFilters(predicate, new string[] { "Provinces", "Provinces.City" });

            count = entities.Count();
            var skipAmount = 0;
            if (page > 0)
                skipAmount = top * (page - 1);

            entities = entities
                .OrderByPropertyOrField(orderBy, ascending)
                .Skip(skipAmount)
                .Take(top);
            List<CountryBE> listbe = new List<CountryBE>();
            foreach (Countries item in entities)
            {
                listbe.Add(Patterns.Factories.FactoryCountry.GetInstance().CreateBusiness(item));
            }
            return listbe;
        }

        public CountryBE GetById(Int64 Id)
        {
            Expression<Func<DataModal.DataClasses.Countries, Boolean>> predicate = u => u.CountryId == Id && u.Voided == (byte)StateEnum.Activated;
            Countries entity = _unitOfWork.Countryrepository.GetOneByFilters(predicate, new string[] { "Provinces", "Provinces.City" });
            CountryBE be = null;
            if (entity != null)
            {
                be = new CountryBE();
                be = Patterns.Factories.FactoryCountry.GetInstance().CreateBusiness(entity);
            }
            return be;
        }
        public bool Update(CountryBE Be)
        {            
            try
            {
                if (Be == null)
                    throw new ApiBusinessException(1001, "Entity is not complete", System.Net.HttpStatusCode.NotFound, "Http");

                Countries entity = Patterns.Factories.FactoryCountry.GetInstance().CreateEntity(Be);
                _unitOfWork.Countryrepository.Update(entity, new List<String> { "CountryName", "CountryCode", "UpdatedAt","UpdatedBy" });
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
