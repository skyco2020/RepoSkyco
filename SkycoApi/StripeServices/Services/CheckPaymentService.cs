using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeServices.Services
{
    public static class CheckPaymentService
    {
        public static Boolean CheckPayMent(String subId)
        {
            #region Secret Key
            Key.SecretKey();
            #endregion          

            var service1 = new SubscriptionService();
            Subscription Subs = service1.Get(subId);
            if (Subs.Items.Count() > 0)
            {
                DateTime date = Subs.Items.Data.LastOrDefault().Created;
                if (date.AddMonths(1) >= DateTime.Today)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}
