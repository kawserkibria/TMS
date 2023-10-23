using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jagoron.Web.Models
{
    public class DressSetup
    {
        public int intDressid { get; set; }
        public string strDressName { get; set; }
        public int intDressFor { get; set; }
        public int intPOSITION { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string strImage { get; set; }
        public byte[] strIamge { get; set; }
        public HttpPostedFileBase Picture { get; set; }
    }
}