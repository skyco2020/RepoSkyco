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
    public class TokenServices : ITokenServices
    {
        #region Constructor
        private readonly UnitOfWork _unitOfWork;
        public TokenServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        public long Create(TokenBE Be)
        {
            try
            {
                Tokens entity = Patterns.Factories.FactoryToken.GetInstance().CreateEntity(Be);
                _unitOfWork.TokenRepository.Create(entity);
                _unitOfWork.Commit();
                return entity.idtoken;

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
                Expression<Func<DataModal.DataClasses.Tokens, Boolean>> predicate = u => u.idtoken == Id && u.state == (Int32)StateEnum.Activated;
                Tokens entity = _unitOfWork.TokenRepository.GetOneByFilters(predicate, null);
                if (entity == null)
                    throw new ApiBusinessException(1000, "Entity not found", System.Net.HttpStatusCode.NotFound, "Http");
                if (entity.cards != null)
                {
                    foreach (Cards item in entity.cards)
                    {
                        item.state = (Int32)StateEnum.Deleted;
                        _unitOfWork.CardRepository.Delete(item, new List<string>() { "state" });
                    }
                }
                entity.state = (Int32)StateEnum.Deleted;
                _unitOfWork.TokenRepository.Delete(entity, new List<string>() { "state"});
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        public List<TokenBE> GetAll(int state, int page, int top, string orderBy, string ascending, ref int count)
        {
            Expression<Func<DataModal.DataClasses.Tokens, Boolean>> predicate = u => u.state == state;
            IQueryable<DataModal.DataClasses.Tokens> entities = _unitOfWork.TokenRepository.GetAllByFilters(predicate, new string[] { "cards" });

            count = entities.Count();
            var skipAmount = 0;
            if (page > 0)
                skipAmount = top * (page - 1);

            entities = entities
                .OrderByPropertyOrField(orderBy, ascending)
                .Skip(skipAmount)
                .Take(top);
            List<TokenBE> listbe = new List<TokenBE>();
            foreach (Tokens item in entities)
            {
                listbe.Add(Patterns.Factories.FactoryToken.GetInstance().CreateBusiness(item));
            }
            return listbe;
        }

        public TokenBE GetById(long Id)
        {
            Expression<Func<DataModal.DataClasses.Tokens, Boolean>> predicate = u => u.idtoken == Id && u.state == (Int32)StateEnum.Activated;
            Tokens entity = _unitOfWork.TokenRepository.GetOneByFilters(predicate, new string[] { "cards" });
            TokenBE be = null;
            if (entity != null)
            {
                be = new TokenBE();
                be = Patterns.Factories.FactoryToken.GetInstance().CreateBusiness(entity);
            }
            return be;
        }

        public bool Update(TokenBE Be)
        {
            try
            {
                if (Be == null)
                    throw new ApiBusinessException(1001, "Entity is not complete", System.Net.HttpStatusCode.NotFound, "Http");

                Tokens entity = Patterns.Factories.FactoryToken.GetInstance().CreateEntity(Be);
                if (entity.cards != null)
                {
                    foreach (Cards item in entity.cards)
                    {
                        _unitOfWork.CardRepository.Update(item, new List<string>() { "id", "exp_month", "exp_year", "address_city", "address_country", "address_line1",
                            "address_line1_check","address_line2","address_state","address_zip","address_zip_check","brand","country",
                            "cvc_check","dynamic_last4","funding","last4","name","objectcard","tokenization_method" });
                    }
                }
                _unitOfWork.TokenRepository.Update(entity, new List<String> { "id", "client_ip", "created", "livemode", "objectcart", "type", "used" });
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
