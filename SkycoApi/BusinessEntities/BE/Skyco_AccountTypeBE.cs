using System;
using System.Collections.Generic;

namespace BusinessEntities.BE
{   
    public partial class Skyco_AccountTypeBE
    {
        #region Properties
        public Int64 AccountTypeId { get; set; }
        public string AccountTypeName { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime? VoidedAt { get; set; }
        public string VoidedBy { get; set; }

        public byte? Voided { get; set; }
        #endregion

        #region List
        public List<Skyco_AccountBE> Skyco_Account { get; set; }
        #endregion
    }
}
