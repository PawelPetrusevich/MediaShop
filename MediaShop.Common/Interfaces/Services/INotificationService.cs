using MediaShop.Common.Dto;
using MediaShop.Common.Models.Notification;

namespace MediaShop.Common.Interfaces.Services
{
    public interface INotificationService
    {
        void Notify(int userId, NotificationDto notification);
    }
}