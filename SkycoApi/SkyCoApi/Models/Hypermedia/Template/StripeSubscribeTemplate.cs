using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;

namespace SkyCoApi.Models.Hypermedia.Template
{
    public class StripeSubscribeTemplate : BaseTemplate
    {
        #region Constructor
        private static StripeSubscribeTemplate _mytemplate;
        private StripeSubscribeTemplate() { }
        public static StripeSubscribeTemplate GetInstance()
        {
            if (_mytemplate == null)
                _mytemplate = new StripeSubscribeTemplate();
            return _mytemplate;
        }
        #endregion

        #region Gets
        public override Link GetMyOwnReference(Int64 ID)
        {
            return StripeSubscribe.CreateLink(new { id = ID });
        }

        public override Link GetMyCollectionReference()
        {
            return GetStripeSubscribes.CreateLink();
        }

        public override Link GetMyCollectionPagination()
        {
            return GetStripeSubscribePagination.CreateLink();
        }

        public override Link GetMyUpdateLink(Int64 ID)
        {
            return UpdateStripeSubscribe.CreateLink(new { id = ID });
        }

        public override Link GetMyDeleteLink(Int64 ID)
        {
            return DeleteStripeSubscribe.CreateLink(new { id = ID });
        }

        public override Link GetMyRelationReference()
        {
            return StripeSubscribesRelation;
        }
        #endregion

        #region Routes
        public static Link GetStripeSubscribes { get { return new Link("ref", baseaddress + "/StripeSubscribes"); } }
        public static Link StripeSubscribe { get { return new Link("self", baseaddress + "/StripeSubscribes/{id}"); } }
        public static Link StripeSubscribesRelation { get { return new Link("StripeSubscribe", baseaddress + "/StripeSubscribes/{id}"); } }
        public static Link UpdateStripeSubscribe { get { return new Link("update", baseaddress + "/StripeSubscribes/{id}"); } }
        public static Link DeleteStripeSubscribe { get { return new Link("delete", baseaddress + "/StripeSubscribes/{id}"); } }
        public static Link GetStripeSubscribePagination { get { return new Link("StripeSubscribes", baseaddress + "/StripeSubscribes/{?page}"); } }
        #endregion
    }
}