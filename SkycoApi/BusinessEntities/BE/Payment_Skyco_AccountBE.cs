using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BE
{
    public class Payment_Skyco_AccountBE : BaseBE
    {
        public Int64 idPayment { get; set; }
        public Int64 AccountId { get; set; }
        public DateTime paymentdate { get; set; }
        public string idstripecard { get; set; }
        public Decimal Amount { get; set; }

        #region Relation
        public PaymentBE Payments { get; set; }
        public Skyco_AccountBE Accounts { get; set; }
        #endregion
    }
}
