using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BE
{
    public class StripeSubscribeBE : BaseBE
    {
        public Int64 AccountId { get; set; }
        public String idPlanPriceStripe { get; set; }
        public String idStripeCustomer { get; set; }
        public String idSubscribe { get; set; }
        public String idCardStripe { get; set; }
        public String idTokenStripe { get; set; }
        public DateTime SubscribeDate { get; set; }

        #region Relation
        public Skyco_AccountBE Accounts { get; set; }
        #endregion
    }
}
