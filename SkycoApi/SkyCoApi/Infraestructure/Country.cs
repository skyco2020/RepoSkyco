using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace SkyCoApi.Infraestructure
{
    [Table("Country")]
    public partial class Country
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryId { get; set; }

        [StringLength(5)]
        public string CountryCode { get; set; }

        [StringLength(50)]
        public string CountryName { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
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

        #region List
        public  List<Location> Location { get; set; }
        public List<Provinces> Provinces { get; set; }
        #endregion
    }
}
