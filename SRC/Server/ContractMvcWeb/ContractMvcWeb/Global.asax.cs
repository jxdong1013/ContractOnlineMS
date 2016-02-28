using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ContractMvcWeb
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public MvcApplication()
        {
            //this.AuthenticateRequest += MvcApplication_AuthenticateRequest;
        }

        //void MvcApplication_AuthenticateRequest(object sender, EventArgs e)
        //{
        //    if (Context.User == null) return;
        //    var id = Context.User.Identity as  System.Web.Security.FormsIdentity;
        //    if (id != null && id.IsAuthenticated)
        //    {
        //        //var roles = id.Ticket.UserData.Split(',');
        //        System.Web.Script.Serialization.JavaScriptSerializer se = new System.Web.Script.Serialization.JavaScriptSerializer();
        //        List<ContractMvcWeb.Models.userrole> roles = se.Deserialize<List<ContractMvcWeb.Models.userrole>>(id.Ticket.UserData);
        //        string[] rs = null;
        //        if (roles != null)
        //        {
        //            rs = new string[roles.Count];
        //            int i=0;
        //            foreach (ContractMvcWeb.Models.userrole item in roles)
        //            {
        //                rs[i] = item.roleid.ToString();
        //            }
        //        }

        //        Context.User = new System.Security.Principal.GenericPrincipal(id, rs );
        //    }
        //}

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

     
    }
}