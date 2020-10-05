
using DataModal.DataClasses;
using DataModal.GenericRepository;
using DataModal.Repositories.Interface;
using DataModal.Repositories.Repository;
using DataModal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        #region Member
        private SkyCoDbContext context = null;
        private bool disposed = false;
        #endregion

        #region Constructor
        public UnitOfWork()
        {
            context = new SkyCoDbContext();
        }

        public SkyCoDbContext GetNewContext()
        {
            return new SkyCoDbContext();
        }

        public SkyCoGenericRepository<T> getRepository<T>() where T : class
        {
            return new SkyCoGenericRepository<T>(context);
        }

        #endregion

        #region Commit
        public void Commit()
        {
            context.SaveChanges();
        }
        #endregion

        #region Dispose
        protected virtual void Dispose(bool disposed)
        {
            if (!this.disposed)
            {
                if (disposed)
                {
                    context.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion


        #region Relation Repository

        #region Members 
        private ICityRepository _CityRepository;
        private ICountryRepository _CountryRepository;
        private ILocationRepository _LocationRepository;
        private IStripeSubscribeRepository _StripeSubscribeRepository;
        private IPlanRepository _PlanRepository;
        private IProvinceRepository _ProvinceRepository;
        private ISkyco_AccountRepository _Skyco_AccountRepository;
        private ISkyco_AccountTypeRepository _Skyco_AccountTypeRepository;
        private ISkyco_AddressRepository _Skyco_AddressRepository;
        private ISkyco_PhoneRepository _Skyco_PhoneRepository;
        private ISkyco_UserRepository _Skyco_UserRepository;
        private IPerfilRepository _PerfilRepository;

        #endregion


        #region Properties
        public ICityRepository CityRepository
        {
            get
            {
                if (_CityRepository == null)
                {
                    return _CityRepository = new CityRepository(context);
                }
                return _CityRepository;
            }

            set
            {
                _CityRepository = value;
            }
        }  
        public ICountryRepository Countryrepository
        {
            get
            {
                if (_CountryRepository == null)
                {
                    _CountryRepository = new CountryRepository(context);
                }
                return _CountryRepository;
            }
            set => _CountryRepository = value;
        }
        public ILocationRepository LocationRepository
        {
            get
            {
                if (_LocationRepository == null)
                {
                    _LocationRepository = new LocationRepository(context);
                }
                return _LocationRepository;
            }
            set { _LocationRepository = value; }
        }
        public IStripeSubscribeRepository StripeSubscribeRepository
        {
            get
            {
                if (_StripeSubscribeRepository == null)
                {
                    _StripeSubscribeRepository = new StripeSubscribeRepository(context);
                }
                return _StripeSubscribeRepository;
            }
            set { _StripeSubscribeRepository = value; }
        }
        public IPlanRepository PlanRepository
        {
            get
            {
                if (_PlanRepository == null)
                {
                    _PlanRepository = new PlanRepository(context);
                }
                return _PlanRepository;
            }
            set { _PlanRepository = value; }
        }
        public IProvinceRepository ProvinceRepository
        {
            get
            {
                if (_ProvinceRepository == null)
                {
                    return _ProvinceRepository = new ProvinceRepository(context);
                }
                return _ProvinceRepository;
            }

            set
            {
                _ProvinceRepository = value;
            }
        }

        public ISkyco_AccountRepository SkycoAccountRepository
        {
            get
            {
                if (_Skyco_AccountRepository == null)
                {
                    return _Skyco_AccountRepository = new Skyco_AccountRepository(context);
                }
                return _Skyco_AccountRepository;
            }

            set
            {
                _Skyco_AccountRepository = value;
            }
        }

        public ISkyco_AddressRepository Skyco_AddressRepository
        {
            get
            {
                if (_Skyco_AddressRepository == null)
                {
                    return _Skyco_AddressRepository = new Skyco_AddressRepository(context);
                }
                return _Skyco_AddressRepository;
            }

            set
            {
                _Skyco_AddressRepository = value;
            }
        }
      
        public ISkyco_AccountTypeRepository Skyco_AccountTypeRepository
        {
            get
            {
                if (_Skyco_AccountTypeRepository == null)
                {
                    return _Skyco_AccountTypeRepository = new Skyco_AccountTypeRepository(context);
                }
                return _Skyco_AccountTypeRepository;
            }

            set
            {
                _Skyco_AccountTypeRepository = value;
            }
        }

        public ISkyco_PhoneRepository Skyco_PhoneRepository
        {
            get
            {
                if (_Skyco_PhoneRepository == null)
                {
                    return _Skyco_PhoneRepository = new Skyco_PhoneRepository(context);
                }
                return _Skyco_PhoneRepository;
            }

            set
            {
                _Skyco_PhoneRepository = value;
            }
        }

        public ISkyco_UserRepository Skyco_UserRepository
        {
            get
            {
                if (_Skyco_UserRepository == null)
                {
                    return _Skyco_UserRepository = new Skyco_UserRepository(context);
                }
                return _Skyco_UserRepository;
            }

            set
            {
                _Skyco_UserRepository = value;
            }
        }

        public IPerfilRepository PerfilRepository
        {
            get
            {
                if (_PerfilRepository == null)
                {
                    return _PerfilRepository = new PerfilRepository(context);
                }
                return _PerfilRepository;
            }

            set
            {
                _PerfilRepository = value;
            }
        }

        #endregion

        #endregion
    }
}
