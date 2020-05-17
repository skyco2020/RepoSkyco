using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DataModal.DataClasses
{
    public partial class Skyco_Users
    {
        #region Prperties
        [Key]
        public Int64 UserId { get; set; }

        [StringLength(50)]
        public string Firstname { get; set; }

        [StringLength(50)]
        public string Lastname { get; set; }

        public byte? Gender { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(50)]
        public string NumberAddress { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

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

        #region List       
        public List<Skyco_Accounts> Skyco_Account { get; set; }
        public List<Skyco_Addresses> Skyco_Address { get; set; }
        public List<Skyco_Phones> Skyco_Phone { get; set; }
        #endregion
    }
}
