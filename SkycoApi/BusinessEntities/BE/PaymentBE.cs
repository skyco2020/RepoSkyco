using Resolver.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BE
{
    public class PaymentBE : BaseBE
    {
        public Int64 idcard { get; set; }
        public Int64 PlanId { get; set; }
        public String name { get; set; }
        public String Description { get; set; }
        public String Currency { get; set; }
        public Int32 Quantity { get; set; }

        #region List
        public List<Payment_Skyco_AccountBE> Payment_Skyco_Accounts { get; set; }
        #endregion
    }
}
