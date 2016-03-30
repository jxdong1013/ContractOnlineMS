using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ContractMvcWeb.Models.Beans
{
    public class ContractCG
    {

        public ContractCG()
		{}
		#region Model
		private int _contractid;
		private string _seq;
		private string _type;
		private string _content;
		private decimal _price=0.00M;
		private int _count=0;
		private decimal _subtotal=0.00M;
		private decimal _total=0.00M;
		private string _contractnum;
		private string _department;
		private string _linker;
		private string _tel;
		private string _projectnum;
		private string _budgetamount;
		private string _fundsource;
		private string _super;
		private string _superlinker;
		private string _supertel;
		private string _settleamount;
		private string _freecontent;
		private string _freevalue;
		private string _validate;
		private string _remark;
		private string _payprogress;
		private string _chargedepartment;
        private string _place;
		private string _operatorid;
		private string _operatorname;
		private DateTime _createtime= DateTime.Now;
		private DateTime _modifytime= DateTime.Now;
        private string _buytime;

        /// <summary>
        /// 
        /// </summary>
        public string buytime
        {
            get { return _buytime; }
            set { _buytime = value; }
        }
		/// <summary>
		/// auto_increment
		/// </summary>
		public int contractid
		{
			set{ _contractid=value;}
			get{return _contractid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string seq
		{
			set{ _seq=value;}
			get{return _seq;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
        [Display(Name="单价")]
		public decimal price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
        [Display(Name="数量")]
		public int count
		{
			set{ _count=value;}
			get{return _count;}
		}
		/// <summary>
		/// 
		/// </summary>
        /// 
        [Display(Name = "小计")]
		public decimal subtotal
		{
			set{ _subtotal=value;}
			get{return _subtotal;}
		}
		/// <summary>
		/// 
		/// </summary>
        [Display(Name = "总价")]
		public decimal total
		{
			set{ _total=value;}
			get{return _total;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string contractnum
		{
			set{ _contractnum=value;}
			get{return _contractnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string department
		{
			set{ _department=value;}
			get{return _department;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string linker
		{
			set{ _linker=value;}
			get{return _linker;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string tel
		{
			set{ _tel=value;}
			get{return _tel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string projectnum
		{
			set{ _projectnum=value;}
			get{return _projectnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string budgetamount
		{
			set{ _budgetamount=value;}
			get{return _budgetamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fundsource
		{
			set{ _fundsource=value;}
			get{return _fundsource;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string super
		{
			set{ _super=value;}
			get{return _super;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string superlinker
		{
			set{ _superlinker=value;}
			get{return _superlinker;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string supertel
		{
			set{ _supertel=value;}
			get{return _supertel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string settleamount
		{
			set{ _settleamount=value;}
			get{return _settleamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string freecontent
		{
			set{ _freecontent=value;}
			get{return _freecontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string freevalue
		{
			set{ _freevalue=value;}
			get{return _freevalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string validate
		{
			set{ _validate=value;}
			get{return _validate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string payprogress
		{
			set{ _payprogress=value;}
			get{return _payprogress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string chargedepartment
		{
			set{ _chargedepartment=value;}
			get{return _chargedepartment;}
		}
        public string place
        {
            get { return _place; }
            set { _place = value; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string operatorId
		{
			set{ _operatorid=value;}
			get{return _operatorid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string operatorName
		{
			set{ _operatorname=value;}
			get{return _operatorname;}
		}
		/// <summary>
		/// on update CURRENT_TIMESTAMP
		/// </summary>
		public DateTime createtime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// on update CURRENT_TIMESTAMP
		/// </summary>
		public DateTime modifytime
		{
			set{ _modifytime=value;}
			get{return _modifytime;}
		}


		#endregion Model

        //新增的查询条件
        private string _pkey;
        private string _pvalue;
        //排序字段
        private string _sortkey;
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
        private int _pageidx = 1;

        public int pageidx
        {
            get { return _pageidx; }
            set { _pageidx = value; }
        }

    }
}