using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jagoron.Web.Models
{
    public class BaseModelcs
    {
        public string strUserName { get; set; }
        public string strInsertBy { get; set; }
        public string strInsertDate { get; set; }
        public string strUpdateBy { get; set; }
        public string strUpdateDate { get; set; }
    }
}