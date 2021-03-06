﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Web;
using System.Data;
using ContractMvcWeb.Models.Beans;

namespace ContractMvcWeb.Models
{
    public class ContractCGContext
    {
        public bool ExistContractBySeqAndContent( string seq, string content)
        {
            string sql = string.Format(" select count(1) from t_contract_cg where seq='{0}' and content='{1}'", seq, content);
            object obj = MySqlHelper.GetSingle(sql);
            if (obj == null) return false;
            int count = 0;
            int.TryParse(obj.ToString(), out count);
            return count > 0 ? true : false;
        }

        public bool AddContract(Beans.ContractCG model)
        {
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into t_contract_cg(");
            strSql.Append("seq,type,content,price,count,subtotal,total,contractnum,department,linker,tel,projectnum,budgetamount,fundsource,super,superlinker,supertel,settleamount,freecontent,freevalue,validate,remark,payprogress,chargedepartment,place,operatorId,operatorName,createtime,modifytime,buytime)");
			strSql.Append(" values (");
            strSql.Append("@seq,@type,@content,@price,@count,@subtotal,@total,@contractnum,@department,@linker,@tel,@projectnum,@budgetamount,@fundsource,@super,@superlinker,@supertel,@settleamount,@freecontent,@freevalue,@validate,@remark,@payprogress,@chargedepartment,@place,@operatorId,@operatorName,@createtime,@modifytime,@buytime)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@seq", MySqlDbType.VarChar,50),
					new MySqlParameter("@type", MySqlDbType.VarChar,100),
					new MySqlParameter("@content", MySqlDbType.VarChar,255),
					new MySqlParameter("@price", MySqlDbType.Decimal,10),
					new MySqlParameter("@count", MySqlDbType.Int32,11),
					new MySqlParameter("@subtotal", MySqlDbType.Decimal,12),
					new MySqlParameter("@total", MySqlDbType.Decimal,12),
					new MySqlParameter("@contractnum", MySqlDbType.VarChar,255),
					new MySqlParameter("@department", MySqlDbType.VarChar,255),
					new MySqlParameter("@linker", MySqlDbType.VarChar,255),
					new MySqlParameter("@tel", MySqlDbType.VarChar,255),
					new MySqlParameter("@projectnum", MySqlDbType.VarChar,50),
					new MySqlParameter("@budgetamount", MySqlDbType.VarChar,50),
					new MySqlParameter("@fundsource", MySqlDbType.VarChar,255),
					new MySqlParameter("@super", MySqlDbType.VarChar,255),
					new MySqlParameter("@superlinker", MySqlDbType.VarChar,255),
					new MySqlParameter("@supertel", MySqlDbType.VarChar,255),
					new MySqlParameter("@settleamount", MySqlDbType.VarChar,255),
					new MySqlParameter("@freecontent", MySqlDbType.VarChar,255),
					new MySqlParameter("@freevalue", MySqlDbType.VarChar,255),
					new MySqlParameter("@validate", MySqlDbType.VarChar,255),
					new MySqlParameter("@remark", MySqlDbType.VarChar,255),
					new MySqlParameter("@payprogress", MySqlDbType.VarChar,255),
					new MySqlParameter("@chargedepartment", MySqlDbType.VarChar,255),
					new MySqlParameter("@place", MySqlDbType.VarChar,255),
					new MySqlParameter("@operatorId", MySqlDbType.VarChar,255),
					new MySqlParameter("@operatorName", MySqlDbType.VarChar,255),
					new MySqlParameter("@createtime", MySqlDbType.Timestamp),
					new MySqlParameter("@modifytime", MySqlDbType.Timestamp),
                    new MySqlParameter("@buytime",MySqlDbType.VarChar,255)                      
                                          };
			parameters[0].Value = model.seq;
			parameters[1].Value = model.type;
			parameters[2].Value = model.content;
			parameters[3].Value = model.price;
			parameters[4].Value = model.count;
			parameters[5].Value = model.subtotal;
			parameters[6].Value = model.total;
			parameters[7].Value = model.contractnum;
			parameters[8].Value = model.department;
			parameters[9].Value = model.linker;
			parameters[10].Value = model.tel;
			parameters[11].Value = model.projectnum;
			parameters[12].Value = model.budgetamount;
			parameters[13].Value = model.fundsource;
			parameters[14].Value = model.super;
			parameters[15].Value = model.superlinker;
			parameters[16].Value = model.supertel;
			parameters[17].Value = model.settleamount;
			parameters[18].Value = model.freecontent;
			parameters[19].Value = model.freevalue;
			parameters[20].Value = model.validate;
			parameters[21].Value = model.remark;
			parameters[22].Value = model.payprogress;
			parameters[23].Value = model.chargedepartment;
			parameters[24].Value = model.place;
			parameters[25].Value = model.operatorId;
			parameters[26].Value = model.operatorName;
			parameters[27].Value = model.createtime;
			parameters[28].Value = model.modifytime;
            parameters[29].Value = model.buytime;

            int rows = MySqlHelper.ExecuteSql(strSql.ToString(), parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}		
        }

