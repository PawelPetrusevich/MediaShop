using MediaShop.Common.Dto;
using MediaShop.Common.Models.Notification;

namespace MediaShop.Common.Interfaces.Services
{
    public interface INotificationSubscribedUserService
    {
        NotificationSubscribedUserDto Subscribe(NotificationSubscribedUserDto subscribe);
        
        bool UserIsSubscribed(NotificationSubscribedUserDto subscribe);
    }
}