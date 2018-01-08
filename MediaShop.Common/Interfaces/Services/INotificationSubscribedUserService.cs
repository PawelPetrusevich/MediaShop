using MediaShop.Common.Dto;
using MediaShop.Common.Models.Notification;

namespace MediaShop.Common.Interfaces.Services
{
    public interface INotificationSubscribedUserService
    {
        NotificationSubscribedUserDto Subscribe(long userId, string deviceToken);

        bool UserIsSubscribed(long userId, string deviceToken);
    }
}