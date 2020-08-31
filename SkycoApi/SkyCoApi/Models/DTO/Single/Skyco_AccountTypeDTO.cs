using SkyCoApi.Models.Hypermedia.Template;
using SkyCoApi.Models.Representations;
using System;
using System.Collections.Generic;

namespace SkyCoApi.Models.DTO.Single
{   
    public partial class Skyco_AccountTypeDTO : BaseRepresentation
    {
        #region Properties
        public Int64 AccountTypeId { get; set; }
        public string AccountTypeName { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime? VoidedAt { get; set; }
        public string VoidedBy { get; set; }

        public byte? Voided { get; set; }
        #endregion

        #region List
        public List<Skyco_AccountDTO> Skyco_Account { get; set; }
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
                    _mytemplate = Skyco_AccountTypeTemplate.GetInstance();
                return _mytemplate;
            }

            set
            {
                _mytemplate = value;
            }
        }

        public override Int64 IDRepresentation()
        {
            return AccountTypeId;
        }

        protected override void CreateHypermedia()
        {
            Href = Skyco_AccountTypeTemplate.Skyco_AccountType.CreateLink(new { id = AccountTypeId }).Href;
        }
        #endregion
    }
}
