﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BE
{
    public class PlanBE: BaseBE
    {
        public Int64 AccountId { get; set; }
        public String idProductStripe { get; set; }
        public Decimal Price { get; set; }
        public String Description { get; set; }
        public String TypePlan { get; set; }
        public DateTime PlanDate { get; set; }

        #region Relation
        public Skyco_AccountBE Accounts { get; set; }
        #endregion
    }
}
