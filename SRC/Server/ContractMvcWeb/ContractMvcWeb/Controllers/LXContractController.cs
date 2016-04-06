using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContractMvcWeb.Models.Beans;
using ContractMvcWeb.Attributes;
using ContractMvcWeb.Models;

namespace ContractMvcWeb.Controllers
{
    /// <summary>
    /// 零星合同控制器
    /// </summary>
    [MyAuthorize(Roles = Constant.ROLE_ADMIN)]
    public class LXContractController : Controller
    {
        //
        // GET: /LXContract/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContractList(string pvalue, string pkey = "all", string sortkey = "", string sorttype = "", int pageidx = 1, int pagesize = 20)
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

        public ActionResult ImportContract()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportContract(string msg = "")
        {
            HttpPostedFileBase file = Request.Files["uploadFile"];
            if (file == null)
            {
                return View("ImportContract");
            }
            string filename = file.FileName;
            string prefix = System.IO.Path.GetExtension(filename).ToLower().Trim();
            if (prefix.Equals(".xls") == false && prefix.Equals(".xlsx") == false)
            {
                ModelState.AddModelError("fileerror", "请上传Excel格式的文件。");
                return View("ImportContract");
            }

            string folder = Request.MapPath("~/Uploadfiles/");
            if (System.IO.Directory.Exists(folder) == false)
            {
                System.IO.Directory.CreateDirectory(folder);
            }
            if (folder.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()) == false)
            {
                folder += System.IO.Path.DirectorySeparatorChar;
            }

            string filepath = folder + "lingxing_" + DateTime.Now.ToString("yyyyMMddHhmmss") + prefix;
            file.SaveAs(filepath);

            List<Models.Beans.ContractLX> list = Utils.ExcelUtils.ParseLXExcel(filepath);
            ContractMvcWeb.Models.ContractLXContext dbContext = new Models.ContractLXContext();
            //int result = dbContext.BatchAddContracts(list , User.Identity.Name);
            Models.Beans.BatchImportResult result = dbContext.BatchAddContracts(list, User.Identity.Name);

            string message = string.Empty;
            message += "共" + result.TotalCount + " 条,新增 " + result.AddCount + "条,更新 " + result.UpdateCount + "条";
            if (result.FailureCount != 0)
            {
                message += ",失败 " + result.FailureCount + "条。";
            }
            ModelState.AddModelError("summary", message);
            if (result.ErrorList != null && result.ErrorList.Count > 0)
            {
                message = "";
                foreach (Models.Beans.BatchImportResult.ExcelErrorLine item in result.ErrorList)
                {
                    string temp = string.Format("行号:{0},信息:{1}", item.Line, item.Error);
                    message += temp;

                    ModelState.AddModelError(item.Line, temp);
                }

            }

            return View("ImportContract");
        }
        

        public ActionResult DownloadExcelTemplate()
        {
            string fileName = Server.MapPath("~/ExcelTemplate/LXTemplate.xls");
            if (System.IO.File.Exists(fileName))
            {
                return File(fileName, "application/vnd.ms-excel", "零星合同导入模板.xls");
            }
            else
            {
                ModelState.AddModelError("", "模板文件不存在.");
                return View("ImportContract");
            }
        }

        public ActionResult AddContract()
        {
            SetDropDownlist("未签合同");
            return View(new ContractLX());
        }

        [HttpPost]
        public ActionResult AddContract(ContractLX model)
        {
            try
            {
                SetDropDownlist(model.payprogress);
                if (ModelState.IsValid == false) return View();

                bool isok = CheckContractData(model);

                if (isok == false) return View();

                ContractMvcWeb.Models.ContractLXContext dbContext = new Models.ContractLXContext();
                bool isExist = dbContext.ExistContractBySeqAndContent(model.seq, model.content);
                if (isExist)
                {
                    ModelState.AddModelError("e1", "采购编号和采购内容已经存在，操作失败。");
                    return View();
                }
                model.createtime = DateTime.Now;
                model.modifytime = model.createtime;
                model.operatorName = Request.RequestContext.HttpContext.User.Identity.Name;
                //model.operatorId = 

                bool result = dbContext.AddContract(model);
                if (result == false)
                {
                    ModelState.AddModelError("e2", "新增失败。");
                    return View();
                }

                return new RedirectResult("~/lxcontract/contractlist");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("e3", ex.Message);
                return View();
            }
        }


        protected bool CheckContractData(ContractLX model)
        {
            if (model == null) return false;

            bool isok = true;
            //if (string.IsNullOrEmpty(model.contractnum))
            //{
            //    ModelState.AddModelError("ht1", "合同编号不能空。");
            //    isok = false;
            //}

            if (string.IsNullOrEmpty(model.seq))
            {
                ModelState.AddModelError("ht3", "采购编号不能空。");
                isok = false;
            }

            if (string.IsNullOrEmpty(model.content))
            {
                ModelState.AddModelError("ht2", "采购内容不能空。");
                isok = false;
            }
            //if (string.IsNullOrEmpty(model.projectname))
            //{
            //    ModelState.AddModelError("ht", "项目名称不能空。");
            //    isok = false;
            //}

            //decimal fbys = 0;

            //if (string.IsNullOrEmpty(model.packageBudget))
            //{
            //    model.packageBudget = "0.00";
            //}
            //if (decimal.TryParse(model.packageBudget, out fbys) == false)
            //{
            //    ModelState.AddModelError("fbys", "分包预算必须是数字。");
            //    isok = false;
            //}

            //if (string.IsNullOrEmpty(model.money))
            //{
            //    model.money = "0.00";
            //}
            //decimal zbje = 0;
            //if (decimal.TryParse(model.money, out zbje) == false)
            //{
            //    ModelState.AddModelError("zbje", "中标金额必须是数字。");
            //    isok = false;
            //}
            return isok;
        }

