using MediaShop.Common.Dto;
using MediaShop.Common.Models.Notification;

namespace MediaShop.Common.Interfaces.Services
{
    public interface INotificationService
    {
        void Notify(long userId, NotificationDto notification);
    }
}