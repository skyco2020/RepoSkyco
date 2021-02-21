using BusinessEntities.BE;
using DataModal.DataClasses;
using DataModal.UnitOfWork;
using Resolver.Enumerations;
using Resolver.Exceptions;
using Resolver.QueryableExtensions;
using Stripe;
using StripeServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StripeServices.Services
{
    public class ProductServiceStripe: IProductServiceStripe
    {
        #region Constructor
        private readonly UnitOfWork _unitOfWork;
        public ProductServiceStripe(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Retrieve All plan

        public List<ProductBE> RetrieveAllProduct(bool active, int page, int top, string orderBy, string ascending, ref int count)
        {


            Expression<Func<DataModal.DataClasses.Products, Boolean>> predicate = u => u.active == active;
            IQueryable<DataModal.DataClasses.Products> entities = _unitOfWork.ProductRepository.GetAllByFilters(predicate, null /*new string[] { "Accounts", "Products" }*/);

            count = entities.Count();
            var skipAmount = 0;
            if (page > 0)
                skipAmount = top * (page - 1);

            entities = entities
                .OrderByPropertyOrField(orderBy, ascending)
                .Skip(skipAmount)
                .Take(top);
            List<ProductBE> listbe = new List<ProductBE>();

            foreach (Products item in entities)
            {
                listbe.Add(Patterns.Factories.FactoryProduct.GetInstance().CreateBusiness(item));
            }
            return listbe;
        }
        #endregion

        #region Retrieve a Product

        public async Task<dynamic> RetrieveProduct(Int64 productId)
        {
            #region Secret Key
            Key.SecretKey();
            #endregion
            Expression<Func<DataModal.DataClasses.StripeSubscribes, Boolean>> predicate = u => u.AccountId == productId;
            DataModal.DataClasses.StripeSubscribes entities = _unitOfWork.StripeSubscribeRepository.GetOneByFilters(predicate, null);
            if (entities == null)
                return null;
            ProductService service = new ProductService();
            Product product = service.Get("0NUbQGvXF32j1aWc3Kdu");
            return product;
        }
        #endregion

        #region Update Product

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

        #region Create a product

        public Int64 CreateProduct(ProductBE prod)
        {
            #region Secret Key
            Key.SecretKey();
            #endregion
            try
            {
                Products entity = Patterns.Factories.FactoryProduct.GetInstance().CreateEntity(prod);
                var options = new ProductCreateOptions
                {
                    Name = entity.name,
                    Description = entity.description,
                    Active = entity.active,
                    Metadata = new Dictionary<string, string>
                    {
                        { "product", entity.name },
                    },
                };
                ProductService service = new ProductService();
                Product product = service.Create(options);
                entity.idproductStripe = product.Id;
                return this.Insert(entity); ;
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
           
        }
        #endregion

        #region Delete

        public Boolean DeleteProduct(Int64 idproduct)
        {
            #region Secret Key
            Key.SecretKey();
            #endregion
            try
            {
                Expression<Func<DataModal.DataClasses.Products, Boolean>> predicate = u => u.idProduct == idproduct && u.active == true;
                Products entity = _unitOfWork.ProductRepository.GetOneByFilters(predicate, null);
                if (entity == null)
                    throw new ApiBusinessException(2000, "Entity not found", System.Net.HttpStatusCode.NotFound, "Http");

                ProductService service = new ProductService();
                Product productcreate = service.Delete(entity.idproductStripe);

                this.DeletePlanBD(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
           
        }
        #endregion

        #region Private Method
        private Int64 Insert(Products prod)
        {
            try
            {
                _unitOfWork.ProductRepository.Create(prod);
                _unitOfWork.Commit();
                return prod.idProduct;
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }

        }
        private void DeletePlanBD(Products entity)
        {
            try
            {  
                entity.active = false;
                _unitOfWork.ProductRepository.Delete(entity, new List<string>() { "active"});
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
