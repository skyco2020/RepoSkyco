using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace DataModal.DataClasses
{
   
    [Table("Cities")]
    public partial class Cities
    {
        #region Properties
        [Key]
        public Int64 CityId { get; set; }

        public Int64 ProvinceId { get; set; }

        [StringLength(50)]
        public string CityName { get; set; }

        public DateTime? CreatedAt { get; set; }

        [StringLength(30)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [StringLength(30)]
        public string UpdatedBy { get; set; }

        public DateTime? VoidedAt { get; set; }
        public byte? Voided { get; set; }

        [StringLength(30)]
        public string VoidedBy { get; set; }
        #endregion

        #region Relation
        [ForeignKey("ProvinceId")]
        public Provinces Provinces { get; set; }
        #endregion

        #region List
        public List<Locations> Location { get; set; }
        #endregion
    }
}
