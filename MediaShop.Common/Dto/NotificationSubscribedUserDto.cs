using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.Common.Dto
{
    public class NotificationSubscribedUserDto
    {
        public ulong UserId { get; set; }

        public string DeviceIdentifier { get; set; }
    }
}
