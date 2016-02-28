using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ContractMvcWeb.Models
{
    public class user
    {
        public int userid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string remark { get; set; }
    }

    public class role
    {
        public int roleid { get; set; }
        public string rolename { get; set; }
    }
    public class userrole
    {
        public int userid { get; set; }
        public int roleid { get; set; }
    }

}