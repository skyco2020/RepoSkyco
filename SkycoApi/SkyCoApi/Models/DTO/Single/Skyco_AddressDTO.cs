using SkyCoApi.Models.Hypermedia.Template;
using SkyCoApi.Models.Representations;
using System;
using System.Collections.Generic;

namespace SkyCoApi.Models.DTO.Single
{
    public partial class Skyco_AddressDTO : BaseRepresentation
    {
        #region Constructor
        public Skyco_AddressDTO()
        {
            Rel = Mytemplate.GetMyRelationReference().Rel;
        }
        #endregion

        #region Properties
        public Int64 AddressId { get; set; }
        public Int64 UserId { get; set; }

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
                    _mytemplate = Skyco_AddressTemplate.GetInstance();
                return _mytemplate;
            }

            set
            {
                _mytemplate = value;
            }
        }

        public override Int64 IDRepresentation()
        {
            return AddressId;
        }

        protected override void CreateHypermedia()
        {
            Href = Skyco_AddressTemplate.Skyco_Address.CreateLink(new { id = AddressId }).Href;
        }
        #endregion
    }
}
