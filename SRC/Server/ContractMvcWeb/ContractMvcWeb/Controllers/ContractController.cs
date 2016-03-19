using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ContractMvcWeb.Attributes;
using ContractMvcWeb.Models;
using ContractMvcWeb.Models.Beans;

namespace ContractMvcWeb.Controllers
{
    [MyAuthorize]
    public class ContractController : Controller
    {                                                          
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContractList( string seq, string contractnum, string projectnum,
            string projectname, string rfid, string contractplace,
            string bcompany, string money, string pvalue, string pkey = "seq", string sortkey= "" , string sorttype="",  int pageidx = 1, int pagesize = 20)
        {

            Contract query = new Contract();
            query.seq = seq;
            query.contractnum = contractnum;
            query.projectnum = projectnum;
            query.projectname = projectname;
            query.contractrfid = rfid;
            query.contractplace = contractplace;
            query.bcompany = bcompany;
            query.money = money;

            //
            query.pkey = pkey;
            query.pvalue = pvalue;
            //
            query.sortkey = sortkey;
            query.sorttype = sorttype;

            Page<Contract> page = GetData(query, pageidx, pagesize);

            

            return View(page);  
        }

        protected Page<Contract> GetData( Contract query , int pageidx ,  int pagesize )
        {
            Models.ContractContext dbContext = new Models.ContractContext();
            Page<Contract> page = dbContext.QueryByPage(query, pageidx, pagesize);
            return page;
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

        [MyAuthorize(Roles=Constant.ROLE_ADMIN)]
        public ActionResult ImportContract()
        {                             
            return View();
        }

        //public ActionResult ImportXYContract()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult ImportContract(string msg = "")
        //{
        //    HttpPostedFileBase file = Request.Files["uploadFile"];
        //    if (file == null)
        //    {
        //        return View("ImportContract");
        //    }
        //    string filename = file.FileName;
        //    string prefix = System.IO.Path.GetExtension(filename).ToLower().Trim();
        //    if (prefix.Equals(".xls") == false && prefix.Equals(".xlsx") == false)
        //    {
        //        ModelState.AddModelError("fileerror", "请上传Excel格式的文件。");
        //        return View("ImportContract");
        //    }

        //    string folder = Request.MapPath("~/Uploadfiles/");
        //    if (System.IO.Directory.Exists(folder) == false)
        //    {
        //        System.IO.Directory.CreateDirectory(folder);
        //    }
        //    if (folder.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()) == false)
        //    {
        //        folder += System.IO.Path.DirectorySeparatorChar;
        //    }
            
        //    string filepath = folder + DateTime.Now.ToString("yyyyMMddHhmmss")+ prefix ;
        //    file.SaveAs( filepath  );

        //    List<Models.Beans.Contract> list = Utils.ExcelUtils.ParseExcel(filepath);
        //    ContractMvcWeb.Models.ContractContext dbContext = new Models.ContractContext();
        //    //int result = dbContext.BatchAddContracts(list , User.Identity.Name);
        //    Models.Beans.BatchImportResult result = dbContext.BatchAddContracts(list, User.Identity.Name);

        //    string message = string.Empty;
        //    message += "共" + result.TotalCount + " 条,新增 " + result.AddCount + "条,更新 " + result.UpdateCount + "条";
        //    if (result.FailureCount != 0)
        //    {
        //        message += ",失败 " + result.FailureCount + "条。";
        //    }
        //    ModelState.AddModelError("summary", message);
        //    if (result.ErrorList != null && result.ErrorList .Count > 0 )
        //    {
        //        message = "";
        //        foreach (Models.Beans.BatchImportResult.ExcelErrorLine item in result.ErrorList)
        //        {
        //              string temp = string.Format("行号:{0},信息:{1}" ,item.Line , item.Error );
        //              message += temp;

        //            ModelState.AddModelError( item.Line , temp );
        //        }                          
                
        //    }

        //    return View("ImportContract");
        //}


        //[HttpPost]
        //public ActionResult ImportXYContract(string msg = "")
        //{
        //    HttpPostedFileBase file = Request.Files["uploadFile"];
        //    if (file == null)
        //    {
        //        return View("ImportXYContract");
        //    }
        //    string filename = file.FileName;
        //    string prefix = System.IO.Path.GetExtension(filename).ToLower().Trim();
        //    if (prefix.Equals(".xls") == false && prefix.Equals(".xlsx") == false)
        //    {
        //        ModelState.AddModelError("fileerror", "请上传Excel格式的文件。");
        //        return View("ImportXYContract");
        //    }

        //    string folder = Request.MapPath("~/Uploadfiles/");
        //    if (System.IO.Directory.Exists(folder) == false)
        //    {
        //        System.IO.Directory.CreateDirectory(folder);
        //    }
        //    if (folder.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()) == false)
        //    {
        //        folder += System.IO.Path.DirectorySeparatorChar;
        //    }

        //    string filepath = folder + "caigou_"+ DateTime.Now.ToString("yyyyMMddHhmmss") + prefix;
        //    file.SaveAs(filepath);

        //    List<Models.Beans.ContractCG> list = Utils.ExcelUtils.ParseCGExcel(filepath);
        //    ContractMvcWeb.Models.ContractCGContext dbContext = new Models.ContractCGContext();
        //    //int result = dbContext.BatchAddContracts(list , User.Identity.Name);
        //    Models.Beans.BatchImportResult result = dbContext.BatchAddContracts(list, User.Identity.Name);

        //    string message = string.Empty;
        //    message += "共" + result.TotalCount + " 条,新增 " + result.AddCount + "条,更新 " + result.UpdateCount + "条";
        //    if (result.FailureCount != 0)
        //    {
        //        message += ",失败 " + result.FailureCount + "条。";
        //    }
        //    ModelState.AddModelError("summary", message);
        //    if (result.ErrorList != null && result.ErrorList.Count > 0)
        //    {
        //        message = "";
        //        foreach (Models.Beans.BatchImportResult.ExcelErrorLine item in result.ErrorList)
        //        {
        //            string temp = string.Format("行号:{0},信息:{1}", item.Line, item.Error);
        //            message += temp;

        //            ModelState.AddModelError(item.Line, temp);
        //        }

        //    }

        //    return View("ImportXYContract");
        //}
        

        public FileResult ExportExcel(string seq, string contractnum, string projectnum,
            string projectname, string rfid, string contractplace,
            string bcompany, string money)
        {
            Contract query = new Contract();
            query.seq = seq;
            query.contractnum = contractnum;
            query.projectnum = projectnum;
            query.projectname = projectname;
            query.contractrfid = rfid;
            query.contractplace = contractplace;
            query.bcompany = bcompany;
            query.money = money;

            List<Contract> list = GetData(query);

            string fileName =  DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

            //第二种:使用FileStreamResult
            System.IO.MemoryStream fileStream = Utils.ExcelUtils.RenderToExcel(list); // new MemoryStream(fileContents);
            return File(fileStream, "application/ms-excel", fileName);  

            // Utils.ExcelUtils.RenderToBrowser(list, this.HttpContext , fileName ); 
        }

        public List<Contract> GetData(Contract query)
        {
            Models.ContractContext dbContext = new Models.ContractContext();
            List<Contract> list = dbContext.Query(query);
            return list;
        }

        public ActionResult DownloadExcelTemplate()
        {                         
            string fileName = Server.MapPath("~/ExcelTemplate/ContractTemplate.xls");
            if (System.IO.File.Exists(fileName))
            {
                return File(fileName, "application/vnd.ms-excel", "合同导入模板.xls");
            }
            else
            {
                ModelState.AddModelError("", "模板文件不存在.");
                return View("ImportContract");
            }  
        }

        //public ActionResult DownloadExcelXieYiTemplate()
        //{
        //    string fileName = Server.MapPath("~/ExcelTemplate/XYTemplate.xls");
        //    if (System.IO.File.Exists(fileName))
        //    {
        //        return File(fileName, "application/vnd.ms-excel", "采购合同导入模板.xls");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "模板文件不存在.");
        //        return View("ImportXYContract");
        //    }  
        //}
         
      
        public ActionResult EditContract(int contractid)
        {
            ContractMvcWeb.Models.ContractContext dbContext = new Models.ContractContext();
            Contract model = dbContext.GetModel(contractid);
            return View( model );
        }

        [MyAuthorize(Roles=Constant.ROLE_ADMIN)]
        [HttpPost]
        public ActionResult EditContract(Contract contract)
        {
            if (ModelState.IsValid)
            {
                bool isok = CheckContractData(contract);
                if (isok == false) return View();

                ContractMvcWeb.Models.ContractContext dbContext = new Models.ContractContext();
                contract.modifytime = DateTime.Now;

                bool isExist = dbContext.ExistContractBySeqAndprojectNum(contract.seq, contract.projectnum, contract.contractid);
                if (isExist)
                {
                    ModelState.AddModelError("error", "序号和项目编号已经存在。");
                    return View();
                }

                bool success = dbContext.EditContract(contract);
                if (success == true)
                {
                    return new RedirectResult("~/Contract/ContractList");
                }
                else
                {
                    ModelState.AddModelError("e3", "保存失败!");
                    return View();
                }
            }
            return View ();
        }
      
        [MyAuthorize(Roles=Constant.ROLE_ADMIN)]
        public ActionResult AddContract()
        {
            return View();
        }

        //public ActionResult AddCaiGouContract()
        //{
        //    return View();
        //}

        protected bool CheckContractData(Contract model)
        {
            if (model == null) return false;

            bool isok = true;
            if (string.IsNullOrEmpty(model.contractnum) )
            {
                ModelState.AddModelError("ht1", "合同编号不能空。");
                isok = false;
            }

            if (string.IsNullOrEmpty(model.seq))
            {
                ModelState.AddModelError("ht3", "序号不能空。");
                isok = false;
            }

            if( string.IsNullOrEmpty( model.projectnum ))
            {
                ModelState.AddModelError("ht2", "项目编号不能空。");
                isok = false;
            }
            if (string.IsNullOrEmpty(model.projectname))
            {
                ModelState.AddModelError("ht", "项目名称不能空。");
                isok = false;
            }

            decimal fbys = 0;

            if (string.IsNullOrEmpty(model.packageBudget))
            {
                model.packageBudget = "0.00";
            }
            if (decimal.TryParse(model.packageBudget, out fbys) == false)
            {
                ModelState.AddModelError("fbys", "分包预算必须是数字。");
                isok = false;
            }

            if (string.IsNullOrEmpty(model.money))
            {
                model.money = "0.00";
            }
            decimal zbje = 0;
            if (decimal.TryParse(model.money, out zbje) == false)
            {
                ModelState.AddModelError("zbje", "中标金额必须是数字。");
                isok = false;
            }
            return isok;
        }

        [MyAuthorize(Roles=Constant.ROLE_ADMIN)]
        [HttpPost]
        public ActionResult AddContract(Contract model)
        {
            try
            {
                if (ModelState.IsValid == false) return View();

                bool isok = CheckContractData(model);

                if (isok == false) return View();

                ContractMvcWeb.Models.ContractContext dbContext = new Models.ContractContext();
                bool isExist = dbContext.ExistContractBySeqAndprojectNum( model.seq , model.projectnum );
                if (isExist)
                {
                    ModelState.AddModelError("e1", "序号和项目编号已经存在，操作失败。");
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

                return new RedirectResult("~/contract/contractlist");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("e3", ex.Message);
                return View();
            }
        }

        protected bool CheckContractCGData(ContractCG model)
        {
            if (model == null) return false;

            bool isok = true;
            if (string.IsNullOrEmpty(model.contractnum))
            {
                ModelState.AddModelError("ht1", "合同编号不能空。");
                isok = false;
            }

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


        //[HttpPost]
        //public ActionResult AddCaiGouContract(ContractCG model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid == false) return View();
        //        bool isok = CheckContractCGData(model);

        //        if (isok == false) return View();

        //        ContractMvcWeb.Models.ContractCGContext dbContext = new Models.ContractCGContext();
        //        bool isExist = dbContext.ExistContractByContractNumAndSeqAndprojectNum( model.contractnum , model.seq, model.projectnum);
        //        if (isExist)
        //        {
        //            ModelState.AddModelError("e1", "合同编号，序号和项目编号已经存在，操作失败。");
        //            return View();
        //        }
        //        model.createtime = DateTime.Now;
        //        model.modifytime = model.createtime;
        //        model.operatorName = Request.RequestContext.HttpContext.User.Identity.Name;
        //        //model.operatorId = 

        //        bool result = dbContext.AddContract(model);
        //        if (result == false)
        //        {
        //            ModelState.AddModelError("e2", "新增失败。");
        //            return View();
        //        }

        //        return new RedirectResult("~/contract/contractcglist");
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("e3", ex.Message);
        //        return View();
        //    }
        //}

        [MyAuthorize(Roles=Constant.ROLE_ADMIN)]
        [HttpPost]
        public JsonResult DeleteContracts(List<int> contractids)
        {
            JsonResult json = new JsonResult();    
            if (contractids == null || contractids.Count < 1)
            {
                json.Data = new Models.Result((int)Models.ResultCodeEnum.Error, "请选择要删除的记录", null);
                return json;
            }
            ContractMvcWeb.Models.ContractContext dbContext = new Models.ContractContext();
            bool isok= dbContext.DeleteContracts(contractids);
            json.Data = new Models.Result((int)Models.ResultCodeEnum.Success, "", "");
            return json;
        }
    
    }
}
