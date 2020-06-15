using SkyCoApi.Models.Hypermedia.Template;
using SkyCoApi.Models.Representations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.DTO.Single
{
    public class PaymentDTO : BaseRepresentation
    {
        #region Constructor
        public PaymentDTO()
        {
            Rel = Mytemplate.GetMyRelationReference().Rel;
        }
        #endregion

        public Int64 idpayment { get; set; }
        public Int64 idcard { get; set; }
        public Int64 PlanId { get; set; }
        public String name { get; set; }
        public String Description { get; set; }
        public String Currency { get; set; }
        public Int32 Quantity { get; set; }
        public Int32 state { get; set; }
        #region List
        public List<Payment_Skyco_AccountDTO> Payment_Skyco_Accounts { get; set; }
        #endregion

        #region Override & Hypermedia
        public override BaseTemplate Mytemplate
        {
            get
            {
                if (_mytemplate == null)
                    _mytemplate = PaymentTemplate.GetInstance();
                return _mytemplate;
            }

            set
            {
                _mytemplate = value;
            }
        }

        public override Int64 IDRepresentation()
        {
            return idpayment;
        }

        protected override void CreateHypermedia()
        {
            Href = PaymentTemplate.Payment.CreateLink(new { id = idpayment }).Href;
        }
        #endregion
    }
}