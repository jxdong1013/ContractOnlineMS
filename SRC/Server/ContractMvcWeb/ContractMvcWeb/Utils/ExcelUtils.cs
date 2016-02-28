using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using NPOI.SS.UserModel;

namespace ContractMvcWeb.Utils
{
    public class ExcelUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<Models.Beans.Contract> ParseExcel(string filePath)
        {
            FileStream fs = null;   
            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                NPOI.SS.UserModel.IWorkbook workbook = null;
                workbook = NPOI.SS.UserModel.WorkbookFactory.Create(fs);

                NPOI.SS.UserModel.ISheet sheet = workbook.GetSheetAt(0);
                if (sheet == null) return null;                  
                NPOI.SS.UserModel.IRow firstRow = sheet.GetRow(0);                    
                int rowCount = sheet.LastRowNum;
                int cellCount = firstRow.LastCellNum;

                List<Models.Beans.Contract> list = new List<Models.Beans.Contract>();

                for (int i = sheet.FirstRowNum + 1; i <= rowCount; i++)
                {
                    try
                    {
                        Models.Beans.Contract model = new Models.Beans.Contract();

                        NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                        if (row == null) continue;

                        model.contractplace = row.GetCell(0) == null ? string.Empty : row.GetCell(0).ToString();
                        model.contractnum = row.GetCell(1) == null ? string.Empty : row.GetCell(1).ToString();
                        model.seq = row.GetCell(2) == null ? string.Empty : row.GetCell(2).ToString();
                        model.projectnum = row.GetCell(3) == null ? string.Empty : row.GetCell(3).ToString();
                        model.projectname = row.GetCell(4) == null ? string.Empty : row.GetCell(4).ToString();
                        model.projectmanager = row.GetCell(5) == null ? string.Empty : row.GetCell(5).ToString();
                        model.tel = row.GetCell(6) == null ? string.Empty : row.GetCell(6).ToString();
                        model.depart = row.GetCell(7) == null ? string.Empty : row.GetCell(7).ToString();
                        model.packageName = row.GetCell(8) == null ? string.Empty : row.GetCell(8).ToString();
                        model.packageBudget = row.GetCell(9) == null ? string.Empty : row.GetCell(9).ToString();
                        model.tendarNum = row.GetCell(10) == null ? string.Empty : row.GetCell(10).ToString();
                        model.tendarCompany = row.GetCell(11) == null ? string.Empty : row.GetCell(11).ToString();
                        model.tendarStartTime = row.GetCell(12) == null ? string.Empty : row.GetCell(12).ToString();
                        model.paymethod = row.GetCell(13) == null ? string.Empty : row.GetCell(13).ToString();
                        model.bcompany = row.GetCell(14) == null ? string.Empty : row.GetCell(14).ToString();
                        model.linker = row.GetCell(15) == null ? string.Empty : row.GetCell(15).ToString();
                        model.phone = row.GetCell(16) == null ? string.Empty : row.GetCell(16).ToString();
                        model.money = row.GetCell(17) == null ? string.Empty : row.GetCell(17).ToString();
                        model.signingdate = row.GetCell(18) == null ? string.Empty : row.GetCell(18).ToString();
                        model.deliveryTime = row.GetCell(19) == null ? string.Empty : row.GetCell(19).ToString();
                        model.inspection = row.GetCell(20) == null ? string.Empty : row.GetCell(20).ToString();
                        model.progress = row.GetCell(21) == null ? string.Empty : row.GetCell(21).ToString();
                        model.isPayAll = row.GetCell(22) == null ? string.Empty : row.GetCell(22).ToString();
                        model.isArmoured = row.GetCell(23) == null ? string.Empty : row.GetCell(23).ToString();
                        model.isRefund = row.GetCell(24) == null ? string.Empty : row.GetCell(24).ToString();

                        if (CheckEmptyLine( model )) continue; 

                        list.Add(model);
                    }
                    catch (Exception ex)
                    {
                        //string msg = ex.Message;
                        throw ex;
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
            }
        } 

        protected static void CreateCell(IRow row, int cellIdx, string txt, CellType type = CellType.String, ICellStyle style = null)
        {
            ICell cell = row.CreateCell(cellIdx, type);
            if (style != null) cell.CellStyle = style;
            cell.SetCellValue(txt);     
        }

        public static MemoryStream RenderToExcel(List<Models.Beans.Contract> list)
        {   
            MemoryStream ms = new MemoryStream();

            NPOI.SS.UserModel.IWorkbook workbook = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet1");

            #region head row
            NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);
            ICellStyle style = workbook.CreateCellStyle();
            IFont font = workbook.CreateFont();
            font.Boldweight = (short)FontBoldWeight.Bold;
            style.SetFont(font);
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            //row.RowStyle = style;
            row.Height = 26 * 20;

