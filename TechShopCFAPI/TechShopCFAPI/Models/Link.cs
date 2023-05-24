using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechShopCFAPI.Models
{
    public class Link
    {
        public string Url { get; set; }
        public string Method { get; set; }
        public string Relation { get; set; }
    }
}