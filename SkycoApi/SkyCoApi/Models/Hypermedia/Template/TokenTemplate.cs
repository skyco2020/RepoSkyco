using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;

namespace SkyCoApi.Models.Hypermedia.Template
{
    public class TokenTemplate : BaseTemplate
    {
        #region Constructor
        private static TokenTemplate _mytemplate;
        private TokenTemplate() { }
        public static TokenTemplate GetInstance()
        {
            if (_mytemplate == null)
                _mytemplate = new TokenTemplate();
            return _mytemplate;
        }
        #endregion

        #region Gets
        public override Link GetMyOwnReference(Int64 ID)
        {
            return Token.CreateLink(new { id = ID });
        }

        public override Link GetMyCollectionReference()
        {
            return GetTokens.CreateLink();
        }

        public override Link GetMyCollectionPagination()
        {
            return GetTokenPagination.CreateLink();
        }

        public override Link GetMyUpdateLink(Int64 ID)
        {
            return UpdateToken.CreateLink(new { id = ID });
        }

        public override Link GetMyDeleteLink(Int64 ID)
        {
            return DeleteToken.CreateLink(new { id = ID });
        }

        public override Link GetMyRelationReference()
        {
            return TokensRelation;
        }
        #endregion

        #region Routes
        public static Link GetTokens { get { return new Link("ref", baseaddress + "/Tokens"); } }
        public static Link Token { get { return new Link("self", baseaddress + "/Tokens/{id}"); } }
        public static Link TokensRelation { get { return new Link("Token", baseaddress + "/Tokens/{id}"); } }
        public static Link UpdateToken { get { return new Link("update", baseaddress + "/Tokens/{id}"); } }
        public static Link DeleteToken { get { return new Link("delete", baseaddress + "/Tokens/{id}"); } }
        public static Link GetTokenPagination { get { return new Link("Tokens", baseaddress + "/Tokens/{?page}"); } }
        #endregion
    }
}