using System;
using System.Collections.Generic;

namespace BusinessEntities.BE
{
    public partial class Skyco_AccountBE
    {
        #region Properties
        public Int64 AccountId { get; set; }

        public Int64 UserId { get; set; }
        public Int64 AccountTypeId { get; set; }
        public Int64 LocationId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }

        public byte? AccountType { get; set; }

        public byte? AccountState { get; set; }
        public byte?[] AccountImage { get; set; }
        public string AccountImageUrl { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public bool? IsLoggedIn { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime? VoidedAt { get; set; }
        public string VoidedBy { get; set; }

        public byte? Voided { get; set; }
        #endregion

        #region Relation
        public Skyco_AccountTypeBE Skyco_AccountType { get; set; }       
        public Skyco_UserBE Skyco_User { get; set; }
        public LocationBE Location { get; set; }
        #endregion

        #region List
        public List<Payment_Skyco_AccountBE> Payment_Skyco_Accounts { get; set; }
        #endregion
    }
}
