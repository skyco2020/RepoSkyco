using SkyCoApi.Models.Hypermedia.Template;
using SkyCoApi.Models.Representations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyCoApi.Models.DTO.Single
{
    public class PlanDTO : BaseRepresentation
    {
        #region Constructor
        public PlanDTO()
        {
            Rel = Mytemplate.GetMyRelationReference().Rel;
        }
        #endregion
        public Int64 PlanId { get; set; }
        public Int64 idProduct { get; set; }
        public String idplanstripe { get; set; }        
        public Int64 AccountId { get; set; }
        public Decimal Price { get; set; }
        public String Description { get; set; }
        public String TypePlan { get; set; }
        public DateTime PlanDate { get; set; }
        public Int32 state { get; set; }

        #region Relation
        public Skyco_AccountDTO Accounts { get; set; }
        public ProductDTO Products { get; set; }
        #endregion

        #region List
        #endregion

        #region Override & Hypermedia
        public override BaseTemplate Mytemplate
        {
            get
            {
                if (_mytemplate == null)
                    _mytemplate = PlanTemplate.GetInstance();
                return _mytemplate;
            }

            set
            {
                _mytemplate = value;
            }
        }

        public override Int64 IDRepresentation()
        {
            return PlanId;
        }

        protected override void CreateHypermedia()
        {
            Href = PlanTemplate.Plan.CreateLink(new { id = PlanId }).Href;
        }
        #endregion
    }
}