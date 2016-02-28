using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ContractMvcWeb.Attributes;
using ContractMvcWeb.Models.Beans;

namespace ContractMvcWeb.Controllers
{
    [AuthorizeRestful]
    public class ContractRestfullController : Controller
    {

        public JsonResult ContractList( string seq , string contractnum , string projectnum ,
            string projectname , string rfid , string contractplace ,
            string bcompanay , string money , int pageidx =1, int pagesize = 20)
        {
            Contract query = new Contract();
            query.seq = seq;
            query.contractnum = contractnum;
            query.projectnum = projectnum;
            query.projectname = projectname;
            query.contractrfid = rfid;
            query.contractplace = contractplace;
            query.bcompany = bcompanay;
            query.money = money;
            
            Page<Contract> page = GetData(query, pageidx, pagesize);

            ContractMvcWeb.Models.Result result = new Models.Result((int)Models.ResultCodeEnum.Success, "", page);

            JsonResult json = new JsonResult();
            json.Data = result;
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return json;
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

        //[HttpPost]
        //public ActionResult ImportExcel()
        //{
        //    HttpPostedFileBase file = Request.Files["uploadFile"];
        //    if (file == null)
        //    {
        //        return View();
        //    }
        //    string filename = file.FileName;
        //    string prefix = System.IO.Path.GetExtension(filename).ToLower().Trim();
        //    if (prefix.Equals(".xls") == false && prefix.Equals(".xlsx") == false)
        //    {
        //        ModelState.AddModelError("", "请上传Excel格式的文件。");
        //        return View("ContractList");
        //    }

        //    string filepath = Request.MapPath("~/uploadfiles/") + DateTime.Now.ToString("yyyyMMddHhmmss")+ prefix ;
        //    file.SaveAs( filepath  );

        //    List<Models.Beans.Contract> list = Utils.ExcelUtils.ParseExcel(filepath);
        //    ContractMvcWeb.Models.ContractContext dbContext = new Models.ContractContext();
        //    int result = dbContext.BatchAddContracts(list, User.Identity.Name);

        //    return new RedirectResult("~/contract/contractlist");
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

            string fileName = "合同"+DateTime.Now.ToString("yyyyMMddHHmmss")+".xls";

            //第二种:使用FileStreamResult
            System.IO.MemoryStream  fileStream = Utils.ExcelUtils.RenderToExcel(list); // new MemoryStream(fileContents);
             return File(fileStream, "application/ms-excel", fileName );
 

           // Utils.ExcelUtils.RenderToBrowser(list, this.HttpContext , fileName );

        }

        public List<Contract> GetData(Contract query)
        {
            Models.ContractContext dbContext = new Models.ContractContext();
            List<Contract> list = dbContext.Query(query);
            return list;
        }

        [HttpPost]
        public JsonResult SaveContract(Contract model)
        {
            if (model.contractid != 0)
            {
                return EditContract(model);
            }
            else
            {
                return AddContract(model);
            }
        }


        protected JsonResult EditContract(Contract contract)
        {
            decimal fbys = 0;
            if (string.IsNullOrEmpty(contract.packageBudget))
            {
                contract.packageBudget = "0.00";
            }

            if (decimal.TryParse(contract.packageBudget, out fbys) == false)
            {
                Models.Result result = new Models.Result((int)Models.ResultCodeEnum.Error, "分包预算必须数字", "");
                JsonResult json = new JsonResult();
                json.Data = result;
                return json;
            }
            if (string.IsNullOrEmpty(contract.money))
            {
                contract.money = "0.00";
            }
            decimal money = 0;
            if (decimal.TryParse(contract.money, out money) == false)
            {
                Models.Result result = new Models.Result((int)Models.ResultCodeEnum.Error, "中标金额必须数字", "");
                JsonResult json = new JsonResult();
                json.Data = result;
                return json;
            }                  

            ContractMvcWeb.Models.ContractContext dbContext = new Models.ContractContext();

            bool isExist = dbContext.ExistContractBySeqAndprojectNum(contract.seq, contract.projectnum, contract.contractid);  //dbContext.ExistContract(contract.projectnum, contract.projectname , contract.contractid);
            if (isExist)
            {
                Models.Result result = new Models.Result((int)Models.ResultCodeEnum.Error, "序号和项目编号已经存在，操作失败。", "");
                JsonResult json = new JsonResult();
                json.Data = result;
                return json;
            }
                        

            if (string.IsNullOrEmpty(contract.contractrfid) == false)
            {
                isExist = dbContext.ExistContractNotSelf( contract.contractrfid , contract.contractid );
                if (isExist)
                {
                    Models.Result result = new Models.Result((int)Models.ResultCodeEnum.Error, "标签已经被使用，操作失败。", "");
                    JsonResult json = new JsonResult();
                    json.Data = result;
                    return json;
                }
            }

            contract.modifytime = DateTime.Now;                                                         
            bool success = dbContext.EditContract(contract);

            if (success == true)
            {
                Models.Result result = new Models.Result((int)Models.ResultCodeEnum.Success, "", "");
                JsonResult json = new JsonResult();
                json.Data = result;
                return json;
            }
            else
            {

                Models.Result result = new Models.Result((int)Models.ResultCodeEnum.Error, "", "");
                JsonResult json = new JsonResult();
                json.Data = result;
                return json;
            }
        }

