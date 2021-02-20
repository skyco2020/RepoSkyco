using BusinessEntities.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeServices.Interfaces
{
    public interface IProductServiceStripe
    {
        List<ProductBE> RetrieveAllProduct(bool active, int page, int top, string orderBy, string ascending, ref int count);
    }
}
