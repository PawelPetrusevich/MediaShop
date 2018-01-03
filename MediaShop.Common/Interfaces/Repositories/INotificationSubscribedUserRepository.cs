using System.Collections.Generic;
using MediaShop.Common.Dto;
using MediaShop.Common.Models.Notification;

namespace MediaShop.Common.Interfaces.Repositories
{
    public interface INotificationSubscribedUserRepository : IRespository<NotificationSubscribedUser>
    {
        List<string> GetUserDeviceTokens(int userId);

        bool IsExists(ulong userId, string deviceToken);
    }
}