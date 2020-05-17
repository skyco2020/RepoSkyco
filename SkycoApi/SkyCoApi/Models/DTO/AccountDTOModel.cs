using SkyCoApi.Infraestructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.DTO
{
    public class AccountDTOModel
    {
        #region Account user
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

        public byte[] AccountImage { get; set; }

        [StringLength(255)]
        public string AccountImageUrl { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public bool? IsLoggedIn { get; set; }

        #endregion

        #region User

        [Required]
        [StringLength(50)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(50)]
        public string Lastname { get; set; }

        public byte? Gender { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }
        public string NumberAddress { get; set; }
        public DateTime DateOfBirth { get; set; }

        #endregion

        #region Phone
        public int IdPhone { get; set; }

        public byte? Preferred { get; set; }

        #endregion

        #region Base
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime? VoidedAt { get; set; }
        public string VoidedBy { get; set; }

        public byte? Voided { get; set; }
        #endregion
    }
}