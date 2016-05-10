using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

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

        protected static bool CheckEmptyLine(Models.Beans.ContractCG model , ICell cell1, ICell cell2,ICell cell3, ICell cell4 )
        {
            return string.IsNullOrEmpty(model.contractnum) &&
                string.IsNullOrEmpty(model.seq) &&
                string.IsNullOrEmpty(model.department) &&
                string.IsNullOrEmpty(model.linker) &&
                string.IsNullOrEmpty(model.tel) &&
                string.IsNullOrEmpty(model.projectnum) &&
                string.IsNullOrEmpty(model.fundsource) &&
                string.IsNullOrEmpty(model.type) &&
                string.IsNullOrEmpty(model.content) &&
                string.IsNullOrEmpty(model.budgetamount) &&
                string.IsNullOrEmpty(model.super) &&
                string.IsNullOrEmpty(model.superlinker) &&
                string.IsNullOrEmpty(model.supertel) &&
                string.IsNullOrEmpty(model.settleamount) &&
                string.IsNullOrEmpty(model.freecontent) &&
                string.IsNullOrEmpty(model.freevalue) &&
                string.IsNullOrEmpty(model.validate) &&
                string.IsNullOrEmpty(model.place) &&
                string.IsNullOrEmpty(model.payprogress) &&
                string.IsNullOrEmpty(model.chargedepartment) &&
                string.IsNullOrEmpty(model.remark) &&
                (cell1 == null || string.IsNullOrEmpty(cell1.ToString())) &&
                (cell2 == null || string.IsNullOrEmpty(cell2.ToString())) &&
                (cell3 == null || string.IsNullOrEmpty(cell3.ToString())) &&
                (cell4 == null || string.IsNullOrEmpty(cell4.ToString()));
        }

        protected static bool CheckEmptyLine(Models.Beans.ContractCG model, ICell cell1, ICell cell2, ICell cell3)
        {
            return string.IsNullOrEmpty(model.contractnum) &&
                string.IsNullOrEmpty(model.seq) &&
                string.IsNullOrEmpty(model.department) &&
                string.IsNullOrEmpty(model.linker) &&
                string.IsNullOrEmpty(model.tel) &&
                string.IsNullOrEmpty(model.projectnum) &&
                string.IsNullOrEmpty(model.fundsource) &&
                string.IsNullOrEmpty(model.type) &&
                string.IsNullOrEmpty(model.content) &&
                string.IsNullOrEmpty(model.budgetamount) &&
                string.IsNullOrEmpty(model.super) &&
                string.IsNullOrEmpty(model.superlinker) &&
                string.IsNullOrEmpty(model.supertel) &&
                string.IsNullOrEmpty(model.settleamount) &&
                string.IsNullOrEmpty(model.freecontent) &&
                string.IsNullOrEmpty(model.freevalue) &&
                string.IsNullOrEmpty(model.validate) &&
                string.IsNullOrEmpty(model.place) &&
                string.IsNullOrEmpty(model.payprogress) &&
                string.IsNullOrEmpty(model.chargedepartment) &&
                string.IsNullOrEmpty(model.remark) &&
                (cell1 == null || string.IsNullOrEmpty(cell1.ToString())) &&
                (cell2 == null || string.IsNullOrEmpty(cell2.ToString())) &&
                (cell3 == null || string.IsNullOrEmpty(cell3.ToString()));
        }
  
       
        public static List<Models.Beans.ContractCG> ParseCGExcel(string filePath)
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

                List<Models.Beans.ContractCG> list = new List<Models.Beans.ContractCG>();

                for (int i = sheet.FirstRowNum + 1; i <= rowCount; i++)
                {
                    try
                    {
                        Models.Beans.ContractCG model = new Models.Beans.ContractCG();

                        NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                        if (row == null) continue;

                        model.buytime = row.GetCell(0) == null ? string.Empty : row.GetCell(0).ToString();
                        model.seq = row.GetCell(1) == null ? string.Empty : row.GetCell(1).ToString();
                        model.projectnum = row.GetCell(2) == null ? string.Empty : row.GetCell(2).ToString();
                        model.department = row.GetCell(3) == null ? string.Empty : row.GetCell(3).ToString();
                        model.linker = row.GetCell(4) == null ? string.Empty : row.GetCell(4).ToString();
                        model.tel = row.GetCell(5) == null ? string.Empty : row.GetCell(5).ToString();
                        model.type = row.GetCell(6) == null ? string.Empty : row.GetCell(6).ToString();
                        model.content = row.GetCell(7) == null ? string.Empty : row.GetCell(7).ToString();
                        int count = 0;
                        if( row.GetCell(8) != null ){
                            int.TryParse(row.GetCell(8).ToString(),out count);
                        }
                        model.count = count;

                        decimal price = 0;
                        if (row.GetCell(9) != null)
                        {
                            decimal.TryParse(row.GetCell(9).ToString(), out price);
                        }

                        model.price = price; 

                        decimal subtotal = 0;
                        if (row.GetCell(10) != null)
                        {
                            decimal.TryParse(row.GetCell(10).ToString(), out subtotal);
                        }
                        model.subtotal = subtotal;

                        decimal total = 0;
                        if (row.GetCell(11) != null)
                        {
                            //decimal.TryParse(row.GetCell(11).ToString(), out total);                            
                            int rowspan=0;
                            int colspan=0;
                            int firstrow =0;
                            int firstcol = 0;
                            bool ismerged2 = GetMerginRowCol(sheet, row.RowNum, 11, out rowspan, out colspan,out firstrow, out firstcol);
                            if (ismerged2)
                            {
                                string temp = sheet.GetRow(firstrow).GetCell(firstcol).ToString();
                                decimal.TryParse(temp, out total);
                            }
                            else
                            {
                                decimal.TryParse(row.GetCell(11).ToString(), out total);
                            }
                        }
                        model.total = total;

                        model.validate = row.GetCell(12) == null ? string.Empty : row.GetCell(12).ToString();
                        model.place = row.GetCell(13) == null ? string.Empty : row.GetCell(13).ToString();
                        model.contractnum = row.GetCell(14) == null ? string.Empty : row.GetCell(14).ToString();
                        model.super = row.GetCell(15) == null ? string.Empty : row.GetCell(15).ToString();
                        model.superlinker = row.GetCell(16) == null ? string.Empty : row.GetCell(16).ToString();
                        model.supertel = row.GetCell(17) == null ? string.Empty : row.GetCell(17).ToString();
                        model.freecontent = row.GetCell(18) == null ? string.Empty : row.GetCell(18).ToString();
                        model.freevalue = row.GetCell(19) == null ? string.Empty : row.GetCell(19).ToString();
                        model.chargedepartment = row.GetCell(20) == null ? string.Empty : row.GetCell(20).ToString();
                        model.remark = row.GetCell(21) == null ? string.Empty : row.GetCell(21).ToString();                       
                        model.payprogress = row.GetCell(22) == null ? string.Empty : row.GetCell(22).ToString();
                     
                        if (CheckEmptyLine(model , row.GetCell(8), row.GetCell(9) , row.GetCell(10) , row.GetCell(11))) continue;

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

        protected static bool GetMerginRowCol(ISheet sheet, int rowNum, int colNum, out int rowSpan, out int colSpan, out int firstRow , out int firstCol)
        {
            bool result = false;
            rowSpan = 0;
            colSpan = 0;
            firstRow = 0;
            firstCol = 0;
            if ((rowNum < 1) || (colNum < 1)) return result;
            int rowIndex = rowNum - 1;
            int colIndex = colNum - 1;
            int regionsCount = sheet.NumMergedRegions;
            rowSpan = 1;
            colSpan = 1;
            firstRow = 0;
            firstCol = 0;

            for (int i = 0; i < regionsCount; i++)
            {
                CellRangeAddress range = sheet.GetMergedRegion(i);
                sheet.IsMergedRegion(range);
                //if (range.FirstRow == rowIndex && range.FirstColumn == colIndex)
                //{
                //    rowSpan = range.LastRow - range.FirstRow + 1;
                //    colSpan = range.LastColumn - range.FirstColumn + 1;
                //    break;
                //}
                if (range.FirstRow <= rowNum && range.LastRow >= rowNum && range.FirstColumn <= colNum && range.LastColumn>= colNum)
                {
                    rowSpan = range.LastRow - range.FirstRow + 1;
                    colSpan = range.LastColumn - range.FirstColumn + 1;
                    firstRow = range.FirstRow;
                    firstCol = range.FirstColumn;
                    result = true;
                    break;
                }
            }
            //try
            //{
                //result = sheet.GetRow(rowIndex).GetCell(colIndex).IsMergedCell;
                //result = sheet.GetRow(rowNum).GetCell(colIndex).IsMergedCell;
            //}
            //catch
            //{
            //}
            return result;
        }


        public static List<Models.Beans.ContractLX> ParseLXExcel(string filePath)
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

                List<Models.Beans.ContractLX> list = new List<Models.Beans.ContractLX>();

                for (int i = sheet.FirstRowNum + 1; i <= rowCount; i++)
                {
                    try
                    {
                        Models.Beans.ContractLX model = new Models.Beans.ContractLX();

                        NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                        if (row == null) continue;

                        model.seq = row.GetCell(0) == null ? string.Empty : row.GetCell(0).ToString();
                        model.buytime = row.GetCell(1) == null ? string.Empty : row.GetCell(1).ToString();
                        model.projectnum = row.GetCell(2) == null ? string.Empty : row.GetCell(2).ToString();
                        model.department = row.GetCell(3) == null ? string.Empty : row.GetCell(3).ToString();
                        model.linker = row.GetCell(4) == null ? string.Empty : row.GetCell(4).ToString();
                        model.tel = row.GetCell(5) == null ? string.Empty : row.GetCell(5).ToString();
                        model.type = row.GetCell(6) == null ? string.Empty : row.GetCell(6).ToString();
                        model.content = row.GetCell(7) == null ? string.Empty : row.GetCell(7).ToString();
                        decimal price = 0;
                        if (row.GetCell(8) != null)
                        {
                            decimal.TryParse(row.GetCell(8).ToString(), out price);
                        }
                        model.price = price;
                        int count = 0;
                        if (row.GetCell(9) != null)
                        {
                            int.TryParse(row.GetCell(9).ToString(), out count);
                        }
                        model.count = count;

                        decimal total = 0;
                        if (row.GetCell(10) != null)
                        {
                            decimal.TryParse(row.GetCell(10).ToString(), out total);
                        }
                        model.total = total;

                        model.contractnum = row.GetCell(11) == null ? string.Empty : row.GetCell(11).ToString();
                        model.summaryofbuy = row.GetCell(12) == null ? string.Empty : row.GetCell(12).ToString();
                        model.validate = row.GetCell(13) == null ? string.Empty : row.GetCell(13).ToString();
                        model.super = row.GetCell(14) == null ? string.Empty : row.GetCell(14).ToString();
                        model.superlinker = row.GetCell(15) == null ? string.Empty : row.GetCell(15).ToString();
                        model.supertel = row.GetCell(16) == null ? string.Empty : row.GetCell(16).ToString();
                        model.chargedepartment = row.GetCell(17) == null ? string.Empty : row.GetCell(17).ToString();
                        model.remark = row.GetCell(18) == null ? string.Empty : row.GetCell(18).ToString();                       
                        model.payprogress = row.GetCell(19) == null ? string.Empty : row.GetCell(19).ToString();

                        if (CheckEmptyLine(model, row.GetCell(8), row.GetCell(9), row.GetCell(10))) continue;

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



        public static MemoryStream RenderToExcel(List<Models.Beans.ContractCG> list)
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

            CreateCell(row, 0, "采购时间", CellType.String, style);
            CreateCell(row, 1, "采购合同", CellType.String, style);
            CreateCell(row, 2, "采购编号", CellType.String, style);
            CreateCell(row, 3, "采购部门", CellType.String, style);
            CreateCell(row, 4, "采购联系人", CellType.String, style);
            CreateCell(row, 5, "联系电话", CellType.String, style);
            CreateCell(row, 6, "项目编号", CellType.String, style);
            CreateCell(row, 7, "经费来源", CellType.String, style);
            CreateCell(row, 8, "采购类型", CellType.String, style);
            CreateCell(row, 9, "采购内容", CellType.String, style);
            CreateCell(row, 10, "数量", CellType.String, style);
            CreateCell(row, 11, "单价", CellType.String, style);
            CreateCell(row, 12, "小计", CellType.String, style);
            CreateCell(row, 13, "总计", CellType.String, style);
            CreateCell(row, 14, "预算金额", CellType.String, style);
            CreateCell(row, 15, "供货单位", CellType.String, style);
            CreateCell(row, 16, "供货联系人", CellType.String, style);
            CreateCell(row, 17, "联系电话", CellType.String, style);
            CreateCell(row, 18, "结算金额", CellType.String, style);
            CreateCell(row, 19, "赠送内容", CellType.String, style);
            CreateCell(row, 20, "赠送价值", CellType.String, style);
            CreateCell(row, 21, "验收情况", CellType.String, style);
            CreateCell(row, 22, "记录位置", CellType.String, style);
            CreateCell(row, 23, "支付进度", CellType.String, style);
            CreateCell(row, 24, "分管部门", CellType.String, style);
            CreateCell(row, 25, "备注", CellType.String, style);           
            #endregion

            if (list != null && list.Count > 0)
            {
                int rowidx = 0;
                foreach (Models.Beans.ContractCG model in list)
                {
                    #region row
                    rowidx++;
                    row = sheet.CreateRow(rowidx);
                    CreateCell(row, 0, model.buytime);
                    CreateCell(row, 1, model.contractnum);
                    CreateCell(row, 2, model.seq);
                    CreateCell(row, 3, model.department);
                    CreateCell(row, 4, model.linker);
                    CreateCell(row, 5, model.tel);
                    CreateCell(row, 6, model.projectnum);
                    CreateCell(row, 7, model.fundsource);
                    CreateCell(row, 8, model.type);
                    CreateCell(row, 9, model.content);
                    CreateCell(row, 10, model.count.ToString());
                    CreateCell(row, 11, model.price.ToString());
                    CreateCell(row, 12, model.subtotal.ToString());
                    CreateCell(row, 13, model.total.ToString());
                    CreateCell(row, 14, model.budgetamount);
                    CreateCell(row, 15, model.super);
                    CreateCell(row, 16, model.superlinker);
                    CreateCell(row, 17, model.supertel);
                    CreateCell(row, 18, model.settleamount);
                    CreateCell(row, 19, model.freecontent);
                    CreateCell(row, 20, model.freevalue);
                    CreateCell(row, 21, model.validate);
                    CreateCell(row, 22, model.place);
                    CreateCell(row, 23, model.payprogress);
                    CreateCell(row, 24, model.chargedepartment);
                    CreateCell(row, 25, model.remark);                  
                    #endregion
                }
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            return ms;
        }


        public static MemoryStream RenderToExcel(List<Models.Beans.ContractLX> list)
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

            CreateCell(row, 0, "采购编号", CellType.String, style);
            CreateCell(row, 1, "采购时间", CellType.String, style);
            CreateCell(row, 2, "项目编号", CellType.String, style);
                    
            CreateCell(row, 3, "采购部门", CellType.String, style);
            CreateCell(row, 4, "采购联系人", CellType.String, style);
            CreateCell(row, 5, "联系电话", CellType.String, style);
            
            CreateCell(row, 6, "经费来源", CellType.String, style);
            CreateCell(row, 7, "采购类型", CellType.String, style);
            CreateCell(row, 8, "采购内容", CellType.String, style);
            CreateCell(row, 9, "单价", CellType.String, style);
            CreateCell(row, 10, "数量", CellType.String, style);
            CreateCell(row, 11, "总价", CellType.String, style);
            CreateCell(row, 12, "采购合同", CellType.String, style);
            CreateCell(row, 13, "采购纪要", CellType.String, style); 
            CreateCell(row, 14, "预算金额", CellType.String, style);
            CreateCell(row, 15, "供货单位", CellType.String, style);
            CreateCell(row, 16, "供货联系人", CellType.String, style);
            CreateCell(row, 17, "联系电话", CellType.String, style);
            CreateCell(row, 18, "结算金额", CellType.String, style);
            CreateCell(row, 19, "赠送内容", CellType.String, style);
            CreateCell(row, 20, "赠送价值", CellType.String, style);
            CreateCell(row, 21, "验收情况", CellType.String, style);
            CreateCell(row, 22, "记录位置", CellType.String, style);
            CreateCell(row, 23, "支付进度", CellType.String, style);
            CreateCell(row, 24, "分管部门", CellType.String, style);
            CreateCell(row, 25, "备注", CellType.String, style);
            #endregion

            if (list != null && list.Count > 0)
            {
                int rowidx = 0;
                foreach (Models.Beans.ContractLX model in list)
                {
                    #region row
                    rowidx++;
                    row = sheet.CreateRow(rowidx);
                    CreateCell(row, 0, model.seq);
                    CreateCell(row, 1, model.buytime);
                    CreateCell(row, 2, model.projectnum);
                                       
                    CreateCell(row, 3, model.department);
                    CreateCell(row, 4, model.linker);
                    CreateCell(row, 5, model.tel);
                    
                    CreateCell(row, 6, model.fundsource);
                    CreateCell(row, 7, model.type);
                    CreateCell(row, 8, model.content);
                    CreateCell(row, 9, model.price.ToString());
                    CreateCell(row, 10, model.count.ToString());
                    CreateCell(row, 11, model.total.ToString());
                    CreateCell(row, 12, model.contractnum);
                    CreateCell(row, 13, model.summaryofbuy); 
                    CreateCell(row, 14, model.budgetamount);
                    CreateCell(row, 15, model.super);
                    CreateCell(row, 16, model.superlinker);
                    CreateCell(row, 17, model.supertel);
                    CreateCell(row, 18, model.settleamount);
                    CreateCell(row, 19, model.freecontent);
                    CreateCell(row, 20, model.freevalue);
                    CreateCell(row, 21, model.validate);
                    CreateCell(row, 22, model.place);
                    CreateCell(row, 23, model.payprogress);
                    CreateCell(row, 24, model.chargedepartment);
                    CreateCell(row, 25, model.remark);
                    #endregion
                }
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            return ms;
        }

    }
}