        public Beans.BatchImportResult BatchAddContracts(List<Beans.ContractCG> list, string operatorName)
        {
            Beans.BatchImportResult result = new BatchImportResult();

            if (list == null || list.Count < 1) return result;

            if (CheckData(list, out result) == false) return result;

            result = new BatchImportResult();
            result.TotalCount = list.Count;

            int addcount = 0;
            int updatecount = 0;
            int fail = 0;
            List<Beans.BatchImportResult.ExcelErrorLine> errLines = new List<BatchImportResult.ExcelErrorLine>();
            int idx = 1; //因为excel 从第二行开始数据行
            foreach (ContractCG c in list)
            {
                idx++;
                bool isExist = ExistContractBySeqAndContent( c.seq, c.content );
                if (isExist)
                {
                    bool isSuccess = UpdateBySeqAndContent(c); // EditContractByProjectNumAndProjectName(c);
                    updatecount += isSuccess ? 1 : 0;
                    fail += isSuccess ? 0 : 1;
                    if (isSuccess == false) errLines.Add(new BatchImportResult.ExcelErrorLine(idx.ToString(), "更新失败"));
                }
                else
                {
                    c.operatorName = operatorName;
                    bool isSuccess = AddContract(c);
                    addcount += isSuccess ? 1 : 0;
                    fail += isSuccess ? 0 : 1;
                    if (isSuccess == false) errLines.Add(new BatchImportResult.ExcelErrorLine(idx.ToString(), "新增失败"));
                }
            }

            result.AddCount = addcount;
            result.UpdateCount = updatecount;
            result.FailureCount = fail;

            return result;
        }

