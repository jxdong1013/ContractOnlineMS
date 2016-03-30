using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ContractMvcWeb.Models.Beans
{
    [Serializable]
    public class Contract
    {
        public Contract()
        { }
        #region Model
        private int _contractid;
        private string _seq;
        private string _contractnum;
        private string _contractname;
        private string _contractstate;
        private string _unshelve;
        private string _projectnum;
        private string _projectname;
        private string _projectmanager;
        private string _tel;
        private string _depart;
        private string _linker;
        private string _paymethod;
        private string _money="0.00";
        private string _contractplace;
        private string _contractrfid;
        private string _bcompany;
        private string _signingdate;
        private string _packagename;
        private string _packagebudget="0.00";
        private string _tendarnum;
        private string _tendarcompany;
        private string _tendarstarttime;
        private string _phone;
        private string _deliverytime;
        private string _inspection;
        private string _progress;
        private string  _ispayall ;
        private string  _isarmoured;
        private string  _isrefund;
        private string _remark;
        private string _operatorid;
        private string _operatorname;
        private DateTime  _createtime = DateTime.Now;
        //private long _createtimelong = DateTime.Now.Ticks;
        private DateTime  _modifytime = DateTime.Now;
        //private long _modifytimelong = DateTime.Now.Ticks;
        /// <summary>
        /// 合同类型1：中标合同，2：协议合同3：零星合同
        /// </summary>
        //private int _type = 1;
        
        //新增的查询条件
        private string _pkey;
        private string _pvalue;
        //排序字段
        private string _sortkey;
        private int _pageidx;
        
        public string sortkey
        {
            get { return _sortkey; }
            set { _sortkey = value; }
        }
        //排序方式
        private string _sorttype;
        public string sorttype
        {
            get { return _sorttype; }
            set { _sorttype = value; }
        }
        public string pkey
        {
            get { return _pkey; }
            set { _pkey = value; }
        }
        public string pvalue
        {
            get { return _pvalue; }
            set { _pvalue = value; }
        }

        public int pageidx
        {
            get { return _pageidx; }
            set { _pageidx = value; }
        }

        //public int type
        //{
        //    get
        //    {
        //        return _type;
        //    }
        //    set { _type = value; }
        //}

        /// <summary>
        /// auto_increment
        /// </summary>
        public int contractid
        {
            set { _contractid = value; }
            get { return _contractid; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Display(Name="序号")]
        public string seq
        {
            set { _seq = value; }
            get { return _seq; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Display(Name="合同编号")]
        public string contractnum
        {
            set { _contractnum = value; }
            get { return _contractnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string contractname
        {
            set { _contractname = value; }
            get { return _contractname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string contractstate
        {
            set { _contractstate = value; }
            get { return _contractstate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string unshelve
        {
            set { _unshelve = value; }
            get { return _unshelve; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Display(Name="项目编号")]
        public string projectnum
        {
            set { _projectnum = value; }
            get { return _projectnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Display(Name="项目名称")]
        public string projectname
        {
            set { _projectname = value; }
            get { return _projectname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string projectmanager
        {
            set { _projectmanager = value; }
            get { return _projectmanager; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string depart
        {
            set { _depart = value; }
            get { return _depart; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string linker
        {
            set { _linker = value; }
            get { return _linker; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string paymethod
        {
            set { _paymethod = value; }
            get { return _paymethod; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string money
        {
            set { _money = value; }
            get { return _money; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string contractplace
        {
            set { _contractplace = value; }
            get { return _contractplace; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string contractrfid
        {
            set { _contractrfid = value; }
            get { return _contractrfid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bcompany
        {
            set { _bcompany = value; }
            get { return _bcompany; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string signingdate
        {
            set { _signingdate = value; }
            get { return _signingdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string packageName
        {
            set { _packagename = value; }
            get { return _packagename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string packageBudget
        {
            set { _packagebudget = value; }
            get { return _packagebudget; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string tendarNum
        {
            set { _tendarnum = value; }
            get { return _tendarnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string tendarCompany
        {
            set { _tendarcompany = value; }
            get { return _tendarcompany; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string tendarStartTime
        {
            set { _tendarstarttime = value; }
            get { return _tendarstarttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string deliveryTime
        {
            set { _deliverytime = value; }
            get { return _deliverytime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string inspection
        {
            set { _inspection = value; }
            get { return _inspection; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string progress
        {
            set { _progress = value; }
            get { return _progress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string isPayAll
        {
            set { _ispayall = value; }
            get { return _ispayall; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string  isArmoured
        {
            set { _isarmoured = value; }
            get { return _isarmoured; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string  isRefund
        {
            set { _isrefund = value; }
            get { return _isrefund; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string operatorId
        {
            set { _operatorid = value; }
            get { return _operatorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string operatorName
        {
            set { _operatorname = value; }
            get { return _operatorname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime  createtime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 转换成 java 的 日期
        /// </summary>
        public long createtimelong
        {
            get {
                return  _createtime.ToUniversalTime().Subtract( new DateTime(1970,1,1)).Ticks; 
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime modifytime
        {
            set { _modifytime = value; }
            get { return _modifytime; }
        }
        /// <summary>
        /// 转换成 java的日期
        /// </summary>
        public long modifytimelong
        {
            get { return  _modifytime.ToUniversalTime().Subtract( new DateTime( 1970,1,1)).Ticks; }
        }
        #endregion Model
    }
}