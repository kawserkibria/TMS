using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jagoron.Web.Models
{
    public class DressMaster
    {

        public void InitialList()
        {
            data = new List<DressMesrmentList>();
            data2 = new List<DressStyleList>();
        }
        public List<DressMesrmentList> data { get; set; }
        public List<DressStyleList> data2 { get; set; }
    }
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
        public List<DressMesrmentList> DressSubList { get; set; }
        public List<DressStyleList> DressStyleList { get; set; }  
    }

    public class DressMesrmentList
    {
        public int intDressid { get; set; }
        public string strDressName { get; set; }
        public string strDressHead { get; set; }
        public string strMesurmentNameSubtype { get; set; }
    }
    public class DressStyleList
    {
        public string Data { get; set; }
        public byte[] strIamge { get; set; }
        public HttpPostedFileBase Picture { get; set; }
        public string strStyleKey { get; set; }
        public int intDressid { get; set; }
        public string strDressName { get; set; }
        public int intStyleid { get; set; }
        public string strStyleName { get; set; }
        public int intStyleDetailsid { get; set; }
        public string strStyleDetailsName { get; set; }
    }
}