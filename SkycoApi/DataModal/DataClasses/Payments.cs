using Resolver.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.DataClasses
{
    public class Payments
    {
        [Key]
        public Int64 idpayment { get; set; }
        public Int64 idcard { get; set; }
        public Int64 PlanId { get; set; }
        public String name { get; set; }
        public String Description { get; set; }
        public string Currency { get; set; }
        public Int32 Quantity { get; set; }
        public Int32 state { get; set; }

        #region Relation

        [ForeignKey("idcard")]
        public Cards Cards { get; set; }

        [ForeignKey("PlanId")]
        public Plans Plans { get; set; }
        #endregion

        #region List
        public List<Payment_Skyco_Accounts> Payment_Skyco_Accounts { get; set; }
        #endregion
    }
}
