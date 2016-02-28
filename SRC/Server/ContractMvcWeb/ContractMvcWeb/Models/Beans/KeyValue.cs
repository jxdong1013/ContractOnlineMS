

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContractMvcWeb.Models.Beans
{
    public class KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public KeyValue(string k, string v)
        {
            Key = k;
            Value = v;
        }
    }
}