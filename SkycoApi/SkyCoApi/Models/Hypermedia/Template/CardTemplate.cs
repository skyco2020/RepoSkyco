using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;

namespace SkyCoApi.Models.Hypermedia.Template
{
    public class CardTemplate : BaseTemplate
    {
        #region Constructor
        private static CardTemplate _mytemplate;
        private CardTemplate() { }
        public static CardTemplate GetInstance()
        {
            if (_mytemplate == null)
                _mytemplate = new CardTemplate();
            return _mytemplate;
        }
        #endregion

        #region Gets
        public override Link GetMyOwnReference(Int64 ID)
        {
            return Card.CreateLink(new { id = ID });
        }

        public override Link GetMyCollectionReference()
        {
            return GetCards.CreateLink();
        }

        public override Link GetMyCollectionPagination()
        {
            return GetCardPagination.CreateLink();
        }

        public override Link GetMyUpdateLink(Int64 ID)
        {
            return UpdateCard.CreateLink(new { id = ID });
        }

        public override Link GetMyDeleteLink(Int64 ID)
        {
            return DeleteCard.CreateLink(new { id = ID });
        }

        public override Link GetMyRelationReference()
        {
            return CardsRelation;
        }
        #endregion

        #region Routes
        public static Link GetCards { get { return new Link("ref", baseaddress + "/Cards"); } }
        public static Link Card { get { return new Link("self", baseaddress + "/Cards/{id}"); } }
        public static Link CardsRelation { get { return new Link("Card", baseaddress + "/Cards/{id}"); } }
        public static Link UpdateCard { get { return new Link("update", baseaddress + "/Cards/{id}"); } }
        public static Link DeleteCard { get { return new Link("delete", baseaddress + "/Cards/{id}"); } }
        public static Link GetCardPagination { get { return new Link("Cards", baseaddress + "/Cards/{?page}"); } }
        #endregion
    }
}