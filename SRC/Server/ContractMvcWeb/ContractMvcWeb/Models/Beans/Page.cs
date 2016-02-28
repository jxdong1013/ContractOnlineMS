using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContractMvcWeb.Models.Beans
{
    public class Page<T>
    {
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int PageIdx { get; set; }
        public int PageSize { get; set; }

        public List<T> Data { get; set; }
    }
}