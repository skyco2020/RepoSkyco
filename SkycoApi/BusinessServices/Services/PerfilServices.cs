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
    public class PerfilServices : IPerfilServices
    {
        #region Constructor
        private readonly UnitOfWork _unitOfWork;
        public PerfilServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        public long Create(PerfilBE Be)
        {
            try
            {
                Perfils entity = Patterns.Factories.FactoryPerfil.GetInstance().CreateEntity(Be);
                _unitOfWork.PerfilRepository.Create(entity);
                _unitOfWork.Commit();
                return entity.idPerfil;

            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        public bool Delete(long Id)
        {
            try
            {
                Expression<Func<DataModal.DataClasses.Perfils, Boolean>> predicate = u => u.idPerfil == Id && u.state == (Int32)StateEnum.Activated;
                Perfils entity = _unitOfWork.PerfilRepository.GetOneByFilters(predicate, null);
                if (entity == null)
                    throw new ApiBusinessException(2000, "Entity not found", System.Net.HttpStatusCode.NotFound, "Http");

                entity.state = (byte)StateEnum.Deleted;
                _unitOfWork.PerfilRepository.Delete(entity, new List<string>() { "state"});
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }
        public List<PerfilBE> GetAll(int state, int page, int pageSize, string orderBy, string ascending, Int64 AccountId, ref int count)
        {
            Expression<Func<DataModal.DataClasses.Perfils, Boolean>> predicate = u => u.state == (Int32)state &&  (u.AccountId == AccountId || AccountId == 0);
            IQueryable<DataModal.DataClasses.Perfils> entities = _unitOfWork.PerfilRepository.GetAllByFilters(predicate, null);

            count = entities.Count();
            var skipAmount = 0;
            if (page > 0)
                skipAmount = pageSize * (page - 1);

            entities = entities
                .OrderByPropertyOrField(orderBy, ascending)
                .Skip(skipAmount)
                .Take(pageSize);
            List<PerfilBE> listbe = new List<PerfilBE>();
            foreach (Perfils item in entities)
            {
                listbe.Add(Patterns.Factories.FactoryPerfil.GetInstance().CreateBusiness(item));
            }
            return listbe;
        }

        public PerfilBE GetById(long Id)
        {
            Expression<Func<DataModal.DataClasses.Perfils, Boolean>> predicate = u => u.idPerfil == Id && u.state == (Int64)StateEnum.Activated;
            Perfils entity = _unitOfWork.PerfilRepository.GetOneByFilters(predicate, null);
            PerfilBE be = null;
            if (entity != null)
            {
                be = new PerfilBE();
                be = Patterns.Factories.FactoryPerfil.GetInstance().CreateBusiness(entity);
            }
            return be;
        }

        public bool Update(PerfilBE Be)
        {
            try
            {
                if (Be == null)
                    throw new ApiBusinessException(2001, "Entity is not complete", System.Net.HttpStatusCode.NotFound, "Http");

                Perfils entity = Patterns.Factories.FactoryPerfil.GetInstance().CreateEntity(Be);
                _unitOfWork.PerfilRepository.Update(entity, new List<String> { "name", "complete"});
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
