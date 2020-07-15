using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.DataClasses
{
    public class Plans
    {
        [Key]
        public Int64 PlanId { get; set; }
        public String idProductStripe { get; set; }
        public Int64 AccountId { get; set; }
        public String TypePlan { get; set; }
        public Decimal Price { get; set; }
        public String Description { get; set; }
        public DateTime PlanDate { get; set; }
        public Int32 state { get; set; }

        #region Relation
        [ForeignKey("AccountId")]
        public Skyco_Accounts Accounts { get; set; }
        #endregion
    }
}
