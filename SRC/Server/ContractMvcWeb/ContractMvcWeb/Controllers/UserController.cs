using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ContractMvcWeb.Attributes;
using ContractMvcWeb.Models.Beans;
using ContractMvcWeb.Models;
using System.Web.Security;

namespace ContractMvcWeb.Controllers
{
    [MyAuthorize]
    public class UserController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AccountContext dbContext = new AccountContext();
                User userObj = dbContext.Login(model.UserName, model.Password);
                if (userObj != null)
                {
                    //List<userrole> roles = dbContext.GetRoles(personObj.userid);                    
                    //System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                    //string jsstr = js.Serialize(roles); 
                    if (userObj.enable == (int)EnableEnum.DISABLE)
                    {
                        ModelState.AddModelError("", "该用户被禁止登录,请联系管理员。");
                        return View(model);  
                    }

                    int expiration = 0;
                    if (!int.TryParse(System.Configuration.ConfigurationManager.AppSettings["Expiration"].ToString(), out expiration))
                    {
                        expiration = 30;
                    }

                    String userData = "";
                    if (userObj.usertype == ((int)UserTypeEnum.ADMIN).ToString()) { userData = Constant.ROLE_ADMIN; }
                    else if (userObj.usertype == ((int)UserTypeEnum.QUERY).ToString()) { userData = Constant.ROLE_QUERY; }

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, model.UserName, DateTime.Now,
                        DateTime.Now.AddMinutes(expiration), false, userData);       

                    string ticketEncrypt = FormsAuthentication.Encrypt(ticket);
                    System.Web.HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketEncrypt);
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                   
                   

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "提供的用户名或密码不正确。");
                    return View(model);
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            ModelState.AddModelError("", "提供的用户名或密码不正确。");
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("ContractList", "Contract");
            }
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            //WebSecurity.Logout();

            FormsAuthentication.SignOut();

            //return RedirectToAction("ContractList", "Contract");
            return RedirectToAction("Login", "User");
        }
                                             
        public ActionResult Index()
        {
            return View();
        }

        [MyAuthorize(Roles=Constant.ROLE_ADMIN)]
        public ActionResult UserList( string username , string usertype = "2" , int state=2 ,  int pageidx = 1)
        {
            Models.AccountContext dbContext = new Models.AccountContext();
            Page<User> page = dbContext.QueryByPage(username, (EnableEnum)state, pageidx);

            SetDropDownlist(state);


            SetUserTypeDropDownlist(usertype);
          
            return View(page);
        }

        protected void SetDropDownlist( int state)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            SelectListItem item = new SelectListItem();
            item.Text = "请选择";
            item.Value = ((int)EnableEnum.ALL).ToString();
            item.Selected = state == (int)EnableEnum.ALL;
            items.Add(item);

            item = new SelectListItem();
            item.Text = "启用";
            item.Value = ((int)EnableEnum.ENABLE).ToString();
            item.Selected = state == (int)EnableEnum.ENABLE;
            items.Add(item);

            item = new SelectListItem();
            item.Text = "停用";
            item.Value = ((int)EnableEnum.DISABLE).ToString();
            item.Selected = state == (int)EnableEnum.DISABLE;
            items.Add(item);

            ViewData["enableItems"] = items;
        }
               
         protected void SetUserTypeDropDownlist( string statestr )
        {
            int state = 0;
            int.TryParse(statestr, out state);

            List<SelectListItem> items = new List<SelectListItem>();
            SelectListItem item = new SelectListItem();
            item.Text = "查询账户";
            item.Value = ((int)UserTypeEnum.QUERY).ToString();
            item.Selected = state == (int)UserTypeEnum.QUERY;
            items.Add(item);

            item = new SelectListItem();
            item.Text = "管理账户";
            item.Value = ((int)UserTypeEnum.ADMIN).ToString();
            item.Selected = state == (int)UserTypeEnum.ADMIN;
            items.Add(item);

            ViewData["userTypeItems"] = items;
        }

        [MyAuthorize(Roles=Constant.ROLE_ADMIN)]
        public ActionResult AddUser()
        {
            SetDropDownlist((int)EnableEnum.ENABLE);

            SetUserTypeDropDownlist(((int)UserTypeEnum.QUERY).ToString());

            return View();
        }

        [MyAuthorize(Roles=Constant.ROLE_ADMIN)]
        [HttpPost]
        public ActionResult AddUser( ContractMvcWeb.Models.Beans.User  user)
        {
            SetDropDownlist(user.enable);


            SetUserTypeDropDownlist( user.usertype);

            if (ModelState.IsValid == false)
            {
                return View();
            }

            if (user == null)
            {
                return View();
            }

           


            if (string.IsNullOrEmpty(user.password))
            {
                ModelState.AddModelError("password","密码不能为空!");
                return View();
            }

            if (user.password != user.comfirmPassword)
            {
                ModelState.AddModelError("password", "密码不一致!");
                return View();
            }

            ContractMvcWeb.Models.AccountContext dbContext = new Models.AccountContext();
            bool exist = dbContext.ExistUserName(user.username);
            if (exist)
            {
                ModelState.AddModelError("username", "用户名已经存在!");
                return View ();
            }

            int result = dbContext.AddUser(user);
          return new RedirectResult("~/user/userList");

        }

        [MyAuthorize(Roles=Constant.ROLE_ADMIN)]
        public ActionResult EditUser( int userid)
        {
            ContractMvcWeb.Models.AccountContext dbContext = new Models.AccountContext();
            User model = dbContext.GetModel(userid);

            SetDropDownlist( model.enable );
            SetUserTypeDropDownlist( model.usertype);

            return View( model );
        }

        [MyAuthorize(Roles=Constant.ROLE_ADMIN)]
        [HttpPost]
        public ActionResult EditUser(User model)
        {
            SetDropDownlist((int)EnableEnum.ENABLE);

            SetUserTypeDropDownlist(((int)UserTypeEnum.QUERY).ToString());

            ContractMvcWeb.Models.AccountContext dbContext = new Models.AccountContext();



            if (model == null) return View();
            if (string.IsNullOrEmpty(model.username))
            {
                ModelState.AddModelError("", "请输入用户名");
                return View();
            }
            

            bool isSuccess = dbContext.EditUser(model);
            return new RedirectResult("~/user/userlist");
        }

        //[HttpPost]
        //[MyAuthorize(Roles = "2")]
        //public ActionResult DeleteUser(int userid)
        //{
        //    ContractMvcWeb.archiveEntities db = new archiveEntities();
        //    ContractMvcWeb.t_user u = new t_user();
        //    u.userid = userid;
        //    db.t_user.Remove(u);
        //    return new RedirectResult("~/user/userList");
        //}

        [MyAuthorize(Roles = Constant.ROLE_QUERY + "," + Constant.ROLE_ADMIN)]
        public ActionResult ChangePassword()  
        {
            return View();
        }

        [MyAuthorize(Roles=Constant.ROLE_QUERY+","+Constant.ROLE_ADMIN)]
        [HttpPost]
        public ActionResult ChangePassword(Models.Beans.LocalPasswordModel model)
        {
            ContractMvcWeb.Models.AccountContext dbContext = new Models.AccountContext();
            string msg = string.Empty;
            bool  issuccess =  dbContext.ChangePassword(model, User.Identity.Name , out msg );
            if(issuccess == false ){
                ModelState.AddModelError("", msg );
            }
            return View();
        }

    }
}
