using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace SkyCoApi.Infraestructure
{
   
    [Table("City")]
    public partial class City
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }

        public int? ProviceId { get; set; }

        [StringLength(50)]
        public string CityName { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(30)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [StringLength(30)]
        public string UpdatedBy { get; set; }

        public DateTime? VoidedAt { get; set; }
        public byte? Voided { get; set; }

        [StringLength(10)]
        public string VoidedBy { get; set; }
        #endregion

        #region Relation
        [ForeignKey("ProviceId")]
        public Provinces Provinces { get; set; }
        #endregion

        #region List
        public List<Locations> Location { get; set; }
        #endregion
    }
}
