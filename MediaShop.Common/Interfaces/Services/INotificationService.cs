using MediaShop.Common.Dto;
using MediaShop.Common.Models.Notification;
using System.Collections.Generic;

namespace MediaShop.Common.Interfaces.Services
{
    public interface INotificationService
    {
        NotificationDto Notify(NotificationDto notification);
        IEnumerable<NotificationDto> GetByUserId(long userId);
    }
}