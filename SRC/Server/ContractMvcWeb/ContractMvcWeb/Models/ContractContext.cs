using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Text;
using MySql.Data.MySqlClient;
using ContractMvcWeb.Models.Beans;

namespace ContractMvcWeb.Models
{
    public class ContractContext
    {
        public Page<Contract> QueryByPage(Contract query, int pageidx, int pagesize = 20)
        {
            Page<Contract > page= new Page<Contract> ();
            page.PageIdx = pageidx;
            page.PageSize = pagesize;            

            string where = GetWhere(query);
            string limit = string.Format( " limit {0} , {1}" , pageidx <1 ? 0 : (pageidx-1)* pagesize , pagesize );
            string orderby = GetSortString( query.sortkey , query.sorttype ); //"order by modifytime desc";
            string sql = string.Format( "select count(1) from t_contract where {0} " , where );
            int totalrecord = 0;
            object obj = MySqlHelper.GetSingle( sql );
            if( obj ==null ) totalrecord =0;
            int.TryParse ( obj.ToString () , out totalrecord );
            int totalPages = 0;
            totalPages = totalrecord / pagesize ;
            totalPages += totalrecord% pagesize == 0? 0: 1;
            page.TotalPages = totalPages;
            page.TotalRecords = totalrecord;
            sql = string.Format ( " select * from t_contract where {0} {1} {2}", where , orderby , limit );
            DataSet ds = MySqlHelper.Query(sql);
            if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1) return page;
            int count = ds.Tables[0].Rows.Count;
            List<Contract> list = new List<Contract>();
            for (int i = 0; i < count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                Contract model = DataRowToContract(row);
                list.Add( model );
            }
            page.Data = list;      
            return page;
        }

        protected string GetSortString(string sortkey , string sorttype)
        {
            string sortString =   "order by modifytime desc";
            if (string.IsNullOrEmpty(sortkey) == false && string.IsNullOrEmpty(sorttype)==false)
            {
                sortString = " order by "+ sortkey + " "+ sorttype;
            }
            return sortString;
        }

        protected Contract DataRowToContract(DataRow row)
        {
            Contract model = new Contract();
            if (row["contractid"].ToString() != "")
            {
                model.contractid = int.Parse(row["contractid"].ToString());
            }
            model.seq = row["seq"].ToString();
            model.contractnum = row["contractnum"].ToString();
            model.contractname = row["contractname"].ToString();
            model.contractstate = row["contractstate"].ToString();
            model.unshelve = row["unshelve"].ToString();
            model.projectnum = row["projectnum"].ToString();
            model.projectname = row["projectname"].ToString();
            model.projectmanager = row["projectmanager"].ToString();
            model.tel = row["tel"].ToString();
            model.depart = row["depart"].ToString();
            model.linker = row["linker"].ToString();
            model.paymethod = row["paymethod"].ToString();
            model.money = row["money"].ToString();
            model.contractplace = row["contractplace"].ToString();
            model.contractrfid = row["contractrfid"].ToString();
            model.bcompany = row["bcompany"].ToString();
            model.signingdate = row["signingdate"].ToString();
            model.packageName = row["packageName"].ToString();
            model.packageBudget = row["packageBudget"].ToString();
            model.tendarNum = row["tendarNum"].ToString();
            model.tendarCompany = row["tendarCompany"].ToString();
            model.tendarStartTime = row["tendarStartTime"].ToString();
            model.phone = row["phone"].ToString();
            model.deliveryTime = row["deliveryTime"].ToString();
            model.inspection = row["inspection"].ToString();
            model.progress = row["progress"].ToString();
            model.isPayAll = row["isPayAll"].ToString();
            model.isArmoured = row["isArmoured"].ToString();
            model.isRefund = row["isRefund"].ToString();
            model.remark = row["remark"].ToString();
            model.operatorId = row["operatorId"].ToString();
            model.operatorName = row["operatorName"].ToString();
            model.createtime = DateTime.Parse(row["createtime"].ToString());
            model.modifytime = DateTime.Parse(row["modifytime"].ToString());
            int type = 0;
            if( row["type"].ToString() != "")
            {
                int.TryParse(row["type"].ToString(), out type);
                model.type = type;
            }

            return model;
        }

