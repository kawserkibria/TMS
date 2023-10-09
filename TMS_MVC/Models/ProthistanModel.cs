using Jagoron.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class ProthistanModel : BaseModelcs
    {
        public int intSL { get; set; }
        public string strPROTHISTAN_ID { get; set; }
        public string strPROTHISTAN_NAME { get; set; }
        public string strPROTHISTAN_DAILOG { get; set; }
        public int intPROTHISTAN_JOURNY_START_YEAR { get; set; }
        public int intPROTHISTAN_STAF { get; set; }
        public string strPROTHISTAN_ADDRESS { get; set; }
        public string strPROTHISTAN_CITY { get; set; }
        public string strPROTHISTAN_POST_CODE { get; set; }
        public string strPROTHISTAN_MOBILE { get; set; }
        public string strPROTHISTAN_EMAIL { get; set; }
        public string strPROTHISTAN_FACEBOOK { get; set; }
        public string strPROTHISTAN_WEB_SIDE { get; set; }
        public string Data { get; set; }
        public byte[] strIamge { get; set; }
        public int inStatus { get; set; }
        public HttpPostedFileBase Picture { get; set; }
        public bool IsSelected { get; set; }


    }
}