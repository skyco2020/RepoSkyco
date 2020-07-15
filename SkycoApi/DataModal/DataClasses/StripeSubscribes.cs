using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.DataClasses
{
    public class StripeSubscribes
    {
        [Key]
        public Int64 idStripeSubscribe { get; set; }
        public Int64 AccountId { get; set; }
        public String idPlanPriceStripe { get; set; }
        public String idStripeCustomer { get; set; }
        public String idSubscribe { get; set; }
        public String idCardStripe { get; set; }
        public String idTokenStripe { get; set; }
        public DateTime SubscribeDate{ get; set; }
        public Int32 state { get; set; }

        #region Relation
        [ForeignKey("AccountId")]
        public Skyco_Accounts Accounts { get; set; }
        #endregion
    }
}