        public ActionResult EditContract(int? contractid, string queryKey = "all", string queryValue = "", string sortkey = "", string sorttype = "", int pageidx = 1, int pagesize = 20)
        {
            if (contractid.HasValue)
            {
                ContractMvcWeb.Models.ContractLXContext dbContext = new Models.ContractLXContext();
                ContractLX model = dbContext.GetModel(contractid.Value);


                if (model != null)
                {
                    SetDropDownlist(model.payprogress);
                    model.pkey = queryKey;
                    model.pvalue = queryValue;
                    model.sortkey = sortkey;
                    model.sorttype = sorttype;
                    model.pageidx = pageidx;                    

                    return View(model);
                }
                else
                {
                    return new RedirectResult("~/LXContract/ContractList");
                }

            }
            else
            {
                return new RedirectResult("~/LXContract/ContractList");
            }
        }

        [HttpPost]
        public ActionResult EditContract(ContractLX contract)
        {
            SetDropDownlist(contract.payprogress);
            if (ModelState.IsValid)
            {
                bool isok = CheckContractData(contract);
                if (isok == false) return View( contract );

                ContractMvcWeb.Models.ContractLXContext dbContext = new Models.ContractLXContext();
                contract.modifytime = DateTime.Now;

                bool isExist = dbContext.ExistContractBySeqAndContent(contract.seq, contract.content, contract.contractid );
                if (isExist)
                {
                    ModelState.AddModelError("error", "采购编号和采购内容已经存在。");
                    return View( contract );
                }

                bool success = dbContext.Update(contract);
                if (success == true)
                {
                    return new RedirectResult("~/LXContract/ContractList");
                }
                else
                {
                    ModelState.AddModelError("e3", "保存失败!");
                    return View( contract );
                }
            }
            return View();
        }


        [HttpPost]
        public JsonResult DeleteContracts(List<int> contractids)
        {
            JsonResult json = new JsonResult();
            if (contractids == null || contractids.Count < 1)
            {
                json.Data = new Models.Result((int)Models.ResultCodeEnum.Error, "请选择要删除的记录", null);
                return json;
            }
            ContractMvcWeb.Models.ContractLXContext dbContext = new Models.ContractLXContext();
            bool isok = dbContext.DeleteContracts(contractids);
            json.Data = new Models.Result((int)Models.ResultCodeEnum.Success, "", "");
            return json;
        }

        protected void SetDropDownlist(string state)
        {
            state = state.Trim();
            List<SelectListItem> items = new List<SelectListItem>();
            SelectListItem item = new SelectListItem();
            item.Text = "完成";
            item.Value = "完成";//((int)PayProgressEnum.FINISH).ToString();
            item.Selected = state.Equals("完成"); //state == (int)PayProgressEnum.FINISH;
            items.Add(item);

            item = new SelectListItem();
            item.Text = "部分支付";
            item.Value = "部分支付";// ((int)PayProgressEnum.PARTPAY).ToString();
            item.Selected = state.Equals("部分支付");// state == (int)PayProgressEnum.PARTPAY;
            items.Add(item);

            item = new SelectListItem();
            item.Text = "领走发票";
            item.Value = "领走发票";// ((int)PayProgressEnum.TICKET).ToString();
            item.Selected = state.Equals("领走发票");//state == (int)PayProgressEnum.TICKET;
            items.Add(item);

            item = new SelectListItem();
            item.Text = "发票已到";
            item.Value = "发票已到";// ((int)PayProgressEnum.TICKETAT).ToString();
            item.Selected = state.Equals("发票已到"); //state == (int)PayProgressEnum.TICKETAT;
            items.Add(item);

            item = new SelectListItem();
            item.Text = "仅签合同";
            item.Value = "仅签合同";//((int)PayProgressEnum.SIGN).ToString();
            item.Selected = state.Equals("仅签合同");//state == (int)PayProgressEnum.SIGN;
            items.Add(item);

            item = new SelectListItem();
            item.Text = "未签合同";
            item.Value = "未签合同";// ((int)PayProgressEnum.UNSIGN).ToString();
            item.Selected = state.Equals("未签合同"); //state == (int)PayProgressEnum.UNSIGN;
            items.Add(item);

            item = new SelectListItem();
            item.Text = "流标";
            item.Value = "流标"; //((int)PayProgressEnum.LOST).ToString();
            item.Selected = state.Equals("流标");// state == (int)PayProgressEnum.LOST;
            items.Add(item);

            ViewData["payprogressItems"] = items;
        }


        public FileResult ExportExcel(string pvalue, string pkey = "all", string sortkey = "", string sorttype = "")
        {
            ContractLX query = new ContractLX();

            query.pkey = pkey;
            query.pvalue = pvalue;
            query.sortkey = sortkey;
            query.sorttype = sorttype;

            List<ContractLX> list = GetData(query);

            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

            //第二种:使用FileStreamResult
            System.IO.MemoryStream fileStream = Utils.ExcelUtils.RenderToExcel(list); // new MemoryStream(fileContents);
            return File(fileStream, "application/ms-excel", fileName);

            // Utils.ExcelUtils.RenderToBrowser(list, this.HttpContext , fileName ); 
        }

        public List<ContractLX> GetData(ContractLX query)
        {
            Models.ContractLXContext dbContext = new Models.ContractLXContext();
            List<ContractLX> list = dbContext.Query(query);
            return list;
        }

    }
}
