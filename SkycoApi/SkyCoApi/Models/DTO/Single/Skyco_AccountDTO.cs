using SkyCoApi.Models.Hypermedia.Template;
using SkyCoApi.Models.Representations;
using System;
using System.Collections.Generic;

namespace SkyCoApi.Models.DTO.Single
{
    public partial class Skyco_AccountDTO : BaseRepresentation
    {
        #region Constructor
        public Skyco_AccountDTO()
        {
            Rel = Mytemplate.GetMyRelationReference().Rel;
        }
        #endregion

        #region Properties
        public Int64 AccountId { get; set; }

        public Int64 UserId { get; set; }
        public Int64 AccountTypeId { get; set; }
        public Int64 LocationId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string PassowrdSalt { get; set; }
        public string PasswordHash { get; set; }
        public string refreshtoken { get; set; }

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
        public Skyco_AccountTypeDTO Skyco_AccountType { get; set; }       
        public Skyco_UserDTO Skyco_User { get; set; }
        public LocationDTO Location { get; set; }
        #endregion

        #region Override & Hypermedia
        public override BaseTemplate Mytemplate
        {
            get
            {
                if (_mytemplate == null)
                    _mytemplate = Skyco_AccountTemplate.GetInstance();
                return _mytemplate;
            }

            set
            {
                _mytemplate = value;
            }
        }

        public override Int64 IDRepresentation()
        {
            return AccountId;
        }

        protected override void CreateHypermedia()
        {
            Href = Skyco_AccountTemplate.Skyco_Account.CreateLink(new { id = AccountId }).Href;
        }
        #endregion
    }
}
