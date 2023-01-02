using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class CompanyEntry
    {
        public int Id { get; set; }
        public int CompanayID { get; set; }
        public string CompanayName { get; set; }
        public string ContentType { get; set; }
        public string MobileNumber { get; set; }
        public string strUser { get; set; } 
        public string Data { get; set; }
        public byte[] strIamge { get; set; }
        public int inStatus { get; set; }
        public HttpPostedFileBase Picture { get; set; }
        public bool IsSelected { get; set; }
    }
}