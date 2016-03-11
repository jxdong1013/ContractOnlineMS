using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContractMvcWeb.Models.Beans;

namespace ContractMvcWeb.Controllers
{
    public class CGContractController : Controller
    {
        //
        // GET: /CGContract/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContractList(string pvalue, string pkey = "seq", string sortkey = "", string sorttype = "", int pageidx = 1, int pagesize = 20)
        {
            ContractCG query = new ContractCG();
            //
            query.pkey = pkey;
            query.pvalue = pvalue;
            //
            query.sortkey = sortkey;
            query.sorttype = sorttype;

            Page<ContractCG> page = GetData(query, pageidx, pagesize);


            return View(page);

        }


        protected Page<ContractCG> GetData(ContractCG query, int pageidx, int pagesize)
        {
            Models.ContractCGContext dbContext = new Models.ContractCGContext();
            Page<ContractCG> page = dbContext.QueryByPage(query, pageidx, pagesize);
            return page;
        }

    }
}
