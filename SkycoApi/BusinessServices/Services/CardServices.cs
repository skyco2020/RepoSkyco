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
    public class CardServices : ICardServices
    {
        #region Constructor
        private readonly UnitOfWork _unitOfWork;
        public CardServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        public long Create(CardBE Be)
        {
            try
            {
                Cards entity = Patterns.Factories.FactoryCard.GetInstance().CreateEntity(Be);
                _unitOfWork.CardRepository.Create(entity);
                _unitOfWork.Commit();
                return entity.idcard;

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
                Expression<Func<DataModal.DataClasses.Cards, Boolean>> predicate = u => u.idcard == Id && u.state == (Int32)StateEnum.Activated;
                Cards entity = _unitOfWork.CardRepository.GetOneByFilters(predicate, null);
                if (entity == null)
                    throw new ApiBusinessException(1000, "Entity not found", System.Net.HttpStatusCode.NotFound, "Http");

                entity.state = (Int32)StateEnum.Deleted;
                _unitOfWork.CardRepository.Delete(entity, new List<string>() { "state" });
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        public List<CardBE> GetAll(int state, int page, int top, string orderBy, string ascending, ref int count)
        {
            Expression<Func<DataModal.DataClasses.Cards, Boolean>> predicate = u => u.state == state;
            IQueryable<DataModal.DataClasses.Cards> entities = _unitOfWork.CardRepository.GetAllByFilters(predicate, new string[] { "Tokens" });

            count = entities.Count();
            var skipAmount = 0;
            if (page > 0)
                skipAmount = top * (page - 1);

            entities = entities
                .OrderByPropertyOrField(orderBy, ascending)
                .Skip(skipAmount)
                .Take(top);
            List<CardBE> listbe = new List<CardBE>();
            foreach (Cards item in entities)
            {
                listbe.Add(Patterns.Factories.FactoryCard.GetInstance().CreateBusiness(item));
            }
            return listbe;
        }

        public CardBE GetById(long Id)
        {
            Expression<Func<DataModal.DataClasses.Cards, Boolean>> predicate = u => u.idcard == Id && u.state == (Int32)StateEnum.Activated;
            Cards entity = _unitOfWork.CardRepository.GetOneByFilters(predicate, new string[] { "Accounts" });
            CardBE be = null;
            if (entity != null)
            {
                be = new CardBE();
                be = Patterns.Factories.FactoryCard.GetInstance().CreateBusiness(entity);
            }
            return be;
        }

        public bool Update(CardBE Be)
        {
            try
            {
                if (Be == null)
                    throw new ApiBusinessException(1001, "Entity is not complete", System.Net.HttpStatusCode.NotFound, "Http");

                Cards entity = Patterns.Factories.FactoryCard.GetInstance().CreateEntity(Be);
                _unitOfWork.CardRepository.Update(entity, new List<String> { "id", "exp_month", "exp_year", "address_city", "address_country", "address_line1",
                            "address_line1_check","address_line2","address_state","address_zip","address_zip_check","brand","country",
                            "cvc_check","dynamic_last4","funding","last4","name","objectcard","tokenization_method" });
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
