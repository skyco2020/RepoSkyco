using SkyCoApi.Models.Hypermedia.Template;
using SkyCoApi.Models.Representations;
using System;
using System.Collections.Generic;

namespace SkyCoApi.Models.DTO.Single
{
    public partial class Skyco_UserDTO : BaseRepresentation
    {
        #region Constructor
        public Skyco_UserDTO()
        {
            Rel = Mytemplate.GetMyRelationReference().Rel;
        }
        #endregion
        #region Prperties
        public Int64 UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public byte? Gender { get; set; }
        public string Address { get; set; }
        public string NumberAddress { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime? VoidedAt { get; set; }
        public string VoidedBy { get; set; }

        public byte? Voided { get; set; }
        #endregion

        #region List       
        public List<Skyco_AccountDTO> Skyco_Account { get; set; }
        public List<Skyco_AddressDTO> Skyco_Address { get; set; }
        public List<Skyco_PhoneDTO> Skyco_Phone { get; set; }
        #endregion
        #region Override & Hypermedia
        public override BaseTemplate Mytemplate
        {
            get
            {
                if (_mytemplate == null)
                    _mytemplate = Skyco_UserTemplate.GetInstance();
                return _mytemplate;
            }

            set
            {
                _mytemplate = value;
            }
        }

        public override Int64 IDRepresentation()
        {
            return UserId;
        }

        protected override void CreateHypermedia()
        {
            Href = Skyco_UserTemplate.Skyco_User.CreateLink(new { id = UserId }).Href;
        }
        #endregion
    }
}
