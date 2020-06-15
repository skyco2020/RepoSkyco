using SkyCoApi.Models.Hypermedia.Template;
using SkyCoApi.Models.Representations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.DTO.Single
{
    public class Payment_Skyco_AccountDTO : BaseRepresentation
    {
        #region Constructor
        public Payment_Skyco_AccountDTO()
        {
            Rel = Mytemplate.GetMyRelationReference().Rel;
        }
        #endregion
        public Int64 IdPaymentUser { get; set; }
        public Int64 idPayment { get; set; }
        public Int64 AccountId { get; set; }
        public DateTime paymentdate { get; set; }
        public string idstripecard { get; set; }
        public Decimal Amount { get; set; }
        public Int32 state { get; set; }

        #region Relation
        public PaymentDTO Payments { get; set; }
        public Skyco_AccountDTO Account { get; set; }
        #endregion

        #region Override & Hypermedia
        public override BaseTemplate Mytemplate
        {
            get
            {
                if (_mytemplate == null)
                    _mytemplate = Payment_Skyco_AccountTemplate.GetInstance();
                return _mytemplate;
            }

            set
            {
                _mytemplate = value;
            }
        }

        public override Int64 IDRepresentation()
        {
            return IdPaymentUser;
        }

        protected override void CreateHypermedia()
        {
            Href = Payment_Skyco_AccountTemplate.Payment_Skyco_Account.CreateLink(new { id = IdPaymentUser }).Href;
        }
        #endregion
    }
}