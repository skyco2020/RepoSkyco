using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace SkyCoApi.Infraestructure
{
    public partial class Skyco_Address
    {
        #region Properties
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AddressId { get; set; }

        public DateTime? CreatedAt { get; set; }

        [StringLength(30)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [StringLength(30)]
        public string UpdatedBy { get; set; }

        public DateTime? VoidedAt { get; set; }

        [StringLength(10)]
        public string VoidedBy { get; set; }

        public byte? Voided { get; set; }
        #endregion

        #region Relation
        [ForeignKey("UserId")]
        public Skyco_Users Skyco_User { get; set; }
        #endregion

    }
}
