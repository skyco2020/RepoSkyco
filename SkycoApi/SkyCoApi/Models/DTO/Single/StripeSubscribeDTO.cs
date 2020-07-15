using SkyCoApi.Models.Hypermedia.Template;
using SkyCoApi.Models.Representations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.DTO.Single
{
    public class StripeSubscribeDTO : BaseRepresentation
    {
        #region Constructor
        public StripeSubscribeDTO()
        {
            Rel = Mytemplate.GetMyRelationReference().Rel;
        }
        #endregion
        public Int64 idStripeSubscribe { get; set; }
        public Int64 AccountId { get; set; }
        public String idPlanPriceStripe { get; set; }
        public String idStripeCustomer { get; set; }
        public String idSubscribe { get; set; }
        public String idCardStripe { get; set; }
        public String idTokenStripe { get; set; }
        public DateTime SubscribeDate { get; set; }
        public Int32 state { get; set; }

        #region Relation
        public Skyco_AccountDTO Account { get; set; }
        #endregion

        #region Override & Hypermedia
        public override BaseTemplate Mytemplate
        {
            get
            {
                if (_mytemplate == null)
                    _mytemplate = StripeSubscribeTemplate.GetInstance();
                return _mytemplate;
            }

            set
            {
                _mytemplate = value;
            }
        }

        public override Int64 IDRepresentation()
        {
            return idStripeSubscribe;
        }

        protected override void CreateHypermedia()
        {
            Href = StripeSubscribeTemplate.StripeSubscribe.CreateLink(new { id = idStripeSubscribe }).Href;
        }
        #endregion
    }
}