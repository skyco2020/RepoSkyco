using System;
using System.Collections.Generic;

namespace BusinessEntities.BE
{
    public partial class Skyco_AddressBE
    {

        #region Properties
        public Int64 AddressId { get; set; }
        public Int64 UserId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime? VoidedAt { get; set; }
        public string VoidedBy { get; set; }

        public byte? Voided { get; set; }
        #endregion

        #region Relation
        public Skyco_UserBE Skyco_User { get; set; }
        #endregion

    }
}
