using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DataModal.DataClasses
{
   
    public partial class Provinces
    {
        #region Properties
        [Key]
        public Int64 ProvinceId { get; set; }

        public Int64 CountryId { get; set; }

        [StringLength(10)]
        public string ProvinceName { get; set; }

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
        [ForeignKey("CountryId")]
        public Countries Country { get; set; }
        #endregion

        #region List
        public List<Cities> City { get; set; }
        public List<Locations> Location { get; set; }
        #endregion

    }
}
