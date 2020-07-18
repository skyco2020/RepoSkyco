using BusinessEntities.BE;
using BusinessServices.Interfaces;
using BusinessServices.Patterns.Factories;
using DataModal.DataClasses;
using DataModal.UnitOfWork;
using Resolver.Cryptography;
using Resolver.Enumerations;
using Resolver.Exceptions;
using Resolver.Mailing;
using Resolver.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Services
{
    public class Skyco_UsersServices : ISkyco_UsersServices
    {
        #region Single
        private readonly UnitOfWork _unitOfWork;

        public Skyco_UsersServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region location
        private Int64 CountryId = 0;
        private Int64 ProvinceId = 0;
        private Int64 CityId = 0;
        #endregion
        public long Subscribe(Skyco_UserBE Be)
        {
            try
            {
                Skyco_Users entity = Patterns.Factories.FactorySkyco_User.GetInstance().CreateEntity(Be);               

                Expression<Func<DataModal.DataClasses.Skyco_Users, Boolean>> predicate = u => u.EmailAddress == entity.EmailAddress;
                List<DataModal.DataClasses.Skyco_Users> entitynonerepeat = _unitOfWork.Skyco_UserRepository.GetAllByFilters(predicate, null).ToList();

                // Check if the customer was exist
                if (entitynonerepeat.Count > 0)
                {
                    Expression<Func<DataModal.DataClasses.Skyco_Accounts, Boolean>> predicateuser = u => u.Username == entity.EmailAddress;
                    List<DataModal.DataClasses.Skyco_Accounts> entity1 = _unitOfWork.SkycoAccountRepository.GetAllByFilters(predicateuser, new string[] { "Skyco_User", "Skyco_User.Skyco_Phone" }).ToList();
                    
                    if (entity1.Count > 0)
                        if (entity1.FirstOrDefault().Voided == (Int32)StateEnum.Deleted)
                            throw new ApiBusinessException((Int32)entity1.LastOrDefault().Skyco_User.UserId, "Welcome Back at Sky co", System.Net.HttpStatusCode.NotFound, "Http");
                        else
                            throw new ApiBusinessException((Int32)entity1.LastOrDefault().Skyco_User.UserId, "There is already an account with this email", System.Net.HttpStatusCode.NotFound, "Http");
                    else
                        throw new ApiBusinessException((Int32)entitynonerepeat.LastOrDefault().UserId, "Welcome to Sky co", System.Net.HttpStatusCode.NotFound, "Http");                    
                }
             

                _unitOfWork.Skyco_UserRepository.Create(entity);
                _unitOfWork.Commit();                           
                return entity.UserId;

            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        public long RegisterUserCompletely(Skyco_UserBE Be)
        {
            try
            {
                Skyco_Users entity = Patterns.Factories.FactorySkyco_User.GetInstance().CreateEntity(Be);

                Expression<Func<DataModal.DataClasses.Skyco_Accounts, Boolean>> predicateuser = u => u.Username == entity.EmailAddress;
                List<DataModal.DataClasses.Skyco_Accounts> entity1 = _unitOfWork.SkycoAccountRepository.GetAllByFilters(predicateuser, new string[] { "Skyco_User", "Skyco_User.Skyco_Phone" }).ToList();

                if (entity1.Count > 0)
                    if (entity1.FirstOrDefault().Voided == (Int32)StateEnum.Deleted)
                        throw new ApiBusinessException((Int32)entity1.LastOrDefault().Skyco_User.UserId, "There is already an account with this email", System.Net.HttpStatusCode.NotFound, "Http");
                if (entity.Skyco_Account.Count  > 0)
                {
                    if (this.GetLocation(Be))
                    {
                        foreach (var item in entity.Skyco_Account)
                        {
                            item.Location.CityId = this.CityId;
                            item.Location.CountryId = this.CountryId;
                            item.Location.ProvinceId = this.ProvinceId;
                        }
                    }

                    foreach (Skyco_Accounts item in entity.Skyco_Account)
                    {
                        _unitOfWork.SkycoAccountRepository.Create(item);
                    }
                }
                _unitOfWork.Skyco_UserRepository.Update(entity, new List<String> { "Firstname", "Lastname", "Gender", "Address", "NumberAddress", "DateOfBirth", "UpdatedAt", "UpdatedBy" });
                _unitOfWork.Commit();

                RegisterUserStateMail registerUserMail = new RegisterUserStateMail(Be.Firstname + " " + Be.Lastname, Be.Skyco_Account[0].Username, Be.Skyco_Account[0].PasswordHash, Be.Skyco_Account[0].EmailAddress);
                new SimpleMail().SendMail(registerUserMail);

                return entity.UserId;

            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        public bool Delete(long Id, String UserName)
        {
            try
            {
                if (Id > 0)
                {
                    Expression<Func<DataModal.DataClasses.Skyco_Users, Boolean>> predicate = u => u.UserId == Id;
                    DataModal.DataClasses.Skyco_Users entity = _unitOfWork.Skyco_UserRepository.GetOneByFilters(predicate, new string[] {"Skyco_Account", "Skyco_Address", "Skyco_Phone" });

                    if (entity == null)
                        throw new ApiBusinessException(40, "The entity is not available", System.Net.HttpStatusCode.NotFound, "Http");

                    if (entity.Skyco_Address != null)
                    {
                        foreach (Skyco_Addresses item in entity.Skyco_Address)
                        {
                            item.Voided = (Int32)StateEnum.Deleted;
                            item.VoidedBy = UserName;
                            item.VoidedAt = DateTime.Now;

                            _unitOfWork.Skyco_AddressRepository.Delete(item, new List<string> { "Voided", "VoidedBy", "VoidedAt" });
                        }
                    }

                    if (entity.Skyco_Account != null)
                    {
                        foreach (Skyco_Accounts item in entity.Skyco_Account)
                        {
                            item.Voided = (Int32)StateEnum.Deleted;
                            item.VoidedBy = UserName;
                            item.VoidedAt = DateTime.Now;

                            _unitOfWork.SkycoAccountRepository.Delete(item, new List<string> { "Voided", "VoidedBy", "VoidedAt" });
                        }
                    }

                    if (entity.Skyco_Phone != null)
                    {
                        foreach (Skyco_Phones item in entity.Skyco_Phone)
                        {
                            item.Voided = (Int32)StateEnum.Deleted;
                            item.VoidedBy = UserName;
                            item.VoidedAt = DateTime.Now;

                            _unitOfWork.Skyco_PhoneRepository.Delete(item, new List<string> { "Voided", "VoidedBy", "VoidedAt" });
                        }
                    }
                 
                    entity.Voided = (Int32)StateEnum.Deleted;
                    entity.VoidedBy = UserName;
                    entity.VoidedAt = DateTime.Now;

                    _unitOfWork.Skyco_UserRepository.Delete(entity, new List<string> { "Voided", "VoidedBy", "VoidedAt" });
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

        public List<Skyco_UserBE> GetAll(int state, int page, int pageSize, string orderBy, string ascending, ref int count)
        {
            try
            {
                Expression<Func<DataModal.DataClasses.Skyco_Users, Boolean>> predicate = u => u.Voided == state;
                IQueryable<DataModal.DataClasses.Skyco_Users> entities = _unitOfWork.Skyco_UserRepository.GetAllByFilters(predicate, new string[] { "Skyco_Account", "Skyco_Account.Location", "Skyco_Address", "Skyco_Phone" });
               
                count = entities.Count();
                var skipAmount = 0;
                if (page > 0)
                    skipAmount = pageSize * (page - 1);
                entities = entities
                   .OrderByPropertyOrField(orderBy, ascending)
                    .Skip(skipAmount)
                    .Take(pageSize);

                List<Skyco_UserBE> ListBe = new List<Skyco_UserBE>();
                foreach (Skyco_Users item in entities)
                {
                    ListBe.Add(Patterns.Factories.FactorySkyco_User.GetInstance().CreateBusiness(item));
                }

                return ListBe;
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        public Skyco_UserBE GetById(long Id)
        {
            try
            {
                Expression<Func<DataModal.DataClasses.Skyco_Users, Boolean>> predicate = u => u.UserId == Id;
                DataModal.DataClasses.Skyco_Users entities = _unitOfWork.Skyco_UserRepository.GetOneByFilters(predicate, new string[] { "Skyco_Account", "Skyco_Account.Location", "Skyco_Address", "Skyco_Phone" });
                if (entities != null)
                    return FactorySkyco_User.GetInstance().CreateBusiness(entities);
                return null;
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }
        public Skyco_UserBE GetByE_mail(String email)
        {
            try
            {
                Expression<Func<DataModal.DataClasses.Skyco_Users, Boolean>> predicate = u => u.Skyco_Account.Any( p => p.EmailAddress == email);
                DataModal.DataClasses.Skyco_Users entities = _unitOfWork.Skyco_UserRepository.GetOneByFilters(predicate, new string[] { "Skyco_Account", "Skyco_Account.Location", "Skyco_Address", "Skyco_Phone" });
                if (entities != null)
                    return FactorySkyco_User.GetInstance().CreateBusiness(entities);
                return null;
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }
        public bool Update(Skyco_UserBE Be)
        {
            try
            {
                DataModal.DataClasses.Skyco_Users entity = FactorySkyco_User.GetInstance().CreateEntity(Be);

                if (entity.Skyco_Address != null)
                {
                    foreach (Skyco_Addresses item in entity.Skyco_Address)
                    {
                        _unitOfWork.Skyco_AddressRepository.Update(item, new List<string> { "UpdatedAt", "UpdatedBy" });
                    }
                }               

                if (entity.Skyco_Phone != null)
                {
                    foreach (Skyco_Phones item in entity.Skyco_Phone)
                    {
                        _unitOfWork.Skyco_PhoneRepository.Update(item, new List<string> { "UpdatedAt", "UpdatedBy" , "PhoneNumber", "Preferred"});
                    }
                }
                if (entity.Skyco_Account != null)
                {
                    foreach (Skyco_Accounts item in entity.Skyco_Account)
                    {
                        _unitOfWork.SkycoAccountRepository.Update(item, new List<string> { "PasswordHash"});
                    }
                }
                _unitOfWork.Skyco_UserRepository.Update(entity, new List<String> { "Firstname", "Lastname", "Gender", "Address", "NumberAddress", "DateOfBirth", "UpdatedAt", "UpdatedBy" });
                _unitOfWork.Commit();  
                
                RegisterUserStateMail registerUserMail = new RegisterUserStateMail(Be.Firstname + " " + Be.Lastname, Be.Skyco_Account[0].Username, Be.Skyco_Account[0].PasswordHash, Be.Skyco_Account[0].EmailAddress);
                new SimpleMail().SendMail(registerUserMail);

                return true;
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }
        #region Private Method
        private Boolean GetLocation(Skyco_UserBE Be)
        {
            Boolean result = false;
            try
            {
                Countries country = _unitOfWork.Countryrepository.GetOneByFilters(u => u.CountryName.Trim().ToLower() == Be.Country.Trim().ToLower() && u.CountryName.Trim().ToLower() != null, new string[] { "Provinces", "Provinces.City" });
                if (country != null)
                {
                    this.CountryId = country.CountryId;
                    if (country.Provinces != null)
                    {
                        Provinces pro = country.Provinces.Find(p => p.ProvinceName.Trim().ToLower() == Be.province.Trim().ToLower() && p.ProvinceName != null);
                        this.ProvinceId = country.Provinces[0].ProvinceId;
                        if (pro.City != null)
                        {
                            this.CityId = pro.City.Find(x => x.CityName.Trim().ToLower() == Be.city.Trim().ToLower() && x.CityName != null).CityId;
                            result = true;
                        }
                        else
                        {
                            Cities entityCity = Patterns.Factories.FactoryCity.GetInstance().CreateEntity(Be.Skyco_Account[0].Location.Country.Province.FirstOrDefault().City.FirstOrDefault());
                            _unitOfWork.CityRepository.Create(entityCity);
                            this.CityId = entityCity.CityId;
                            result = true;
                        }
                    }
                    else
                    {
                        Provinces entityProvinces = Patterns.Factories.FactoryProvince.GetInstance().CreateEntity(Be.Skyco_Account[0].Location.Country.Province.FirstOrDefault());
                        _unitOfWork.ProvinceRepository.Create(entityProvinces);
                        this.ProvinceId = entityProvinces.ProvinceId;
                        this.CityId = entityProvinces.City[0].CityId;
                        result = true;
                    }
                }
                else
                {

                    Countries entitycountry = Patterns.Factories.FactoryCountry.GetInstance().CreateEntity(Be.Skyco_Account[0].Location.Country);
                    _unitOfWork.Countryrepository.Create(entitycountry);
                    this.CountryId = entitycountry.CountryId;
                    this.ProvinceId = entitycountry.Provinces[0].ProvinceId;
                    this.CityId = entitycountry.Provinces[0].City[0].CityId;
                    result = true;
                }
                return result;
            }
            catch (Exception)
            {
                return result;
            }
          
        }

        #endregion
    }
}
