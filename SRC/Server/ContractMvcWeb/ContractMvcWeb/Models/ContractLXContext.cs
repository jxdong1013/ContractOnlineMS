using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ContractMvcWeb.Models.Beans;

namespace ContractMvcWeb.Models
{
    public class ContractLXContext
    {
        public Page<ContractLX> QueryByPage(ContractLX query, int pageidx, int pagesize = 20)
        {
            Page<ContractLX> page = new Page<ContractLX>();
            page.PageIdx = pageidx;
            page.PageSize = pagesize;
            string where = GetWhere(query);
            string limit = string.Format(" limit {0} , {1}", pageidx < 1 ? 0 : (pageidx - 1) * pagesize, pagesize);
            string orderby = GetSortString(query.sortkey, query.sorttype); //"order by modifytime desc";
            string sql = string.Format("select count(1) from t_contract_lx where {0} ", where);
            int totalrecord = 0;
            object obj = MySqlHelper.GetSingle(sql);
            if (obj == null) totalrecord = 0;
            int.TryParse(obj.ToString(), out totalrecord);
            int totalPages = 0;
            totalPages = totalrecord / pagesize;
            totalPages += totalrecord % pagesize == 0 ? 0 : 1;
            page.TotalPages = totalPages;
            page.TotalRecords = totalrecord;
            sql = string.Format(" select * from t_contract_lx where {0} {1} {2}", where, orderby, limit);
            DataSet ds = MySqlHelper.Query(sql);
            if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1) return page;
            int count = ds.Tables[0].Rows.Count;
            List<ContractLX> list = new List<ContractLX>();
            for (int i = 0; i < count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                ContractLX model = DataRowToContractCG(row);
                list.Add(model);
            }
            page.Data = list;
            return page;
        }

        protected string GetWhere(ContractLX query)
        {
            query.pkey = FilterSpecial(query.pkey);
            query.pvalue = FilterSpecial(query.pvalue);

            string where = " 1=1 ";

            if (string.IsNullOrEmpty(query.pkey) == false && string.IsNullOrEmpty(query.pvalue) == false)
            {
                where += string.Format(" and {0} like '%{1}%'", query.pkey, query.pvalue);
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

        protected ContractLX DataRowToContractCG(DataRow row)
        {
            Models.Beans.ContractLX model = new Models.Beans.ContractLX();
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
            }
            return model;
        }

    }
}