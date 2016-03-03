using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContractMvcWeb.Models.Beans;

namespace ContractMvcWeb.Controllers
{
    /// <summary>
    /// 零星合同控制器
    /// </summary>
    public class LXContractController : Controller
    {
        //
        // GET: /LXContract/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContractList(string pvalue, string pkey = "seq", string sortkey = "", string sorttype = "", int pageidx = 1, int pagesize = 20)
        {
            ContractLX query = new ContractLX();
            //
            query.pkey = pkey;
            query.pvalue = pvalue;
            //
            query.sortkey = sortkey;
            query.sorttype = sorttype;

            Models.ContractLXContext dbContext = new Models.ContractLXContext();

            Page<ContractLX> page = dbContext.QueryByPage(query, pageidx, pagesize);


            return View(page);

        }

    }
}
