using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jagoron.Web.Models
{
    public class BranchEntry
    {
        public int intCompanayID { get; set; }
        public int intBranchID { get; set; }
        public string strBranchName{ get; set; }
        public string strBranch_Address { get; set; }  
        public string strMOBILE_NUMBER1 { get; set; }
        public string strMOBILE_NUMBER2 { get; set; }
        public string strMOBILE_NUMBER3 { get; set; }
        public string strEmail { get; set; }
        public string strfacebookID{ get; set; }
        public int  intBranchStatus { get; set; }
    }
}