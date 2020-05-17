using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace SkyCoApi.Infraestructure
{   
    public partial class Provinces
    {
        #region Properties
        [Key]
        public int ProviceId { get; set; }

        public int CountryId { get; set; }

        [StringLength(10)]
        public string ProvinceName { get; set; }

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

        #region Relation
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        #endregion

        #region List
        public virtual ICollection<City> City { get; set; }
        public virtual ICollection<Location> Location { get; set; }
        #endregion

    }
}
