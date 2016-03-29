using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContractMvcWeb.Models.Beans
{
    /// <summary>
    /// 零星合同模型
    /// </summary>
    public class ContractLX : ContractCG
    {
        private string _summaryofbuy;

        public string summaryofbuy
        {
            get { return _summaryofbuy; }
            set { _summaryofbuy = value; }
        }

    }
}