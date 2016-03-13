using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContractMvcWeb.Models.Beans
{
    /// <summary>
    /// 	支付进度：1-完成，2-部分支付，3-领走发票，4-发票已到，5-仅签合同，6-未签合同，7-流标。不填写有效值默认为6选项，下拉菜单选择									
    /// </summary>
    public enum PayProgressEnum
    {
        FINISH=1,
        PARTPAY=2,
        TICKET=3,
        TICKETAT=4,
        SIGN=5,
        UNSIGN=6,
        LOST=7
    }
}