            CreateCell(row, 0, "存放位置", CellType.String, style);
            CreateCell(row, 1, "合同号", CellType.String, style);
            CreateCell(row, 2, "序号", CellType.String, style);
            CreateCell(row, 3, "项目编号", CellType.String, style);
            CreateCell(row, 4, "项目名称", CellType.String, style);
            CreateCell(row, 5, "项目负责人", CellType.String, style);
            CreateCell(row, 6, "联系方式", CellType.String, style);
            CreateCell(row, 7, "分管部门", CellType.String, style);
            CreateCell(row, 8, "分包名称", CellType.String, style);
            CreateCell(row, 9, "分包预算(万元)", CellType.String, style);
            CreateCell(row, 10, "招标编号", CellType.String, style);
            CreateCell(row, 11, "招标公司", CellType.String, style);
            CreateCell(row, 12, "开标时间", CellType.String, style);
            CreateCell(row, 13, "付款方式", CellType.String, style);
            CreateCell(row, 14, "中标公司名称", CellType.String, style);
            CreateCell(row, 15, "联系人", CellType.String, style);
            CreateCell(row, 16, "手机号码", CellType.String, style);
            CreateCell(row, 17, "中标金额(万元)", CellType.String, style);
            CreateCell(row, 18, "签合同日期", CellType.String, style);
            CreateCell(row, 19, "交货时间", CellType.String, style);
            CreateCell(row, 20, "验收情况", CellType.String, style);
            CreateCell(row, 21, "进度", CellType.String, style);
            CreateCell(row, 22, "支付全款", CellType.String, style);
            CreateCell(row, 23, "押款", CellType.String, style);
            CreateCell(row, 24, "退款", CellType.String, style);
            CreateCell(row, 25, "标签", CellType.String, style);
            #endregion

            if (list != null && list.Count > 0)
            {
                int rowidx = 0;
                foreach (Models.Beans.Contract model in list)
                {
                    #region row
                    rowidx++;
                    row = sheet.CreateRow(rowidx);
                    CreateCell(row, 0, model.contractplace);
                    CreateCell(row, 1, model.contractnum);
                    CreateCell(row, 2, model.seq);
                    CreateCell(row, 3, model.projectnum);
                    CreateCell(row, 4, model.projectname);
                    CreateCell(row, 5, model.projectmanager);
                    CreateCell(row, 6, model.tel);
                    CreateCell(row, 7, model.depart);
                    CreateCell(row, 8, model.packageName);
                    CreateCell(row, 9, model.packageBudget);
                    CreateCell(row, 10, model.tendarNum);
                    CreateCell(row, 11, model.tendarCompany);
                    CreateCell(row, 12, model.tendarStartTime);
                    CreateCell(row, 13, model.paymethod);
                    CreateCell(row, 14, model.bcompany);
                    CreateCell(row, 15, model.linker);
                    CreateCell(row, 16, model.phone);
                    CreateCell(row, 17, model.money);
                    CreateCell(row, 18, model.signingdate);
                    CreateCell(row, 19, model.deliveryTime);
                    CreateCell(row, 20, model.inspection);
                    CreateCell(row, 21, model.progress);
                    CreateCell(row, 22, model.isPayAll);
                    CreateCell(row, 23, model.isArmoured);
                    CreateCell(row, 24, model.isRefund);        
                    CreateCell(row, 25, model.contractrfid);
                    #endregion
                }
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            return ms;
        }

        public static void SaveToFile(MemoryStream ms, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();

                fs.Write(data, 0, data.Length);
                fs.Flush();

                data = null;
            }
        }

        public static void RenderToBrowser(List<Models.Beans.Contract> list, HttpContextBase context, string fileName)
        {
            MemoryStream ms = RenderToExcel(list);
            if (context.Request.Browser.Browser == "IE")
                fileName = HttpUtility.UrlEncode(fileName);
            context.Response.AddHeader("Content-Disposition", "attachment;fileName=" + fileName);
            context.Response.BinaryWrite(ms.ToArray());
            if (ms != null)
            {
                ms.Close();
                ms = null;
            }
        }


        protected static bool CheckEmptyLine( Models.Beans.Contract model )
        {
            return string.IsNullOrEmpty(model.bcompany) &&
                string.IsNullOrEmpty(model.contractname) &&
                string.IsNullOrEmpty(model.contractnum) &&
                string.IsNullOrEmpty(model.contractplace) &&
                string.IsNullOrEmpty(model.deliveryTime) &&
                string.IsNullOrEmpty(model.depart) &&
                string.IsNullOrEmpty(model.inspection) &&
                string.IsNullOrEmpty(model.isArmoured) &&
                string.IsNullOrEmpty(model.isPayAll) &&
                string.IsNullOrEmpty(model.isRefund) &&
                string.IsNullOrEmpty(model.linker) &&
                string.IsNullOrEmpty(model.money) &&
                string.IsNullOrEmpty(model.packageBudget) &&
                string.IsNullOrEmpty(model.packageName) &&
                string.IsNullOrEmpty(model.paymethod) &&
                string.IsNullOrEmpty(model.phone) &&
                string.IsNullOrEmpty(model.progress) &&
                string.IsNullOrEmpty(model.projectmanager) &&
                string.IsNullOrEmpty(model.projectname) &&
                string.IsNullOrEmpty(model.projectnum) &&
                string.IsNullOrEmpty(model.seq) &&
                string.IsNullOrEmpty(model.signingdate) &&
                string.IsNullOrEmpty(model.tel) &&
                string.IsNullOrEmpty(model.tendarCompany) &&
                string.IsNullOrEmpty(model.tendarNum) &&
                string.IsNullOrEmpty(model.tendarStartTime);                                
        
        }
    }
}