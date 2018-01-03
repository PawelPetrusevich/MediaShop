using MediaShop.Common.Dto;
using MediaShop.Common.Models.Notification;

namespace MediaShop.Common.Interfaces.Services
{
    public interface INotificationSubscribedUserService
    {
        NotificationSubscribedUserDto Subscribe(ulong userId, string deviceToken);

        bool UserIsSubscribed(ulong userId, string deviceToken);
    }
}