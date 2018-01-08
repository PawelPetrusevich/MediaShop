using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.Common.Models.Notification
{
    public class NotificationSubscribedUser : Entity
    {
        public long UserId { get; set; }

        public string DeviceIdentifier { get; set; }
    }
}
