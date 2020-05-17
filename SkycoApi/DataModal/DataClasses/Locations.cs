using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DataModal.DataClasses
{
    [Table("Locations")]
    public partial class Locations
    {
        #region Properties
        [Key]
        public Int64 LocationId { get; set; }

        public Int64 CountryId { get; set; }
        public Int64 ProvinceId { get; set; }
        public Int64 CityId { get; set; }

        [StringLength(255)]
        public string AddressName { get; set; }

        [StringLength(10)]
        public string AddressNumber { get; set; }

        [StringLength(255)]
        public string Appartement { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

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
        [ForeignKey("CityId")]
        public Cities City { get; set; }
        [ForeignKey("CountryId")]
        public Countries Country { get; set; }
        [ForeignKey("ProvinceId")]
        public Provinces Provinces { get; set; }
        #endregion
              
    }
}