        public List<Contract> Query(Contract query)
        {
            string where = GetWhere(query);
            string orderby = "order by modifytime desc";
            string sql = string.Format(" select * from t_contract where {0} {1}", where, orderby);
            DataSet ds = MySqlHelper.Query(sql);
            if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1) return null;
            int count = ds.Tables[0].Rows.Count;
            List<Contract> list = new List<Contract>();
            for (int i = 0; i < count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                Contract model = DataRowToContract(row);
                list.Add(model);
            }
            return list;
        }

        protected string GetWhere(Contract query)
        {
            query.seq = FilterSpecial(query.seq);
            query.contractnum = FilterSpecial(query.contractnum);
            query.projectnum = FilterSpecial(query.projectnum);
            query.projectname = FilterSpecial(query.projectname);
            query.contractrfid = FilterSpecial(query.contractrfid);
            query.contractplace = FilterSpecial(query.contractplace);
            query.bcompany = FilterSpecial(query.bcompany);
            query.money = FilterSpecial(query.money);
            query.pkey = FilterSpecial(query.pkey);
            query.pvalue = FilterSpecial(query.pvalue);

            string where = " 1=1 ";
            if (string.IsNullOrEmpty(query.seq) == false)
            {
                where += " and seq ='"+ query.seq +"'";
            }
            if (string.IsNullOrEmpty(query.contractnum) == false)
            {
                where += string.Format ( " and contractnum like'%{0}%'", query.contractnum );
            }
            if (string.IsNullOrEmpty(query.projectnum) == false)
            {
                where += string.Format(" and projectnum like '%{0}%' ", query.projectnum);
            }
            if (string.IsNullOrEmpty(query.projectname) == false)
            {
                where += string.Format(" and projectname like '%{0}%'", query .projectname );
            }
            if (string.IsNullOrEmpty(query.contractrfid) == false)
            {
                where += string.Format(" and contractrfid like '%{0}%'", query.contractrfid);
            }
            if (string.IsNullOrEmpty(query.contractplace) == false)
            {
                where += string.Format(" and contractplace = '{0}'", query.contractplace );
            }
            if (string.IsNullOrEmpty(query.bcompany) == false)
            {
                where += string.Format(" and bcompany like '%{0}%'", query .bcompany );
            }
            if (string.IsNullOrEmpty(query.money) == false)
            {
                where += string.Format(" and money like '%{0}%'", query .money );
            }
            if( string.IsNullOrEmpty ( query.pkey) == false && string.IsNullOrEmpty( query.pvalue) ==false )
            {
                where += string.Format(" and {0} like '%{1}%'", query.pkey, query.pvalue);
            }

            return where;  
        }

        protected string FilterSpecial(string txt)
        {
            if( string.IsNullOrEmpty (txt )) return txt;
            return txt.Replace("'","");
        }

        public bool AddContract(Contract model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into t_contract(");
                strSql.Append("seq,contractnum,contractname,contractstate,unshelve,projectnum,projectname,projectmanager,tel,depart,linker,paymethod,money,contractplace,contractrfid,bcompany,signingdate,packageName,packageBudget,tendarNum,tendarCompany,tendarStartTime,phone,deliveryTime,inspection,progress,isPayAll,isArmoured,isRefund,remark,operatorId,operatorName,createtime,modifytime,type)");
                strSql.Append(" values (");
                strSql.Append("@seq,@contractnum,@contractname,@contractstate,@unshelve,@projectnum,@projectname,@projectmanager,@tel,@depart,@linker,@paymethod,@money,@contractplace,@contractrfid,@bcompany,@signingdate,@packageName,@packageBudget,@tendarNum,@tendarCompany,@tendarStartTime,@phone,@deliveryTime,@inspection,@progress,@isPayAll,@isArmoured,@isRefund,@remark,@operatorId,@operatorName,@createtime,@modifytime,@type)");
                MySqlParameter[] parameters = {
					new MySqlParameter("@seq", MySqlDbType.VarChar,45),
					new MySqlParameter("@contractnum", MySqlDbType.VarChar,45),
					new MySqlParameter("@contractname", MySqlDbType.VarChar,128),
					new MySqlParameter("@contractstate", MySqlDbType.VarChar,45),
					new MySqlParameter("@unshelve", MySqlDbType.VarChar,45),
					new MySqlParameter("@projectnum", MySqlDbType.VarChar,45),
					new MySqlParameter("@projectname", MySqlDbType.VarChar,100),
					new MySqlParameter("@projectmanager", MySqlDbType.VarChar,45),
					new MySqlParameter("@tel", MySqlDbType.VarChar,100),
					new MySqlParameter("@depart", MySqlDbType.VarChar,45),
					new MySqlParameter("@linker", MySqlDbType.VarChar,45),
					new MySqlParameter("@paymethod", MySqlDbType.VarChar,45),
					new MySqlParameter("@money", MySqlDbType.VarChar,45),
					new MySqlParameter("@contractplace", MySqlDbType.VarChar,45),
					new MySqlParameter("@contractrfid", MySqlDbType.VarChar,45),
					new MySqlParameter("@bcompany", MySqlDbType.VarChar,45),
					new MySqlParameter("@signingdate", MySqlDbType.VarChar,45),
					new MySqlParameter("@packageName", MySqlDbType.VarChar,100),
					new MySqlParameter("@packageBudget", MySqlDbType.VarChar,45),
					new MySqlParameter("@tendarNum", MySqlDbType.VarChar,45),
					new MySqlParameter("@tendarCompany", MySqlDbType.VarChar,100),
					new MySqlParameter("@tendarStartTime", MySqlDbType.VarChar,45),
					new MySqlParameter("@phone", MySqlDbType.VarChar,45),
					new MySqlParameter("@deliveryTime", MySqlDbType.VarChar,45),
					new MySqlParameter("@inspection", MySqlDbType.VarChar,128),
					new MySqlParameter("@progress", MySqlDbType.VarChar,45),
					new MySqlParameter("@isPayAll", MySqlDbType.VarChar,20),
					new MySqlParameter("@isArmoured", MySqlDbType.VarChar ,20),
					new MySqlParameter("@isRefund", MySqlDbType.VarChar ,20),
					new MySqlParameter("@remark", MySqlDbType.VarChar,256),
					new MySqlParameter("@operatorId", MySqlDbType.VarChar,45),
					new MySqlParameter("@operatorName", MySqlDbType.VarChar,45),
					new MySqlParameter("@createtime", MySqlDbType.Timestamp),
					new MySqlParameter("@modifytime", MySqlDbType.Timestamp),
                    new MySqlParameter("@type",MySqlDbType.Int16,2)
                };
                parameters[0].Value = model.seq;
                parameters[1].Value = model.contractnum;
                parameters[2].Value = model.contractname;
                parameters[3].Value = model.contractstate;
                parameters[4].Value = model.unshelve;
                parameters[5].Value = model.projectnum;
                parameters[6].Value = model.projectname;
                parameters[7].Value = model.projectmanager;
                parameters[8].Value = model.tel;
                parameters[9].Value = model.depart;
                parameters[10].Value = model.linker;
                parameters[11].Value = model.paymethod;
                parameters[12].Value = model.money;
                parameters[13].Value = model.contractplace;
                parameters[14].Value = model.contractrfid;
                parameters[15].Value = model.bcompany;
                parameters[16].Value = model.signingdate;
                parameters[17].Value = model.packageName;
                parameters[18].Value = model.packageBudget;
                parameters[19].Value = model.tendarNum;
                parameters[20].Value = model.tendarCompany;
                parameters[21].Value = model.tendarStartTime;
                parameters[22].Value = model.phone;
                parameters[23].Value = model.deliveryTime;
                parameters[24].Value = model.inspection;
                parameters[25].Value = model.progress;
                parameters[26].Value = model.isPayAll;
                parameters[27].Value = model.isArmoured;
                parameters[28].Value = model.isRefund;
                parameters[29].Value = model.remark;
                parameters[30].Value = model.operatorId;
                parameters[31].Value = model.operatorName;
                parameters[32].Value = model.createtime;
                parameters[33].Value = model.modifytime;
                parameters[34].Value = model.type;

                int result = MySqlHelper.ExecuteSql(strSql.ToString(), parameters);
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EditContractByProjectNumAndSeq(Contract model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update t_contract set ");
                strSql.Append("seq=@seq,");
                strSql.Append("contractnum=@contractnum,");
                strSql.Append("contractname=@contractname,");
                strSql.Append("contractstate=@contractstate,");
                strSql.Append("unshelve=@unshelve,");
                strSql.Append("projectnum=@projectnum,");
                strSql.Append("projectname=@projectname,");
                strSql.Append("projectmanager=@projectmanager,");
                strSql.Append("tel=@tel,");
                strSql.Append("depart=@depart,");
                strSql.Append("linker=@linker,");
                strSql.Append("paymethod=@paymethod,");
                strSql.Append("money=@money,");
                strSql.Append("contractplace=@contractplace,");
                strSql.Append("contractrfid=@contractrfid,");
                strSql.Append("bcompany=@bcompany,");
                strSql.Append("signingdate=@signingdate,");
                strSql.Append("packageName=@packageName,");
                strSql.Append("packageBudget=@packageBudget,");
                strSql.Append("tendarNum=@tendarNum,");
                strSql.Append("tendarCompany=@tendarCompany,");
                strSql.Append("tendarStartTime=@tendarStartTime,");
                strSql.Append("phone=@phone,");
                strSql.Append("deliveryTime=@deliveryTime,");
                strSql.Append("inspection=@inspection,");
                strSql.Append("progress=@progress,");
                strSql.Append("isPayAll=@isPayAll,");
                strSql.Append("isArmoured=@isArmoured,");
                strSql.Append("isRefund=@isRefund,");
                strSql.Append("remark=@remark,");
                strSql.Append("operatorId=@operatorId,");
                strSql.Append("operatorName=@operatorName,");
                //strSql.Append("createtime=@createtime,");
                strSql.Append("modifytime=@modifytime,");
                strSql.Append("type=@type");
                strSql.Append(" where projectnum=@projectnum2 and ");
                strSql.Append(" seq=@seq2");
                MySqlParameter[] parameters = {
					//new MySqlParameter("@contractid", MySqlDbType.Int32,11),
					new MySqlParameter("@seq", MySqlDbType.VarChar,45),
					new MySqlParameter("@contractnum", MySqlDbType.VarChar,45),
					new MySqlParameter("@contractname", MySqlDbType.VarChar,128),
					new MySqlParameter("@contractstate", MySqlDbType.VarChar,45),
					new MySqlParameter("@unshelve", MySqlDbType.VarChar,45),    
				    new MySqlParameter("@projectnum", MySqlDbType.VarChar,45),
					new MySqlParameter("@projectname", MySqlDbType.VarChar,100),
					new MySqlParameter("@projectmanager", MySqlDbType.VarChar,45),
					new MySqlParameter("@tel", MySqlDbType.VarChar,100),
					new MySqlParameter("@depart", MySqlDbType.VarChar,45),
					new MySqlParameter("@linker", MySqlDbType.VarChar,45),
					new MySqlParameter("@paymethod", MySqlDbType.VarChar,45),
					new MySqlParameter("@money", MySqlDbType.VarChar,45),
					new MySqlParameter("@contractplace", MySqlDbType.VarChar,45),
					new MySqlParameter("@contractrfid", MySqlDbType.VarChar,45),
					new MySqlParameter("@bcompany", MySqlDbType.VarChar,45),
					new MySqlParameter("@signingdate", MySqlDbType.VarChar,45),
					new MySqlParameter("@packageName", MySqlDbType.VarChar,100),
					new MySqlParameter("@packageBudget", MySqlDbType.VarChar,45),
					new MySqlParameter("@tendarNum", MySqlDbType.VarChar,45),
					new MySqlParameter("@tendarCompany", MySqlDbType.VarChar,100),
					new MySqlParameter("@tendarStartTime", MySqlDbType.VarChar,45),
					new MySqlParameter("@phone", MySqlDbType.VarChar,45),
					new MySqlParameter("@deliveryTime", MySqlDbType.VarChar,45),
					new MySqlParameter("@inspection", MySqlDbType.VarChar,128),
					new MySqlParameter("@progress", MySqlDbType.VarChar,45),
					new MySqlParameter("@isPayAll", MySqlDbType.VarChar,20),
					new MySqlParameter("@isArmoured", MySqlDbType.VarChar,20),
					new MySqlParameter("@isRefund", MySqlDbType.VarChar,20),
					new MySqlParameter("@remark", MySqlDbType.VarChar,256),
					new MySqlParameter("@operatorId", MySqlDbType.VarChar,45),
					new MySqlParameter("@operatorName", MySqlDbType.VarChar,45),
					//new MySqlParameter("@createtime", MySqlDbType.Timestamp),
					new MySqlParameter("@modifytime", MySqlDbType.Timestamp),
                    new MySqlParameter("@type",MySqlDbType.Int32),
                    new MySqlParameter("@projectnum2", MySqlDbType.VarChar,45),
					new MySqlParameter("@seq2", MySqlDbType.VarChar,100)
                                          };
                //parameters[0].Value = model.contractid;
                parameters[0].Value = model.seq;
                parameters[1].Value = model.contractnum;
                parameters[2].Value = model.contractname;
                parameters[3].Value = model.contractstate;
                parameters[4].Value = model.unshelve;
                parameters[5].Value = model.projectnum;
                parameters[6].Value = model.projectname;
                parameters[7].Value = model.projectmanager;
                parameters[8].Value = model.tel;
                parameters[9].Value = model.depart;
                parameters[10].Value = model.linker;
                parameters[11].Value = model.paymethod;
                parameters[12].Value = model.money;
                parameters[13].Value = model.contractplace;
                parameters[14].Value = model.contractrfid;
                parameters[15].Value = model.bcompany;
                parameters[16].Value = model.signingdate;
                parameters[17].Value = model.packageName;
                parameters[18].Value = model.packageBudget;
                parameters[19].Value = model.tendarNum;
                parameters[20].Value = model.tendarCompany;
                parameters[21].Value = model.tendarStartTime;
                parameters[22].Value = model.phone;
                parameters[23].Value = model.deliveryTime;
                parameters[24].Value = model.inspection;
                parameters[25].Value = model.progress;
                parameters[26].Value = model.isPayAll;
                parameters[27].Value = model.isArmoured;
                parameters[28].Value = model.isRefund;
                parameters[29].Value = model.remark;
                parameters[30].Value = model.operatorId;
                parameters[31].Value = model.operatorName;
                //parameters[33].Value = model.createtime;
                parameters[32].Value = model.modifytime;
                parameters[33].Value = model.type;
                parameters[34].Value = model.projectnum;
                parameters[35].Value = model.seq;


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
            catch (Exception ex)
            {
                return false;
            }
        }
                      
        [Obsolete]
        public bool EditContractByProjectNumAndProjectName(Contract model )
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update t_contract set ");
                strSql.Append("seq=@seq,");
                strSql.Append("contractnum=@contractnum,");
                strSql.Append("contractname=@contractname,");
                strSql.Append("contractstate=@contractstate,");
                strSql.Append("unshelve=@unshelve,");
                strSql.Append("projectnum=@projectnum,");
                strSql.Append("projectname=@projectname,");
                strSql.Append("projectmanager=@projectmanager,");
                strSql.Append("tel=@tel,");
                strSql.Append("depart=@depart,");
                strSql.Append("linker=@linker,");
                strSql.Append("paymethod=@paymethod,");
                strSql.Append("money=@money,");
                strSql.Append("contractplace=@contractplace,");
                strSql.Append("contractrfid=@contractrfid,");
                strSql.Append("bcompany=@bcompany,");
                strSql.Append("signingdate=@signingdate,");
                strSql.Append("packageName=@packageName,");
                strSql.Append("packageBudget=@packageBudget,");
                strSql.Append("tendarNum=@tendarNum,");
                strSql.Append("tendarCompany=@tendarCompany,");
                strSql.Append("tendarStartTime=@tendarStartTime,");
                strSql.Append("phone=@phone,");
                strSql.Append("deliveryTime=@deliveryTime,");
                strSql.Append("inspection=@inspection,");
                strSql.Append("progress=@progress,");
                strSql.Append("isPayAll=@isPayAll,");
                strSql.Append("isArmoured=@isArmoured,");
                strSql.Append("isRefund=@isRefund,");
                strSql.Append("remark=@remark,");
                strSql.Append("operatorId=@operatorId,");
                strSql.Append("operatorName=@operatorName,");
                //strSql.Append("createtime=@createtime,");
                strSql.Append("modifytime=@modifytime,");
                strSql.Append("type=@type");
                strSql.Append(" where projectnum=@projectnum2 and ");
                strSql.Append(" projectname=@projectname2");
                MySqlParameter[] parameters = {
					//new MySqlParameter("@contractid", MySqlDbType.Int32,11),
					new MySqlParameter("@seq", MySqlDbType.VarChar,45),
					new MySqlParameter("@contractnum", MySqlDbType.VarChar,45),
					new MySqlParameter("@contractname", MySqlDbType.VarChar,128),
					new MySqlParameter("@contractstate", MySqlDbType.VarChar,45),
					new MySqlParameter("@unshelve", MySqlDbType.VarChar,45),    
				    new MySqlParameter("@projectnum", MySqlDbType.VarChar,45),
					new MySqlParameter("@projectname", MySqlDbType.VarChar,100),
					new MySqlParameter("@projectmanager", MySqlDbType.VarChar,45),
					new MySqlParameter("@tel", MySqlDbType.VarChar,100),
					new MySqlParameter("@depart", MySqlDbType.VarChar,45),
					new MySqlParameter("@linker", MySqlDbType.VarChar,45),
					new MySqlParameter("@paymethod", MySqlDbType.VarChar,45),
					new MySqlParameter("@money", MySqlDbType.VarChar,45),
					new MySqlParameter("@contractplace", MySqlDbType.VarChar,45),
					new MySqlParameter("@contractrfid", MySqlDbType.VarChar,45),
					new MySqlParameter("@bcompany", MySqlDbType.VarChar,45),
					new MySqlParameter("@signingdate", MySqlDbType.VarChar,45),
					new MySqlParameter("@packageName", MySqlDbType.VarChar,100),
					new MySqlParameter("@packageBudget", MySqlDbType.VarChar,45),
					new MySqlParameter("@tendarNum", MySqlDbType.VarChar,45),
					new MySqlParameter("@tendarCompany", MySqlDbType.VarChar,100),
					new MySqlParameter("@tendarStartTime", MySqlDbType.VarChar,45),
					new MySqlParameter("@phone", MySqlDbType.VarChar,45),
					new MySqlParameter("@deliveryTime", MySqlDbType.VarChar,45),
					new MySqlParameter("@inspection", MySqlDbType.VarChar,128),
					new MySqlParameter("@progress", MySqlDbType.VarChar,45),
					new MySqlParameter("@isPayAll", MySqlDbType.VarChar,20),
					new MySqlParameter("@isArmoured", MySqlDbType.VarChar,20),
					new MySqlParameter("@isRefund", MySqlDbType.VarChar,20),
					new MySqlParameter("@remark", MySqlDbType.VarChar,256),
					new MySqlParameter("@operatorId", MySqlDbType.VarChar,45),
					new MySqlParameter("@operatorName", MySqlDbType.VarChar,45),
					//new MySqlParameter("@createtime", MySqlDbType.Timestamp),
					new MySqlParameter("@modifytime", MySqlDbType.Timestamp),
                    new MySqlParameter("@type",MySqlDbType.Int32),
                    new MySqlParameter("@projectnum2", MySqlDbType.VarChar,45),
					new MySqlParameter("@projectname2", MySqlDbType.VarChar,100)
                                          };
                //parameters[0].Value = model.contractid;
                parameters[0].Value = model.seq;
                parameters[1].Value = model.contractnum;
                parameters[2].Value = model.contractname;
                parameters[3].Value = model.contractstate;
                parameters[4].Value = model.unshelve;
                parameters[5].Value = model.projectnum;
                parameters[6].Value = model.projectname;
                parameters[7].Value = model.projectmanager;
                parameters[8].Value = model.tel;
                parameters[9].Value = model.depart;
                parameters[10].Value = model.linker;
                parameters[11].Value = model.paymethod;
                parameters[12].Value = model.money;
                parameters[13].Value = model.contractplace;
                parameters[14].Value = model.contractrfid;
                parameters[15].Value = model.bcompany;
                parameters[16].Value = model.signingdate;
                parameters[17].Value = model.packageName;
                parameters[18].Value = model.packageBudget;
                parameters[19].Value = model.tendarNum;
                parameters[20].Value = model.tendarCompany;
                parameters[21].Value = model.tendarStartTime;
                parameters[22].Value = model.phone;
                parameters[23].Value = model.deliveryTime;
                parameters[24].Value = model.inspection;
                parameters[25].Value = model.progress;
                parameters[26].Value = model.isPayAll;
                parameters[27].Value = model.isArmoured;
                parameters[28].Value = model.isRefund;
                parameters[29].Value = model.remark;
                parameters[30].Value = model.operatorId;
                parameters[31].Value = model.operatorName;
                //parameters[33].Value = model.createtime;
                parameters[32].Value = model.modifytime;
                parameters[33].Value = model.type;
                parameters[34].Value = model.projectnum;
                parameters[35].Value = model.projectname;

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
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EditContract(Contract model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_contract set ");
            strSql.Append("seq=@seq,");
            strSql.Append("contractnum=@contractnum,");
            strSql.Append("contractname=@contractname,");
            strSql.Append("contractstate=@contractstate,");
            strSql.Append("unshelve=@unshelve,");
            strSql.Append("projectnum=@projectnum,");
            strSql.Append("projectname=@projectname,");
            strSql.Append("projectmanager=@projectmanager,");
            strSql.Append("tel=@tel,");
            strSql.Append("depart=@depart,");
            strSql.Append("linker=@linker,");
            strSql.Append("paymethod=@paymethod,");
            strSql.Append("money=@money,");
            strSql.Append("contractplace=@contractplace,");
            strSql.Append("contractrfid=@contractrfid,");
            strSql.Append("bcompany=@bcompany,");
            strSql.Append("signingdate=@signingdate,");
            strSql.Append("packageName=@packageName,");
            strSql.Append("packageBudget=@packageBudget,");
            strSql.Append("tendarNum=@tendarNum,");
            strSql.Append("tendarCompany=@tendarCompany,");
            strSql.Append("tendarStartTime=@tendarStartTime,");
            strSql.Append("phone=@phone,");
            strSql.Append("deliveryTime=@deliveryTime,");
            strSql.Append("inspection=@inspection,");
            strSql.Append("progress=@progress,");
            strSql.Append("isPayAll=@isPayAll,");
            strSql.Append("isArmoured=@isArmoured,");
            strSql.Append("isRefund=@isRefund,");
            strSql.Append("remark=@remark,");
            strSql.Append("operatorId=@operatorId,");
            strSql.Append("operatorName=@operatorName,");
            //strSql.Append("createtime=@createtime,");
            strSql.Append("modifytime=@modifytime,");
            strSql.Append("type=@type");
            strSql.Append(" where contractid=@contractid");
            MySqlParameter[] parameters = {
					new MySqlParameter("@contractid", MySqlDbType.Int32,11),
					new MySqlParameter("@seq", MySqlDbType.VarChar,45),
					new MySqlParameter("@contractnum", MySqlDbType.VarChar,45),
					new MySqlParameter("@contractname", MySqlDbType.VarChar,128),
					new MySqlParameter("@contractstate", MySqlDbType.VarChar,45),
					new MySqlParameter("@unshelve", MySqlDbType.VarChar,45),
					new MySqlParameter("@projectnum", MySqlDbType.VarChar,45),
					new MySqlParameter("@projectname", MySqlDbType.VarChar,100),
					new MySqlParameter("@projectmanager", MySqlDbType.VarChar,45),
					new MySqlParameter("@tel", MySqlDbType.VarChar,100),
					new MySqlParameter("@depart", MySqlDbType.VarChar,45),
					new MySqlParameter("@linker", MySqlDbType.VarChar,45),
					new MySqlParameter("@paymethod", MySqlDbType.VarChar,45),
					new MySqlParameter("@money", MySqlDbType.VarChar,45),
					new MySqlParameter("@contractplace", MySqlDbType.VarChar,45),
					new MySqlParameter("@contractrfid", MySqlDbType.VarChar,45),
					new MySqlParameter("@bcompany", MySqlDbType.VarChar,45),
					new MySqlParameter("@signingdate", MySqlDbType.VarChar,45),
					new MySqlParameter("@packageName", MySqlDbType.VarChar,100),
					new MySqlParameter("@packageBudget", MySqlDbType.VarChar,45),
					new MySqlParameter("@tendarNum", MySqlDbType.VarChar,45),
					new MySqlParameter("@tendarCompany", MySqlDbType.VarChar,100),
					new MySqlParameter("@tendarStartTime", MySqlDbType.VarChar,45),
					new MySqlParameter("@phone", MySqlDbType.VarChar,45),
					new MySqlParameter("@deliveryTime", MySqlDbType.VarChar,45),
					new MySqlParameter("@inspection", MySqlDbType.VarChar,128),
					new MySqlParameter("@progress", MySqlDbType.VarChar,45),
					new MySqlParameter("@isPayAll", MySqlDbType.VarChar,20),
					new MySqlParameter("@isArmoured", MySqlDbType.VarChar,20),
					new MySqlParameter("@isRefund", MySqlDbType.VarChar,20),
					new MySqlParameter("@remark", MySqlDbType.VarChar,256),
					new MySqlParameter("@operatorId", MySqlDbType.VarChar,45),
					new MySqlParameter("@operatorName", MySqlDbType.VarChar,45),
					//new MySqlParameter("@createtime", MySqlDbType.Timestamp),
					new MySqlParameter("@modifytime", MySqlDbType.Timestamp),
                    new MySqlParameter("@type",MySqlDbType.Int32)
            };
            parameters[0].Value = model.contractid;
            parameters[1].Value = model.seq;
            parameters[2].Value = model.contractnum;
            parameters[3].Value = model.contractname;
            parameters[4].Value = model.contractstate;
            parameters[5].Value = model.unshelve;
            parameters[6].Value = model.projectnum;
            parameters[7].Value = model.projectname;
            parameters[8].Value = model.projectmanager;
            parameters[9].Value = model.tel;
            parameters[10].Value = model.depart;
            parameters[11].Value = model.linker;
            parameters[12].Value = model.paymethod;
            parameters[13].Value = model.money;
            parameters[14].Value = model.contractplace;
            parameters[15].Value = model.contractrfid;
            parameters[16].Value = model.bcompany;
            parameters[17].Value = model.signingdate;
            parameters[18].Value = model.packageName;
            parameters[19].Value = model.packageBudget;
            parameters[20].Value = model.tendarNum;
            parameters[21].Value = model.tendarCompany;
            parameters[22].Value = model.tendarStartTime;
            parameters[23].Value = model.phone;
            parameters[24].Value = model.deliveryTime;
            parameters[25].Value = model.inspection;
            parameters[26].Value = model.progress;
            parameters[27].Value = model.isPayAll;
            parameters[28].Value = model.isArmoured;
            parameters[29].Value = model.isRefund;
            parameters[30].Value = model.remark;
            parameters[31].Value = model.operatorId;
            parameters[32].Value = model.operatorName;
            //parameters[33].Value = model.createtime;
            parameters[33].Value = model.modifytime;
            parameters[34].Value = model.type;

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

        public Contract GetModel(int contractid)
        {
            string sql = "select * from t_contract where contractid="+ contractid ;
            DataSet ds = MySqlHelper.Query(sql);
            if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1) return null;
            DataRow row = ds.Tables[0].Rows[0];
            Contract model = DataRowToContract(row);
            return model;
        }

        public Beans.BatchImportResult BatchAddContracts(List<Contract> list , string operatorName )
        {
            Beans.BatchImportResult result = null;

            if (list == null || list.Count < 1) return result;

            if (CheckData(list , out result ) == false) return result;

            result = new BatchImportResult();
            result.TotalCount = list.Count;               

            int addcount  =0;  
            int updatecount = 0;
            int fail = 0;
            List<Beans.BatchImportResult.ExcelErrorLine> errLines = new List<BatchImportResult.ExcelErrorLine>();
            int idx = 1; //因为excel 从第二行开始数据行
            foreach (Contract c in list)
            {           
                idx ++;
                bool isExist = ExistContractBySeqAndprojectNum ( c.seq , c.projectnum );
                if (isExist)
                {
                    bool isSuccess = EditContractByProjectNumAndSeq(c); // EditContractByProjectNumAndProjectName(c);
                    updatecount += isSuccess ? 1 : 0;
                    fail += isSuccess ? 0 : 1;   
                    if( isSuccess == false ) errLines.Add( new BatchImportResult.ExcelErrorLine( idx.ToString() , "更新失败"));
                }
                else
                {
                    c.operatorName = operatorName;
                    bool isSuccess = AddContract(c);
                    addcount += isSuccess ? 1 : 0;
                    fail += isSuccess ? 0 : 1;
                    if (isSuccess == false ) errLines.Add( new BatchImportResult.ExcelErrorLine( idx .ToString () , "新增失败"));
                }
            }

            result.AddCount = addcount;
            result.UpdateCount = updatecount;
            result.FailureCount = fail;

            return result;
        }

        [Obsolete]
        public bool ExistContract(string  projectnum , string projectname)
        {
            string sql = string.Format(" select count(1) from t_contract where projectnum='{0}' and projectname='{1}'", projectnum , projectname);
            object obj = MySqlHelper.GetSingle(sql);
            if (obj == null) return false;
            int count = 0;
            int.TryParse(obj.ToString(), out count);
            return count > 0 ? true : false;
        }

        public bool ExistContractBySeqAndprojectNum(string seq, string projectnum)
        {
            string sql = string.Format(" select count(1) from t_contract where projectnum='{0}' and seq='{1}'", projectnum, seq );
            object obj = MySqlHelper.GetSingle(sql);
            if (obj == null) return false;
            int count = 0;
            int.TryParse(obj.ToString(), out count);
            return count > 0 ? true : false;
        }

        public bool ExistContractBySeqAndprojectNum(string seq, string projectnum, int contractid)
        {
            string sql = string.Format(" select count(1) from t_contract where contractid !={0} and projectnum='{1}' and seq='{2}'", contractid, projectnum, seq );
            object obj = MySqlHelper.GetSingle(sql);
            if (obj == null) return false;
            int count = 0;
            int.TryParse(obj.ToString(), out count);
            return count > 0 ? true : false; 
        }

        [Obsolete]
        public bool ExistContract(string projectnum, string projectname , int contractid)
        {
            string sql = string.Format(" select count(1) from t_contract where contractid !={0} and projectnum='{1}' and projectname='{2}'", contractid , projectnum, projectname);
            object obj = MySqlHelper.GetSingle(sql);
            if (obj == null) return false;
            int count = 0;
            int.TryParse(obj.ToString(), out count);
            return count > 0 ? true : false;
        }

        public bool ExistContract(string rfid)
        {
            string sql = string.Format(" select count(1) from t_contract where contractrfid='{0}'", rfid );
            object obj = MySqlHelper.GetSingle(sql);
            if (obj == null) return false;
            int count = 0;
            int.TryParse(obj.ToString(), out count);
            return count > 0 ? true : false;
        }

        public bool ExistContractNotSelf(string rfid, int contractid)
        {
            string sql = string.Format(" select count(1) from t_contract where contractid != {0} and contractrfid='{1}'", contractid , rfid);
            object obj = MySqlHelper.GetSingle(sql);
            if (obj == null) return false;
            int count = 0;
            int.TryParse(obj.ToString(), out count);
            return count > 0 ? true : false;
        }

        public bool DeleteContracts(List<int> contractIds)
        {
            string ids = string.Empty;
            foreach(int id in contractIds )
            {
                if (string.IsNullOrEmpty(ids) == false)
                {
                    ids += ",";
                }
                ids +=id;
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from t_contract where contractid in( "+ids+" )"  );      
            int count = MySqlHelper.ExecuteSql(strSql.ToString());
            return count > 0 ?true: false;
        }

        public bool DeleteAllContracts()
        {
            MySqlHelper.ExecuteSql("delete from t_contract");
            return true;
        }

        protected bool CheckData(List<Contract> list , out BatchImportResult result )
        {
            result = new BatchImportResult();
            if (list == null) return true;

            result.ErrorList = new List<BatchImportResult.ExcelErrorLine>();
            bool isok = true;
            for (int i = 0; i < list.Count; i++)
            {
                string msg = "";
                if (string.IsNullOrEmpty(list[i].contractnum))
                {
                    isok = false;
                    msg += "合同编号不能空";
                }

                if (string.IsNullOrEmpty(list[i].seq))
                {
                    isok = false;
                    if (msg != "")
                    {
                        msg += ",";
                    }
                    msg += "序号不能空";
                }

                if (string.IsNullOrEmpty(list[i].projectnum) || string.IsNullOrEmpty(list[i].projectname))
                {
                    isok = false;
                    if (msg != "")
                    {
                        msg += ",";
                    }
                    msg += "项目编号和项目名称不能空";
                }
                if (string.IsNullOrEmpty(list[i].packageBudget) == false)
                {
                    decimal fbys = 0;
                    if (decimal.TryParse(list[i].packageBudget, out fbys) == false)
                    {
                        isok = false;
                        if (msg != "")
                        {
                            msg += ",";
                        }
                        msg += "分包预算必须是数字";
                    }
                }
                else
                {
                    list[i].packageBudget = "0.00";
                }

                if (string.IsNullOrEmpty(list[i].money) == false)
                {
                    decimal zbje = 0;
                    if (decimal.TryParse(list[i].money, out zbje) == false)
                    {
                        isok = false;
                        if (msg != "")
                        {
                            msg += ",";
                        }
                        msg += "中标金额必须是数字";
                    }
                }
                else
                {
                    list[i].money = "0.00";
                }

                if ( msg !="")
                {
                    result.ErrorList.Add(new BatchImportResult.ExcelErrorLine((i + 2).ToString(), msg ));
                }
            }
            return isok;

        }

        //public Contract GetNextContract(int contractId, long modifyTime , string condition)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append(" select * from t_contract where contractid != "+ contractId + " and "  + condition );
        //    strSql.Append(" order by modifytime desc limit 1 ");

        //}
    }
}