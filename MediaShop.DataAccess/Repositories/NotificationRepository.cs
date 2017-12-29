using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.Notification;
using MediaShop.DataAccess.Context;

namespace MediaShop.DataAccess.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        public Notification Add(Notification model)
        {
            if (ReferenceEquals(model, null))
            {
                throw new ArgumentNullException();
            }

            using (var notificationContext = new MediaContext())
            {
                var currentNotification = notificationContext.Notifications.Add(model);
                notificationContext.SaveChanges();
                return currentNotification;
            }
        }

        public Notification Delete(Notification model)
        {
            if (ReferenceEquals(model, null))
            {
                throw new ArgumentNullException();
            }

            using (var notificationContext = new MediaContext())
            {
                var currentNotification = notificationContext.Notifications.Remove(model);
                notificationContext.SaveChanges();
                return currentNotification;
            }
        }

        public Notification Delete(int id)
        {
            using (var notificationContext = new MediaContext())
            {
                var model = notificationContext.Notifications.Single(n => n.Id == id);
                return this.Delete(model);
            }
        }

        public IEnumerable<Notification> Find(Expression<Func<Notification, bool>> filter)
        {
            using (var notificationContext = new MediaContext())
            {
                var searchResult = notificationContext.Notifications.Where(filter).ToList();
                return searchResult;
            }
        }

        public Notification Get(int id)
        {
            using (var notificationContext = new MediaContext())
            {
                return notificationContext.Notifications.Single(n => n.Id == id);
            }
        }

        public Notification Update(Notification model)
        {
            if (ReferenceEquals(model, null))
            {
                throw new ArgumentNullException();
            }

            using (var notificationContext = new MediaContext())
            {
                var currentNotification = notificationContext.Notifications.Single(n => n.Id == model.Id);
                currentNotification = model;
                notificationContext.SaveChanges();
                return currentNotification;
            }
        }
    }
}
