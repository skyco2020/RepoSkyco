using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.DataClasses
{
    public class Payment_Skyco_Accounts
    {
        [Key]
        public Int64 IdPaymentUser { get; set; }
        public Int64 idPayment { get; set; }
        public Int64 AccountId { get; set; }
        public DateTime paymentdate { get; set; }
        public string idstripecard { get; set; }
        public Decimal Amount { get; set; }
        public Int32 state { get; set; }

        #region Relation
        [ForeignKey("idPayment")]
        public Payments Payments { get; set; }
        [ForeignKey("AccountId")]
        public Skyco_Accounts Accounts { get; set; }
        #endregion
    }
}
