using SkyCoApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace SkyCoApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Bootstrapper.Initialise();
        }      
        void Application_EndRequest(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(HelperModelLog.user))
            {
                if (Application.Get(HelperModelLog.user) == null)
                {
                    Application.Set(HelperModelLog.user, 1);
                }
                else if (HelperModelLog.state == "Login" && HelperModelLog.user != null)
                {
                    Int32 cant = (Int32)Application.Get(HelperModelLog.user) + 1;
                    Application.Set(HelperModelLog.user, cant);
                }
                else if (HelperModelLog.state == "Close" && HelperModelLog.user != null)
                {
                    Int32 cant = (Int32)Application.Get(HelperModelLog.user) - 1;
                    Application.Set(HelperModelLog.user, cant);
                }
                HelperModelLog.state = null;
                HelperModelLog.user = null;
            }
        }

        public Int32 GetCountSreen()
        {
            Int32 count = 1;
            if (Application.Get(HelperModelLog.user) != null)
                count = (Int32)Application.Get(HelperModelLog.user);
            return count;
        }
    }
}
