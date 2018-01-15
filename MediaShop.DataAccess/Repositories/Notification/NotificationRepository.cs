using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.Notification;
using System.Data.Entity;
using MediaShop.DataAccess.Repositories.Base;

namespace MediaShop.DataAccess.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(DbContext context)
            : base(context)
        {
        }
    }
}
