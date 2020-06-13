using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BE
{
    public class PaymentBE : BaseBE
    {
        public string idstripecard { get; set; }
        public Int64 AccountId { get; set; }
        public Int64 idcard { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public DateTime paymentdate { get; set; }
        public string name { get; set; }
        public Int32 Quantity { get; set; }
        public string Description { get; set; }
    }
}
