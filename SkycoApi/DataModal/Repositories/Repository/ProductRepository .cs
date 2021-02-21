using DataModal.DataClasses;
using DataModal.GenericRepository;
using DataModal.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.Repositories.Repository
{
    public class ProductRepository : SkyCoGenericRepository<Products>, IProductRepository
    {
        public ProductRepository(SkyCoDbContext context) : base(context)
        {
        }

        public override void Delete(Products entity, List<string> modifiedfields)
        {
            Products prod = dbcontext.Products.Find(entity.idProduct);
            prod.active = entity.active;

            dbcontext.Products.Attach(prod);
            base.Delete(prod, modifiedfields);
        }

        public override void Update(Products entity, List<string> modifiedfields)
        {
            Products prod = dbcontext.Products.Find(entity.idProduct);
            prod.name = entity.name;
            prod.urlimg = entity.urlimg;
            prod.description = entity.description;

            dbcontext.Products.Attach(prod);
            base.Update(prod, modifiedfields);    
        }
    }
}
