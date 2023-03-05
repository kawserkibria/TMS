using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jagoron.Web.Models
{
    public class OrderSave
    {
        public void InitialList()
        {
            data = new List<OrderD>();
            Mesurmentlist = new List<OrderDD>();
        }

        public List<OrderD> data { get; set; }
        public List<OrderDD> Mesurmentlist { get; set; }

        
    }


    public class OrderM
    {
        public string strorderNo { get; set; }
        public string strCustomerId { get; set; }
        public string strdate { get; set; }
        public int intQty { get; set; }
        public int intRate { get; set; }
        public double dblVal { get; set; }
        public List<OrderD> detailsList { get; set; }

    }
 
    public class OrderD
    {
    
        public List<OrderDList> OrderDList { get; set; }
        public List<OrderDD> Mesurmentlist { get; set; }
        public List<OrderandFabrics> OrderandFabrics { get; set; }

    }
    public class OrderDList
    {
        public string strOrderNo { get; set; }
        public int intDressId { get; set; }
        public string strDressName { get; set; }
        public int intIDressQty { get; set; }
        public double dblDressRate { get; set; }
        public double dblTotalAmount { get; set; }


    }
    public class OrderDD
    {
        public string strorderNo { get; set; }
        public int intDressId { get; set; }
        public int inttxtid { get; set; }
        public double dbltxtVal { get; set; }
    }
    public class OrderandFabrics
    {
        public string strorderNo { get; set; }
        public string strCustomerId { get; set; }
        public string strFabricsName { get; set; }
        public string strdate { get; set; }
        public int intQty { get; set; }
        public int intRate { get; set; }
        public double  dblVal { get; set; }
        public double dbltotalVal { get; set; }
        public int intDressId { get; set; }

    }
}
