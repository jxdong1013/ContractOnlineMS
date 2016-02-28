using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ContractMvcWeb.Models.Beans
{
    /// <summary>
    /// person:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class User
    {
        public User()
        { }

        #region Model
        private int _userid;
        private string _username;
        private string _password;
        private string _link;
        private string _sex;
        private string _comformPassword;
        private DateTime _createtime = DateTime.Now  ;
        private DateTime  _modifytime = DateTime .Now ;
        private int _enable = 1;
        private string _address;
        /// <summary>
        /// 1:管理账户，2：查询账户
        /// </summary>
        private string _usertype = "2";
        /// <summary>
        /// auto_increment
        /// </summary>
        public int userid
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name="用户名")]
        public string username
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name="密码")]
        public string password
        {
            set { _password = value; }
            get { return _password; }
        }
        [Display(Name ="确认密码")]
        public string comfirmPassword
        {
            get { return _comformPassword; }
            set { _comformPassword = value; }
        }
        [Display(Name="联系方式")]
        public string link
        {
            get { return _link; }
            set { _link = value; }
        }
        [Display(Name="性别")]
        public string sex
        {
            get { return _sex; }
            set { _sex = value; }
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
        /// 
        /// </summary>
        public DateTime  modifytime
        {
            set { _modifytime = value; }
            get { return _modifytime; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name="状态")]
        public int enable
        {
            set { _enable = value; }
            get { return _enable; }
        }
        [Display(Name="地址")]
        public string address
        {
            get { return _address; }
            set { _address = value; }
        }

        [Display(Name ="账户类型")]
        public string usertype
        {
            get { return _usertype; }
            set { _usertype = value; }
        }
        #endregion Model

     
    }


    [Serializable ]
    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "当前密码")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [Compare("NewPassword", ErrorMessage = "新密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }

    public enum EnableEnum 
    {
        DISABLE = 0,
        ENABLE = 1,
        ALL = 2
    }


    public enum UserTypeEnum
    {
        ADMIN=1,
        QUERY=2
    }
}