        //protected Contract GetNextContract(int contractid, long modifytime)
        //{
        //    ContractMvcWeb.Models.ContractContext dbContext = new Models.ContractContext();
        //    dbContext.get
        //}

        protected JsonResult AddContract(Contract model)
        {          
            JsonResult json = null;

            if (model == null)
            {
                json = new JsonResult();
                json.Data = new Models.Result((int)Models.ResultCodeEnum.Error, "参数错误", "");
                return json;
            }
            if (string.IsNullOrEmpty(model.contractnum))
            {
                json = new JsonResult();
                json.Data = new Models.Result((int)Models.ResultCodeEnum.Error, "合同编号不能空", "");
                return json;
            }
            if (string.IsNullOrEmpty(model.seq))
            {
                json = new JsonResult();
                json.Data = new Models.Result((int)Models.ResultCodeEnum.Error, "序号不能空","");
                return json;
            }
            if (string.IsNullOrEmpty(model.projectnum))
            {
                json = new JsonResult();
                json.Data = new Models.Result((int)Models.ResultCodeEnum.Error, "项目编号不能空", "");
                return json;
            }
            if (string.IsNullOrEmpty(model.projectname))
            {
                json = new JsonResult();
                json.Data = new Models.Result((int)Models.ResultCodeEnum.Error, "项目名称不能空", "");
                return json;
            }
            decimal fbys = 0;  
            if (string.IsNullOrEmpty(model.packageBudget))
            {
                model.packageBudget = "0.00";
            }
            if (decimal.TryParse(model.packageBudget, out fbys) == false)
            {
                json = new JsonResult();
                json.Data = new Models.Result((int)Models.ResultCodeEnum.Error, "分包预算必须是数字", "");
                return json;
            }
            if (string.IsNullOrEmpty(model.money))
            {
                model.money = "0.00";
            }
            decimal zbje = 0;
            if (decimal.TryParse(model.money, out zbje) == false)
            {
                json = new JsonResult();
                json.Data = new Models.Result((int)Models.ResultCodeEnum.Error, "中标金额必须是数字", "");
                return json;
            }                          

            ContractMvcWeb.Models.ContractContext dbContext = new Models.ContractContext();
            bool isExist = dbContext.ExistContractBySeqAndprojectNum(model.seq, model.projectnum); //dbContext.ExistContract(model.projectnum, model.projectname);
            if (isExist)
            {
                Models.Result result = new Models.Result((int)Models.ResultCodeEnum.Error, "序号和项目编号已经存在，操作失败。", "");
                json = new JsonResult();
                json.Data = result;
                return json;
            }

            if (string.IsNullOrEmpty(model.contractrfid) == false)
            {
                isExist = dbContext.ExistContract(model.contractrfid);
                if (isExist)
                {
                    Models.Result result = new Models.Result((int)Models.ResultCodeEnum.Error, "标签已经被使用，操作失败。", "");
                    json = new JsonResult();
                    json.Data = result;
                    return json;
                }
            }

            model.createtime = DateTime.Now;
            model.modifytime = model.createtime;
            model.operatorName = Request.RequestContext.HttpContext.User.Identity.Name;
            //model.operatorId = 

            bool isok = dbContext.AddContract(model);
            if ( isok == false )
            {
                Models.Result result = new Models.Result((int)Models.ResultCodeEnum.Error, "新增操作失败。", "");
                json = new JsonResult();
                json.Data = result;
                return json;
            }
                        
            json = new JsonResult();
            json.Data = new Models.Result((int)Models.ResultCodeEnum.Success, "", ""); 
            return json;
        }

        [HttpPost]
        public JsonResult DeleteContracts(List<int> contractids)
        {
            JsonResult json = null;
            try
            {
                json = new JsonResult();
                if (contractids == null || contractids.Count < 1)
                {
                    json.Data = new Models.Result((int)Models.ResultCodeEnum.Error, "请选择要删除的记录", null);
                    return json;
                }
                ContractMvcWeb.Models.ContractContext dbContext = new Models.ContractContext();
                bool isok = dbContext.DeleteContracts(contractids);              
                json.Data = new Models.Result((int)Models.ResultCodeEnum.Success, "", "");
                return json;
            }
            catch (Exception ex)
            {
                json = new JsonResult();
                json.Data = new Models.Result((int)Models.ResultCodeEnum.Error, ex.Message , null);
                return json;
            }
        }

        [HttpPost]
        public JsonResult DeleteAllContracts()
        {
            JsonResult result = null;
            try
            {
                ContractMvcWeb.Models.ContractContext dbContext = new Models.ContractContext();
                dbContext.DeleteAllContracts();
                result = new JsonResult();
                result.Data = new Models.Result((int)Models.ResultCodeEnum.Success, "" , null);
                return result;
            }
            catch (Exception ex)
            {
                result = new JsonResult();
                result.Data = new Models.Result((int)Models.ResultCodeEnum.Error, ex.Message, null);
                return result;
            }
        }
    }
}