        protected bool CheckData(List<Beans.ContractCG> list, out Beans.BatchImportResult result)
        {
            result = new BatchImportResult();
            if (list == null) return true;

            result.ErrorList = new List<BatchImportResult.ExcelErrorLine>();
            bool isok = true;
            for (int i = 0; i < list.Count; i++)
            {
                string msg = "";
                //if (string.IsNullOrEmpty(list[i].contractnum))
                //{
                //    isok = false;
                //    msg += "合同编号不能空";
                //}

                if (string.IsNullOrEmpty(list[i].seq))
                {
                    isok = false;
                    if (msg != "")
                    {
                        msg += ",";
                    }
                    msg += "采购编号不能空";
                }
                //if (string.IsNullOrEmpty(list[i].projectnum))
                //{
                //    isok = false;
                //    if (msg != "")
                //    {
                //        msg += ",";
                //    }
                //    msg += "项目编号不能空";
                //}
                if (string.IsNullOrEmpty(list[i].content))
                {
                    isok = false;
                    if (msg != "")
                    {
                        msg += ",";
                    }
                    msg += "采购内容不能空";
                }

                //if (string.IsNullOrEmpty(list[i].projectnum) || string.IsNullOrEmpty(list[i].projectname))
                //{
                //    isok = false;
                //    if (msg != "")
                //    {
                //        msg += ",";
                //    }
                //    msg += "项目编号和项目名称不能空";
                //}
                //if (string.IsNullOrEmpty(list[i].packageBudget) == false)
                //{
                //    decimal fbys = 0;
                //    if (decimal.TryParse(list[i].packageBudget, out fbys) == false)
                //    {
                //        isok = false;
                //        if (msg != "")
                //        {
                //            msg += ",";
                //        }
                //        msg += "分包预算必须是数字";
                //    }
                //}
                //else
                //{
                //    list[i].packageBudget = "0.00";
                //}

                //if (string.IsNullOrEmpty(list[i].money) == false)
                //{
                //    decimal zbje = 0;
                //    if (decimal.TryParse(list[i].money, out zbje) == false)
                //    {
                //        isok = false;
                //        if (msg != "")
                //        {
                //            msg += ",";
                //        }
                //        msg += "中标金额必须是数字";
                //    }
                //}
                //else
                //{
                //    list[i].money = "0.00";
                //}

                if (msg != "")
                {
                    result.ErrorList.Add(new BatchImportResult.ExcelErrorLine((i + 2).ToString(), msg));
                }
            }
            return isok;

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateBySeqAndContent( Models.Beans.ContractCG model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_contract_cg set ");
            strSql.Append("seq=@seq,");
            strSql.Append("type=@type,");
            strSql.Append("content=@content,");
            strSql.Append("price=@price,");
            strSql.Append("count=@count,");
            strSql.Append("subtotal=@subtotal,");
            strSql.Append("total=@total,");
            strSql.Append("contractnum=@contractnum,");
            strSql.Append("department=@department,");
            strSql.Append("linker=@linker,");
            strSql.Append("tel=@tel,");
            strSql.Append("projectnum=@projectnum,");
            strSql.Append("budgetamount=@budgetamount,");
            strSql.Append("fundsource=@fundsource,");
            strSql.Append("super=@super,");
            strSql.Append("superlinker=@superlinker,");
            strSql.Append("supertel=@supertel,");
            strSql.Append("settleamount=@settleamount,");
            strSql.Append("freecontent=@freecontent,");
            strSql.Append("freevalue=@freevalue,");
            strSql.Append("validate=@validate,");
            strSql.Append("remark=@remark,");
            strSql.Append("payprogress=@payprogress,");
            strSql.Append("chargedepartment=@chargedepartment,");
            strSql.Append("place=@place,");
            strSql.Append("operatorId=@operatorId,");
            strSql.Append("operatorName=@operatorName,");
            strSql.Append("modifytime=@modifytime,");
            strSql.Append("buytime=@buytime");
            strSql.Append(" where seq=@seq2 and content=@content2");
            MySqlParameter[] parameters = {                    
					new MySqlParameter("@seq", MySqlDbType.VarChar,50),
					new MySqlParameter("@type", MySqlDbType.VarChar,100),
					new MySqlParameter("@content", MySqlDbType.VarChar,255),
					new MySqlParameter("@price", MySqlDbType.Decimal,10),
					new MySqlParameter("@count", MySqlDbType.Int32,11),
					new MySqlParameter("@subtotal", MySqlDbType.Decimal,12),
					new MySqlParameter("@total", MySqlDbType.Decimal,12),
					new MySqlParameter("@contractnum", MySqlDbType.VarChar,255),
					new MySqlParameter("@department", MySqlDbType.VarChar,255),
					new MySqlParameter("@linker", MySqlDbType.VarChar,255),
					new MySqlParameter("@tel", MySqlDbType.VarChar,255),
					new MySqlParameter("@projectnum", MySqlDbType.VarChar,50),
					new MySqlParameter("@budgetamount", MySqlDbType.VarChar,50),
					new MySqlParameter("@fundsource", MySqlDbType.VarChar,255),
					new MySqlParameter("@super", MySqlDbType.VarChar,255),
					new MySqlParameter("@superlinker", MySqlDbType.VarChar,255),
					new MySqlParameter("@supertel", MySqlDbType.VarChar,255),
					new MySqlParameter("@settleamount", MySqlDbType.VarChar,255),
					new MySqlParameter("@freecontent", MySqlDbType.VarChar,255),
					new MySqlParameter("@freevalue", MySqlDbType.VarChar,255),
					new MySqlParameter("@validate", MySqlDbType.VarChar,255),
					new MySqlParameter("@remark", MySqlDbType.VarChar,255),
					new MySqlParameter("@payprogress", MySqlDbType.VarChar,255),
					new MySqlParameter("@chargedepartment", MySqlDbType.VarChar,255),
					new MySqlParameter("@place", MySqlDbType.VarChar,255),
					new MySqlParameter("@operatorId", MySqlDbType.VarChar,255),
					new MySqlParameter("@operatorName", MySqlDbType.VarChar,255),
                    new MySqlParameter("@modifytime",MySqlDbType.Timestamp),                    
                    new MySqlParameter("@buytime",MySqlDbType.VarChar,255),
                    new MySqlParameter("@seq2", MySqlDbType.VarChar,50),
                    new MySqlParameter("@content2", MySqlDbType.VarChar,255)
					};
            parameters[0].Value = model.seq;
            parameters[1].Value = model.type;
            parameters[2].Value = model.content;
            parameters[3].Value = model.price;
            parameters[4].Value = model.count;
            parameters[5].Value = model.subtotal;
            parameters[6].Value = model.total;
            parameters[7].Value = model.contractnum;
            parameters[8].Value = model.department;
            parameters[9].Value = model.linker;
            parameters[10].Value = model.tel;
            parameters[11].Value = model.projectnum;
            parameters[12].Value = model.budgetamount;
            parameters[13].Value = model.fundsource;
            parameters[14].Value = model.super;
            parameters[15].Value = model.superlinker;
            parameters[16].Value = model.supertel;
            parameters[17].Value = model.settleamount;
            parameters[18].Value = model.freecontent;
            parameters[19].Value = model.freevalue;
            parameters[20].Value = model.validate;
            parameters[21].Value = model.remark;
            parameters[22].Value = model.payprogress;
            parameters[23].Value = model.chargedepartment;
            parameters[24].Value = model.place;
            parameters[25].Value = model.operatorId;
            parameters[26].Value = model.operatorName;
            parameters[27].Value = DateTime.Now;
            parameters[28].Value = model.buytime;
            parameters[29].Value = model.seq;
            parameters[30].Value = model.content;

            int rows = MySqlHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Page<ContractCG> QueryByPage(ContractCG query, int pageidx, int pagesize = 20)
        {
            Page<ContractCG> page = new Page<ContractCG>();
            page.PageIdx = pageidx;
            page.PageSize = pagesize;

            string where = GetWhere(query);
            string limit = string.Format(" limit {0} , {1}", pageidx < 1 ? 0 : (pageidx - 1) * pagesize, pagesize);
            string orderby = GetSortString(query.sortkey, query.sorttype); //"order by modifytime desc";
            string sql = string.Format("select count(1) from t_contract_cg where {0} ", where);
            int totalrecord = 0;
            object obj = MySqlHelper.GetSingle(sql);
            if (obj == null) totalrecord = 0;
            int.TryParse(obj.ToString(), out totalrecord);
            int totalPages = 0;
            totalPages = totalrecord / pagesize;
            totalPages += totalrecord % pagesize == 0 ? 0 : 1;
            page.TotalPages = totalPages;
            page.TotalRecords = totalrecord;
            sql = string.Format(" select * from t_contract_cg where {0} {1} {2}", where, orderby, limit);
            DataSet ds = MySqlHelper.Query(sql);
            if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1) return page;
            int count = ds.Tables[0].Rows.Count;
            List<ContractCG> list = new List<ContractCG>();
            for (int i = 0; i < count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                ContractCG model = DataRowToContractCG(row);
                list.Add(model);
            }
            page.Data = list;
            return page;
        }

        protected string GetWhere(ContractCG query)
        {        
            query.pkey = FilterSpecial(query.pkey);
            query.pvalue = FilterSpecial(query.pvalue);

            string where = " 1=1 ";           
          
            if (string.IsNullOrEmpty(query.pkey) == false && string.IsNullOrEmpty(query.pvalue) == false)
            {
                if (query.pkey.ToLower().Trim().Equals("all"))
                {
                    where += " and " + FullSearch(query.pvalue);
                }
                else
                {
                    where += string.Format(" and {0} like '%{1}%'", query.pkey, query.pvalue);
                }
            }

            return where;
        }

        protected string FullSearch(string key)
        {
            string where = "";

            Type type = typeof(ContractCG);
            System.Reflection.PropertyInfo[] properties = type.GetProperties();
            if (properties == null || properties.Length < 1) return where;
            for (int i = 0; i < properties.Length; i++)
            {
                String name = properties[i].Name;
                if (properties[i].PropertyType != typeof(string)) continue;
                if (name.Equals("sortkey")) continue;
                if (name.Equals("sorttype")) continue;
                if (name.Equals("pkey")) continue;
                if (name.Equals("pvalue")) continue;
                if (name.Equals("operatorid")) continue;
                if (name.Equals("operatorname")) continue;

                if (string.IsNullOrEmpty(where) == false)
                {
                    where += " or ";
                }

                where += string.Format(" ( {0} like '%{1}%' ) ", name, key);
            }

            return where;
        }


        protected string GetSortString(string sortkey, string sorttype)
        {
            string sortString = "order by modifytime desc";
            if (string.IsNullOrEmpty(sortkey) == false && string.IsNullOrEmpty(sorttype) == false)
            {
                sortString = " order by " + sortkey + " " + sorttype;
            }
            return sortString;
        }

        protected string FilterSpecial(string txt)
        {
            if (string.IsNullOrEmpty(txt)) return txt;
            return txt.Replace("'", "");
        }

        protected ContractCG DataRowToContractCG(DataRow row)
        {
            Models.Beans.ContractCG model = new Models.Beans.ContractCG();
            if (row != null)
            {
                if (row["contractid"] != null && row["contractid"].ToString() != "")
                {
                    model.contractid = int.Parse(row["contractid"].ToString());
                }
                if (row["seq"] != null)
                {
                    model.seq = row["seq"].ToString();
                }
                if (row["type"] != null)
                {
                    model.type = row["type"].ToString();
                }
                if (row["content"] != null)
                {
                    model.content = row["content"].ToString();
                }
                if (row["price"] != null && row["price"].ToString() != "")
                {
                    model.price = decimal.Parse(row["price"].ToString());
                }
                if (row["count"] != null && row["count"].ToString() != "")
                {
                    model.count = int.Parse(row["count"].ToString());
                }
                if (row["subtotal"] != null && row["subtotal"].ToString() != "")
                {
                    model.subtotal = decimal.Parse(row["subtotal"].ToString());
                }
                if (row["total"] != null && row["total"].ToString() != "")
                {
                    model.total = decimal.Parse(row["total"].ToString());
                }
                if (row["contractnum"] != null)
                {
                    model.contractnum = row["contractnum"].ToString();
                }
                if (row["department"] != null)
                {
                    model.department = row["department"].ToString();
                }
                if (row["linker"] != null)
                {
                    model.linker = row["linker"].ToString();
                }
                if (row["tel"] != null)
                {
                    model.tel = row["tel"].ToString();
                }
                if (row["projectnum"] != null)
                {
                    model.projectnum = row["projectnum"].ToString();
                }
                if (row["budgetamount"] != null)
                {
                    model.budgetamount = row["budgetamount"].ToString();
                }
                if (row["fundsource"] != null)
                {
                    model.fundsource = row["fundsource"].ToString();
                }
                if (row["super"] != null)
                {
                    model.super = row["super"].ToString();
                }
                if (row["superlinker"] != null)
                {
                    model.superlinker = row["superlinker"].ToString();
                }
                if (row["supertel"] != null)
                {
                    model.supertel = row["supertel"].ToString();
                }
                if (row["settleamount"] != null)
                {
                    model.settleamount = row["settleamount"].ToString();
                }
                if (row["freecontent"] != null)
                {
                    model.freecontent = row["freecontent"].ToString();
                }
                if (row["freevalue"] != null)
                {
                    model.freevalue = row["freevalue"].ToString();
                }
                if (row["validate"] != null)
                {
                    model.validate = row["validate"].ToString();
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
                }
                if (row["payprogress"] != null)
                {
                    model.payprogress = row["payprogress"].ToString();
                }
                if (row["chargedepartment"] != null)
                {
                    model.chargedepartment = row["chargedepartment"].ToString();
                }
                if (row["place"] != null)
                {
                    model.place = row["place"].ToString();
                }
                if (row["operatorId"] != null)
                {
                    model.operatorId = row["operatorId"].ToString();
                }
                if (row["operatorName"] != null)
                {
                    model.operatorName = row["operatorName"].ToString();
                }
                if (row["createtime"] != null && row["createtime"].ToString() != "")
                {
                    model.createtime = DateTime.Parse(row["createtime"].ToString());
                }
                if (row["modifytime"] != null && row["modifytime"].ToString() != "")
                {
                    model.modifytime = DateTime.Parse(row["modifytime"].ToString());
                }
                if( row["buytime"] !=null )
                {
                    model.buytime = row["buytime"].ToString();
                }
            }
            return model;
        }


        public bool DeleteContracts(List<int> contractIds)
        {
            string ids = string.Empty;
            foreach (int id in contractIds)
            {
                if (string.IsNullOrEmpty(ids) == false)
                {
                    ids += ",";
                }
                ids += id;
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from t_contract_cg where contractid in( " + ids + " )");
            int count = MySqlHelper.ExecuteSql(strSql.ToString());
            return count > 0 ? true : false;
        }

        public List<ContractCG> Query(ContractCG query)
        {
            string where = GetWhere(query);
            string orderby = "order by modifytime desc";
            string sql = string.Format(" select * from t_contract_cg where {0} {1}", where, orderby);
            DataSet ds = MySqlHelper.Query(sql);
            if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1) return null;
            int count = ds.Tables[0].Rows.Count;
            List<ContractCG> list = new List<ContractCG>();
            for (int i = 0; i < count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                ContractCG model = DataRowToContractCG(row);
                list.Add(model);
            }
            return list;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ContractCG GetModel(int contractid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select contractid,seq,type,content,price,count,subtotal,total,contractnum,department,linker,tel,projectnum,budgetamount,fundsource,super,superlinker,supertel,settleamount,freecontent,freevalue,validate,remark,payprogress,chargedepartment,place,operatorId,operatorName,createtime,modifytime,buytime from t_contract_cg ");
            strSql.Append(" where contractid=@contractid");
            MySqlParameter[] parameters = {
					new MySqlParameter("@contractid", MySqlDbType.Int32)
			};
            parameters[0].Value = contractid;

            ContractCG model = new ContractCG();
            DataSet ds = MySqlHelper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToContractCG(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        public bool ExistContractBySeqAndContent(string seq, string content , int contractid)
        {
            string sql = string.Format(" select count(1) from t_contract_cg where contractid !={0} and seq='{1}' and content='{2}'", contractid, seq, content );
            object obj = MySqlHelper.GetSingle(sql);
            if (obj == null) return false;
            int count = 0;
            int.TryParse(obj.ToString(), out count);
            return count > 0 ? true : false;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ContractCG model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_contract_cg set ");
            strSql.Append("seq=@seq,");
            strSql.Append("type=@type,");
            strSql.Append("content=@content,");
            strSql.Append("price=@price,");
            strSql.Append("count=@count,");
            strSql.Append("subtotal=@subtotal,");
            strSql.Append("total=@total,");
            strSql.Append("contractnum=@contractnum,");
            strSql.Append("department=@department,");
            strSql.Append("linker=@linker,");
            strSql.Append("tel=@tel,");
            strSql.Append("projectnum=@projectnum,");
            strSql.Append("budgetamount=@budgetamount,");
            strSql.Append("fundsource=@fundsource,");
            strSql.Append("super=@super,");
            strSql.Append("superlinker=@superlinker,");
            strSql.Append("supertel=@supertel,");
            strSql.Append("settleamount=@settleamount,");
            strSql.Append("freecontent=@freecontent,");
            strSql.Append("freevalue=@freevalue,");
            strSql.Append("validate=@validate,");
            strSql.Append("remark=@remark,");
            strSql.Append("payprogress=@payprogress,");
            strSql.Append("chargedepartment=@chargedepartment,");
            strSql.Append("place=@place,");
            strSql.Append("operatorId=@operatorId,");
            strSql.Append("operatorName=@operatorName,");
            strSql.Append("buytime=@buytime");
            strSql.Append(" where contractid=@contractid");
            MySqlParameter[] parameters = {
					new MySqlParameter("@seq", MySqlDbType.VarChar,50),
					new MySqlParameter("@type", MySqlDbType.VarChar,100),
					new MySqlParameter("@content", MySqlDbType.VarChar,255),
					new MySqlParameter("@price", MySqlDbType.Decimal,10),
					new MySqlParameter("@count", MySqlDbType.Int32,11),
					new MySqlParameter("@subtotal", MySqlDbType.Decimal,12),
					new MySqlParameter("@total", MySqlDbType.Decimal,12),
					new MySqlParameter("@contractnum", MySqlDbType.VarChar,255),
					new MySqlParameter("@department", MySqlDbType.VarChar,255),
					new MySqlParameter("@linker", MySqlDbType.VarChar,255),
					new MySqlParameter("@tel", MySqlDbType.VarChar,255),
					new MySqlParameter("@projectnum", MySqlDbType.VarChar,50),
					new MySqlParameter("@budgetamount", MySqlDbType.VarChar,50),
					new MySqlParameter("@fundsource", MySqlDbType.VarChar,255),
					new MySqlParameter("@super", MySqlDbType.VarChar,255),
					new MySqlParameter("@superlinker", MySqlDbType.VarChar,255),
					new MySqlParameter("@supertel", MySqlDbType.VarChar,255),
					new MySqlParameter("@settleamount", MySqlDbType.VarChar,255),
					new MySqlParameter("@freecontent", MySqlDbType.VarChar,255),
					new MySqlParameter("@freevalue", MySqlDbType.VarChar,255),
					new MySqlParameter("@validate", MySqlDbType.VarChar,255),
					new MySqlParameter("@remark", MySqlDbType.VarChar,255),
					new MySqlParameter("@payprogress", MySqlDbType.VarChar,255),
					new MySqlParameter("@chargedepartment", MySqlDbType.VarChar,255),
					new MySqlParameter("@place", MySqlDbType.VarChar,255),
					new MySqlParameter("@operatorId", MySqlDbType.VarChar,255),
					new MySqlParameter("@operatorName", MySqlDbType.VarChar,255),
                    new MySqlParameter("@buytime",MySqlDbType.VarChar,255),
					new MySqlParameter("@contractid", MySqlDbType.Int32,11)};
            parameters[0].Value = model.seq;
            parameters[1].Value = model.type;
            parameters[2].Value = model.content;
            parameters[3].Value = model.price;
            parameters[4].Value = model.count;
            parameters[5].Value = model.subtotal;
            parameters[6].Value = model.total;
            parameters[7].Value = model.contractnum;
            parameters[8].Value = model.department;
            parameters[9].Value = model.linker;
            parameters[10].Value = model.tel;
            parameters[11].Value = model.projectnum;
            parameters[12].Value = model.budgetamount;
            parameters[13].Value = model.fundsource;
            parameters[14].Value = model.super;
            parameters[15].Value = model.superlinker;
            parameters[16].Value = model.supertel;
            parameters[17].Value = model.settleamount;
            parameters[18].Value = model.freecontent;
            parameters[19].Value = model.freevalue;
            parameters[20].Value = model.validate;
            parameters[21].Value = model.remark;
            parameters[22].Value = model.payprogress;
            parameters[23].Value = model.chargedepartment;
            parameters[24].Value = model.place;
            parameters[25].Value = model.operatorId;
            parameters[26].Value = model.operatorName;
            parameters[27].Value = model.buytime;
            parameters[28].Value = model.contractid;

            int rows = MySqlHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}