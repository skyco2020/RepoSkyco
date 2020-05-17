using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DataModal.DataClasses
{
    public partial class Skyco_Phones
    {
        #region Properties
        [Key]
        public Int64 IdPhone { get; set; }
        public Int64 UserId { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        public byte? Preferred { get; set; }

        public DateTime? CreatedAt { get; set; }

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

        #region Relation
        [ForeignKey("UserId")]
        public Skyco_Users Skyco_User { get; set; }
        #endregion

    }
}
