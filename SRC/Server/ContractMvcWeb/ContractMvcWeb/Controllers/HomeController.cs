using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContractMvcWeb.Attributes;

namespace ContractMvcWeb.Controllers
{          
    [MyAuthorize]
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            ViewBag.Message = "RFID合同管理系统。";

            return View();
        }
        //[MyAuthorize(Roles = "2")]
        public ActionResult About()
        {
            ViewBag.Message = "你的应用程序说明页。";

            return View();
        }
        //[MyAuthorize(Roles = "3")]
        public ActionResult Contact()
        {
            ViewBag.Message = "你的联系方式页。";

            return View();
        }


        public ActionResult NotVisit()
        {
            ViewBag.Message = "你无权访问!";
            return View();
        }

        //[Attributes.AuthorizeRestful(Roles="2")]
        //public JsonResult GetUserInfo()
        //{
        //    List<ContractMvcWeb.Models.user> list = new List<Models.user>();
        //    for (int i = 0; i < 500; i++)
        //    {
        //        ContractMvcWeb.Models.user u = new Models.user();
        //        u.userid = i;
        //        u.username = "金向东";
        //        u.password = DateTime.Now.ToString("HHmmss");
        //        u.email = "超级无敌长的email地址，而且还是中文的哦！";
        //        u.address = "中国浙江省杭州市江干区凯旋街道188号12楼12001室";
        //        u.remark = "实际上我们不需要在每个视图文件中指定Layout，MVC会搜索一个名为 _ViewStart.cshtml的文件，它的内容会自动插入到所有视图文件中，所以如果我们要为所有视图文件指定布局文件可以在 _ViewStart.cshtml中定义";
        //        list.Add(u);
        //    }

            
        //    //ContractMvcWeb.Models.Result re = new Models.Result( (int)Models.ResultCodeEnum.Success , "" );
        //    //re.Code = 

        //    JsonResult result = new JsonResult();
        //    result.Data = list ;
        //    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        //    return result;
        //}
    }
}
