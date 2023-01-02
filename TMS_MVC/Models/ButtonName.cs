using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jagoron.Web.Models
{
    public class ButtonName
    {
        public int intSERIAL_NO { get; set; }
        public int intBUTTON_ID { get; set; }
        public string strBUTTON_NAME { get; set; }
        public string strUser { get; set; }
        public string Data { get; set; }
        public byte[] strIamge { get; set; }
        public int inStatus { get; set; }
        public HttpPostedFileBase Picture { get; set; }
        public bool IsSelected { get; set; }
    }
}