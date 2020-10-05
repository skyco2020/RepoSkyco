using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;

namespace SkyCoApi.Models.Hypermedia.Template
{
    public class PlanTemplate : BaseTemplate
    {
        #region Constructor
        private static PlanTemplate _mytemplate;
        private PlanTemplate() { }
        public static PlanTemplate GetInstance()
        {
            if (_mytemplate == null)
                _mytemplate = new PlanTemplate();
            return _mytemplate;
        }
        #endregion

        #region Gets
        public override Link GetMyOwnReference(Int64 ID)
        {
            return Plan.CreateLink(new { id = ID });
        }

        public override Link GetMyCollectionReference()
        {
            return GetPlans.CreateLink();
        }

        public override Link GetMyCollectionPagination()
        {
            return GetPlanPagination.CreateLink();
        }

        public override Link GetMyUpdateLink(Int64 ID)
        {
            return UpdatePlan.CreateLink(new { id = ID });
        }

        public override Link GetMyDeleteLink(Int64 ID)
        {
            return DeletePlan.CreateLink(new { id = ID });
        }

        public override Link GetMyRelationReference()
        {
            return PlansRelation;
        }
        #endregion

        #region Routes
        public static Link GetPlans { get { return new Link("ref", baseaddress + "/Plans"); } }
        public static Link Plan { get { return new Link("self", baseaddress + "/Plans/{id}"); } }
        public static Link PlansRelation { get { return new Link("Plan", baseaddress + "/Plans/{id}"); } }
        public static Link UpdatePlan { get { return new Link("update", baseaddress + "/Plans/{id}"); } }
        public static Link DeletePlan { get { return new Link("delete", baseaddress + "/Plans/{id}"); } }
        public static Link GetPlanPagination { get { return new Link("Plans", baseaddress + "/Plans/{?page}"); } }
        #endregion
    }
}