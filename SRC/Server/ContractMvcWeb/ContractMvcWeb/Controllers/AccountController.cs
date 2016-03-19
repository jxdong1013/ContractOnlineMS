using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using ContractMvcWeb.Models;
using ContractMvcWeb.Models.Beans;

namespace ContractMvcWeb.Controllers
{
    //[MyAuthorize]
    public class AccountController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        public JsonResult LoginRestfull(string userName, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                {
                    Result result = new Result((int)ResultCodeEnum.Error, "用户名或密码空!", null);
                    JsonResult jr = new JsonResult();
                    jr.Data = result;
                    jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    return jr;
                }
                AccountContext dbContext = new AccountContext();
                ContractMvcWeb.Models.Beans.User userObj = dbContext.Login(userName, password);
                if (userObj != null)
                {
                    //List<userrole> roles = dbcontext.GetRoles(userobj.userid);
                    //System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                    //string jsstr = js.Serialize(roles);
                    Result result = null;
                    JsonResult jr = null;
                    if (userObj.enable == (int)EnableEnum.DISABLE)
                    {
                        result = new Result((int)ResultCodeEnum.Error ,"该用户被禁止登录。",null);
                        jr = new JsonResult();
                        jr.Data = result;
                        return jr;
                    }

                    int expiration = 0;
                    if (!int.TryParse(System.Configuration.ConfigurationManager.AppSettings["Expiration"].ToString(), out expiration))
                    {
                        expiration = 30;
                    }

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now,
                        DateTime.Now.AddMinutes(expiration), false, string.Empty);

                    string ticketEncrypt = FormsAuthentication.Encrypt(ticket);
                    System.Web.HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketEncrypt);
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);

                    result = new Result((int)ResultCodeEnum.Success, "", userObj);
                    jr = new JsonResult();
                    jr.Data = result;
                    return jr;
                }
                else
                {
                    Result result = new Result((int)ResultCodeEnum.Error, "用户名或密码错误!", null);
                    JsonResult jResult = new JsonResult();
                    jResult.Data = result;
                    return jResult;
                }
            }
            catch (Exception ex)
            {
                JsonResult jr = new JsonResult();
                jr.Data = new Result((int)ResultCodeEnum.Error, ex.Message, "");
                return jr;
            }
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult LogoutRestfull()
        {
            Result result = null;
            try
            {
                FormsAuthentication.SignOut();

                if (HttpContext.Response.Cookies != null)
                {
                    int count = HttpContext.Response.Cookies.Count;
                    for(int i =0;i<count ;i++ )
                    {
                        HttpCookie c = HttpContext.Response.Cookies[i];
                        c.Expires = DateTime.Now.AddDays(-1);
                    }
                    HttpContext.Response.Cookies.Clear();                       
                }
            }
            catch (Exception ex)
            {                    
                result = new Result((int)ResultCodeEnum.Error, ex.Message,null );
                JsonResult jr3 = new JsonResult();
                jr3.Data = result;
                return jr3;
            }


            result = new Result((int)ResultCodeEnum.Success, "", null);
            JsonResult jr = new JsonResult();
            jr.Data = result;
            return jr;
        }      
       
    }
}
