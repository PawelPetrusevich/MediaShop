using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Models.User;

namespace MediaShop.Common.Models.Notification
{
    public class NotificationSubscribedUser : Entity
    {
        public long UserId { get; set; }

        public string DeviceIdentifier { get; set; }

        public virtual AccountDbModel Account { get; set; }
    }
}
