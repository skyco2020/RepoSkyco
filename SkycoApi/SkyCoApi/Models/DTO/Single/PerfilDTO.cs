using SkyCoApi.Models.Hypermedia.Template;
using SkyCoApi.Models.Representations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.DTO.Single
{
    public class PerfilDTO : BaseRepresentation
    {
        #region Constructor
        public PerfilDTO()
        {
            Rel = Mytemplate.GetMyRelationReference().Rel;
        }
        #endregion
        public Int64 idPerfil { get; set; }
        public Int64 AccountId { get; set; }
        public String name { get; set; }
        public Boolean complete { get; set; }
        public Int32 typeperfil { get; set; }
        public Int32 state { get; set; }

        #region Relation
        public Skyco_AccountDTO Account { get; set; }
        #endregion

        #region Relation
        public Skyco_AccountDTO Accounts { get; set; }
        #endregion

        #region List
        #endregion

        #region Override & Hypermedia
        public override BaseTemplate Mytemplate
        {
            get
            {
                if (_mytemplate == null)
                    _mytemplate = PerfilTemplate.GetInstance();
                return _mytemplate;
            }

            set
            {
                _mytemplate = value;
            }
        }

        public override Int64 IDRepresentation()
        {
            return idPerfil;
        }

        protected override void CreateHypermedia()
        {
            Href = PerfilTemplate.Perfil.CreateLink(new { id = idPerfil }).Href;
        }
        #endregion
    }
}