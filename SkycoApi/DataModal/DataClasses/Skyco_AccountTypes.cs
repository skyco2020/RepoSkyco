using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DataModal.DataClasses
{   
    public partial class Skyco_AccountTypes
    {
        #region Properties
        [Key]
        public Int64 AccountTypeId { get; set; }

        [StringLength(50)]
        public string AccountTypeName { get; set; }

        public DateTime CreatedAt { get; set; }

        [StringLength(30)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [StringLength(30)]
        public string UpdatedBy { get; set; }

        public DateTime? VoidedAt { get; set; }

        [StringLength(30)]
        public string VoidedBy { get; set; }

        public byte? Voided { get; set; }
        #endregion

        #region List
        public List<Skyco_Accounts> Skyco_Account { get; set; }
        #endregion
    }
}
