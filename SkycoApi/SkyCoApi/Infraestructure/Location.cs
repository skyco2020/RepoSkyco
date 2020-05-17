using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace SkyCoApi.Infraestructure
{
    [Table("Location")]
    public partial class Location
    {
        #region Properties
        [Key]
        public long LocationId { get; set; }

        public int CityId { get; set; }

        [StringLength(255)]
        public string AddressName { get; set; }

        [StringLength(10)]
        public string AddressNumber { get; set; }

        [StringLength(255)]
        public string Appartement { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

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

        //public int ProvinceId { get; set; }

        //public int CountryId { get; set; }
        #endregion

        #region Relation
        [ForeignKey("CityId")]
        public City City { get; set; }
        //[ForeignKey("CountryId")]
        //public Country Country { get; set; }
        //[ForeignKey("ProvinceId")]
        //public Provinces Provinces { get; set; }
        #endregion

        #region List
        public virtual ICollection<Skyco_Address> Skyco_Address { get; set; }
        #endregion
    }
}
