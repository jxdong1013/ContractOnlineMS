using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ContractMvcWeb.Attributes
{
    public class MyAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //return base.AuthorizeCore(httpContext);
            //获得当前的验证cookie   
            if (httpContext.User.Identity.IsAuthenticated == false)
            {
                return false;
            }

            HttpCookie authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            //如果当前的cookie为空，则返回。   
            if (authCookie == null || authCookie.Value == "")
            {
                return false;
            }
            FormsAuthenticationTicket authTicket;
            try
            {
                //对当前的cookie进行解密   
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch
            {
                return false;
            }

            if (string.IsNullOrEmpty(Roles)) return true;

            if (authTicket != null)
            {
                System.Web.Script.Serialization.JavaScriptSerializer ds = new System.Web.Script.Serialization.JavaScriptSerializer();
                //var userRoles = ds.Deserialize< List<ContractMvcWeb.Models.userrole>>( authTicket.UserData);
                if (authTicket.UserData == null || authTicket.UserData.ToString() == "" ) return false;
                //if (userRoles == null) return false;

                var roles = Roles.Split(new[] { ',' }).ToList();
                String userData = authTicket.UserData.ToString();

                //return roles.Any(x => userRoles.Exists((i) => { return i.roleid.ToString() == x; }));
                return roles.Any(x => { return userData.Contains(x); });
            }
            return false;  
        }

        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            //base.HandleUnauthorizedRequest(filterContext);
            if (filterContext.HttpContext.User.Identity.IsAuthenticated == false)
            {                                                                                             
                string baclurl = filterContext.HttpContext.Request.AppRelativeCurrentExecutionFilePath;
                filterContext.Result = new System.Web.Mvc.RedirectResult(FormsAuthentication.LoginUrl + "?returnUrl=" + baclurl);
                return;
            }

            string rootPath = filterContext.HttpContext.Request.ApplicationPath;
            string path = rootPath.EndsWith("/") ? rootPath + "Home/NotVisit" : rootPath + "/Home/NotVisit";
            filterContext.Result = new System.Web.Mvc.RedirectResult( path );
        }
    }

}