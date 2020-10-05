using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;

namespace SkyCoApi.Models.Hypermedia.Template
{
    public class PerfilTemplate : BaseTemplate
    {
        #region Constructor
        private static PerfilTemplate _mytemplate;
        private PerfilTemplate() { }
        public static PerfilTemplate GetInstance()
        {
            if (_mytemplate == null)
                _mytemplate = new PerfilTemplate();
            return _mytemplate;
        }
        #endregion

        #region Gets
        public override Link GetMyOwnReference(Int64 ID)
        {
            return Perfil.CreateLink(new { id = ID });
        }

        public override Link GetMyCollectionReference()
        {
            return GetPerfils.CreateLink();
        }

        public override Link GetMyCollectionPagination()
        {
            return GetPerfilPagination.CreateLink();
        }

        public override Link GetMyUpdateLink(Int64 ID)
        {
            return UpdatePerfil.CreateLink(new { id = ID });
        }

        public override Link GetMyDeleteLink(Int64 ID)
        {
            return DeletePerfil.CreateLink(new { id = ID });
        }

        public override Link GetMyRelationReference()
        {
            return PerfilsRelation;
        }
        #endregion

        #region Routes
        public static Link GetPerfils { get { return new Link("ref", baseaddress + "/Perfils"); } }
        public static Link Perfil { get { return new Link("self", baseaddress + "/Perfils/{id}"); } }
        public static Link PerfilsRelation { get { return new Link("Perfil", baseaddress + "/Perfils/{id}"); } }
        public static Link UpdatePerfil { get { return new Link("update", baseaddress + "/Perfils/{id}"); } }
        public static Link DeletePerfil { get { return new Link("delete", baseaddress + "/Perfils/{id}"); } }
        public static Link GetPerfilPagination { get { return new Link("Perfils", baseaddress + "/Perfils/{?page}"); } }
        #endregion
    }
}