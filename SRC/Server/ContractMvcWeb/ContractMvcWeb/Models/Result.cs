using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContractMvcWeb.Models
{
    public class Result
    {
        public int Code { get;set;}
        public string Message { get; set; }
        public Object Data {get;set;}
        public Result(int c , string m , object d)
        {
            Code = c; Message = m; Data = d;
        }
    }
    public enum ResultCodeEnum
    {
        Error=0,
        Success=1
    }
}