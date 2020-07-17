using BusinessEntities.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Interfaces
{
    public interface IStripeCardServices
    {
        dynamic Create(PaymentIntentBE Be);
        dynamic Update(PaymentIntentBE Be);
        Task<dynamic> GetAllPricePlan(int count);
    }
}
