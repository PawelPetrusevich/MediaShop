using System.Collections.Generic;
using MediaShop.Common.Dto;
using MediaShop.Common.Models.Notification;

namespace MediaShop.Common.Interfaces.Repositories
{
    public interface INotificationSubscribedUserRepository : IRespository<NotificationSubscribedUser>
    {
        List<string> GetUserDeviceTokens(long userId);

        bool IsExists(long userId, string deviceToken);

        NotificationSubscribedUser Get(long userId, string deviceId);
    }
}