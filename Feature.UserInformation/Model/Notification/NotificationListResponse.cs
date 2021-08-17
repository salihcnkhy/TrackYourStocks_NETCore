using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.UserInformation.Model
{
    public class NotificationListResponse : Response
    {
        public List<NotificationModel> NotificationList { get; set; }
    }
}
