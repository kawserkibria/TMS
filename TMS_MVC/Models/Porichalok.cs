﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jagoron.Web.Models
{
    public class Porichalok : BaseModelcs
    {
        public string  strPorichalokName { get; set; }
        public string strPosition { get; set; }
        public string strPorichalokFathersName { get; set; }
        public string strPorichalokMobile { get; set; }
        public string strPorichalokEmail { get; set; }
        public string strPorichalokAddress { get; set; }
        public byte[] strIamge { get; set; }
        public HttpPostedFileBase Picture { get; set; }
    }

  

}