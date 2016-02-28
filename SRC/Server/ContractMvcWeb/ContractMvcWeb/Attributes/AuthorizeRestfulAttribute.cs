using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;
using System.Web.Security;

namespace ContractMvcWeb.Attributes
{
    public class AuthorizeRestfulAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //return base.AuthorizeCore(httpContext);
            //获得当前的验证cookie   
            if (httpContext.User.Identity.IsAuthenticated == false)
            {
                return false;
            }

            //获得当前的验证cookie   
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
                var userRoles = ds.Deserialize<List<ContractMvcWeb.Models.userrole>>(authTicket.UserData);
                if (userRoles == null) return false;

                var roles = Roles.Split(new[] { ',' }).ToList();                             

                return roles.Any(x => userRoles.Exists((i) => { return i.roleid.ToString() == x; }));
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated == false)
            {
                Models.Result res2 = new Models.Result((int)Models.ResultCodeEnum.Error, "请登录以后在操作。",null);
                string temp2 = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(res2);
                filterContext.HttpContext.Response.Write(temp2);
                filterContext.HttpContext.Response.End();
                return;
            }
            //base.HandleUnauthorizedRequest(filterContext);
            Models.Result res = new Models.Result ( (int)Models.ResultCodeEnum.Error ,"你无权访问。" ,null);
            string temp = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(res);
            filterContext.HttpContext.Response.Write(temp);
            filterContext.HttpContext.Response.End();
        }
    }
}