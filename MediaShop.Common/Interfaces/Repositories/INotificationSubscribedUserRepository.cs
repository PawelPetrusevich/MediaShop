using System.Collections.Generic;
using MediaShop.Common.Models.Notification;

namespace MediaShop.Common.Interfaces.Repositories
{
    public interface INotificationSubscribedUserRepository : IRespository<NotificationSubscribedUser>
    {
        List<string> GetUserTokenList(int userId);
    }
}