using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace SkyCoApi.Infraestructure
{
    public partial class Skyco_Account
    {
        #region Properties
        [Key]
        public long AccountId { get; set; }

        public long UserId { get; set; }

        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [StringLength(30)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(30)]
        public string PassowrdSalt { get; set; }

        [Required]
        [StringLength(30)]
        public string PasswordHash { get; set; }

        public byte AccountType { get; set; }

        public byte? AccountState { get; set; }

        [Column(TypeName = "image")]
        public byte[] AccountImage { get; set; }

        [StringLength(255)]
        public string AccountImageUrl { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public bool? IsLoggedIn { get; set; }

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
        [ForeignKey("AccountType")]
        public  Skyco_AccountType Skyco_AccountType { get; set; }
        [ForeignKey("UserId")]
        public  Skyco_User Skyco_User { get; set; }
    }
    #endregion

}
