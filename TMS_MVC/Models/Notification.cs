using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jagoron.Web.Models
{
    public class Notification
    {
        public long lngNotiFication { get; set; }
        public long lngNorthOrder{ get; set; }
        public long lngWestOdrer { get; set; }
        public List<NotificationMessgaeList> NotiMessage { get; set; }
    }

    public class NotificationMessgaeList
    {
       
        public string strNotiMessage { get; set; }
    }

}