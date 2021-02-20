using BusinessEntities.BE;
using DataModal.DataClasses;
using DataModal.UnitOfWork;
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

        public async Task<dynamic> CreateProduct(PruductStripeBE prod)
        {
            #region Secret Key
            Key.SecretKey();
            #endregion
            var options = new ProductCreateOptions
            {
                Name = prod.name,
                Description = prod.description,
                Active = prod.active,
                Metadata = new Dictionary<string, string>
                {
                    { "product", prod.name },
                },


            };
            ProductService service = new ProductService();
            Product product = service.Create(options);
            return product;
        }
        #endregion

        #region Update Delete

        //public async Task<dynamic> DeletePlan(PlanProduct plan)
        //{
        //    #region Secret Key
        //    Key.SecretKey();
        //    #endregion

        //    PlanCreateOptions options = new PlanCreateOptions
        //    {
        //        Amount = plan.Price,
        //        Currency = "usd",
        //        Interval = "month",
        //        Product = plan.idProductStripe,
        //    };
        //    var service = new PlanService();
        //    Plan plancreate = service.Create(options);
        //    return plancreate;
        //}
        #endregion

        #region Private Method
        private void Insert(Products prod)
        {
            try
            {
                _unitOfWork.ProductRepository.Create(prod);
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
