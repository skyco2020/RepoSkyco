using BusinessEntities.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interfaces
{
    public interface IStripeSubscribeServices
    {
        StripeSubscribeBE GetById(Int64 Id);
        List<StripeSubscribeBE> GetAll(Int32 state, Int32 page, Int32 top, String orderBy, String ascending, ref Int32 count);
        Int64 Create(StripeSubscribeBE Be);
        Boolean Update(StripeSubscribeBE Be);
        Boolean Delete(Int64 Id, String UserName);
    }
}
