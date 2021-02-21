using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BE
{
    public class PlanBE: BaseBE
    {
        public Int64 idProduct { get; set; }
        public Int64 AccountId { get; set; }
        public String idplanstripe { get; set; }
        public String TypePlan { get; set; }
        public Int64 Price { get; set; }
        public String Description { get; set; }
        public String Motive { get; set; }
        public DateTime PlanDate { get; set; }
        public Boolean State { get; set; }

        #region Relation
        public Skyco_AccountBE Accounts { get; set; }
        public ProductBE Products { get; set; }

        #endregion
    }
}
