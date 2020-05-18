using SkyCoApi.Models.Hypermedia.Template;
using SkyCoApi.Models.Representations;
using System;
using System.Collections.Generic;

namespace SkyCoApi.Models.DTO.Single
{
    public partial class Skyco_PhoneDTO : BaseRepresentation
    {
        #region Constructor
        public Skyco_PhoneDTO()
        {
            Rel = Mytemplate.GetMyRelationReference().Rel;
        }
        #endregion
        #region Properties
        public Int64 IdPhone { get; set; }
        public Int64 UserId { get; set; }
        public string PhoneNumber { get; set; }

        public byte? Preferred { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime? VoidedAt { get; set; }
        public string VoidedBy { get; set; }

        public byte? Voided { get; set; }
        #endregion

        #region Relation
        public Skyco_UserDTO Skyco_User { get; set; }
        #endregion

        #region Override & Hypermedia
        public override BaseTemplate Mytemplate
        {
            get
            {
                if (_mytemplate == null)
                    _mytemplate = Skyco_PhoneTemplate.GetInstance();
                return _mytemplate;
            }

            set
            {
                _mytemplate = value;
            }
        }

        public override Int64 IDRepresentation()
        {
            return IdPhone;
        }

        protected override void CreateHypermedia()
        {
            Href = Skyco_PhoneTemplate.Skyco_Phone.CreateLink(new { id = IdPhone }).Href;
        }
        #endregion
    }
}
