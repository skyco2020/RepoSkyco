using SkyCoApi.Models.Hypermedia.Template;
using SkyCoApi.Models.Representations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.DTO.Single
{
    public class CardDTO : BaseRepresentation
    {
        #region Constructor
        public CardDTO()
        {
            Rel = Mytemplate.GetMyRelationReference().Rel;
        }
        #endregion

        public Int64 idcard { get; set; }
        public Int64 idtoken { get; set; }
        public String id { get; set; }
        public Int32 exp_month { get; set; }
        public Int32 exp_year { get; set; }
        public String address_city { get; set; }
        public String address_country { get; set; }
        public String address_line1 { get; set; }
        public String address_line1_check { get; set; }
        public String address_line2 { get; set; }
        public String address_state { get; set; }
        public String address_zip { get; set; }
        public String address_zip_check { get; set; }
        public String brand { get; set; }
        public String country { get; set; }
        public String cvc_check { get; set; }
        public String dynamic_last4 { get; set; }
        public String funding { get; set; }
        public String last4 { get; set; }
        public String name { get; set; }
        public String objectcard { get; set; }
        public String tokenization_method { get; set; }
        public Int32 state { get; set; }

        #region Relation
        public TokenDTO Tokens { get; set; }
        #endregion

        #region Override & Hypermedia
        public override BaseTemplate Mytemplate
        {
            get
            {
                if (_mytemplate == null)
                    _mytemplate = CardTemplate.GetInstance();
                return _mytemplate;
            }

            set
            {
                _mytemplate = value;
            }
        }

        public override Int64 IDRepresentation()
        {
            return idcard;
        }

        protected override void CreateHypermedia()
        {
            Href = CardTemplate.Card.CreateLink(new { id = idcard }).Href;
        }
        #endregion
    }
}