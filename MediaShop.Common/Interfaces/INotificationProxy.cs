using MediaShop.Common.Dto.Messaging;

namespace MediaShop.Common.Interfaces
{
    public interface INotificationProxy
    {
        void UpdateNotices(NotificationDto model);
    }
}