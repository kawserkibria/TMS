using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jagoron.Web.Models
{
    public class OrderList
    {
        public int intSerialNo { get; set; }
        public string strOrderNo { get; set; }
        public int intCustomer { get; set; }
        public double dblOrderValue { get; set; }
        public string strOrderDate { get; set; }
        public string strInvNO { get; set; }
       
    }
}