using BusinessEntities.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeServices.Interfaces
{
    public interface ISubscribrStripeCardPaymentServices
    {
        Task<dynamic> PayAsync(PaymentIntentBE Be);
        dynamic Update(PaymentIntentBE Be);
        Task<dynamic> GetAllPricePlan(int count);
        Task<dynamic> Retrievecard(Int64 accoutId);
        Task<dynamic> Retrievesubscription(Int64 accoutId);
    }
}
