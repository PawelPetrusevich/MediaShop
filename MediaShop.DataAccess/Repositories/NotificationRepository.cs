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
    public class NotificationRepository : INotificationRepository, IDisposable
    {
        private bool _disposedValue = false;
        private MediaContext _notificationContext = new MediaContext();

        public Notification Add(Notification model)
        {
            if (ReferenceEquals(model, null))
            {
                throw new ArgumentNullException();
            }

            var currentNotification = this._notificationContext.Notifications.Add(model);
            this._notificationContext.SaveChanges();
            return currentNotification;
        }

        public Notification Delete(Notification model)
        {
            if (ReferenceEquals(model, null))
            {
                throw new ArgumentNullException();
            }

            var currentNotification = this._notificationContext.Notifications.Remove(model);
            this._notificationContext.SaveChanges();
            return currentNotification;
        }

        public Notification Delete(int id)
        {
            var model = this._notificationContext.Notifications.Single(n => n.Id == id);
            return this.Delete(model);
        }

        public IEnumerable<Notification> Find(Expression<Func<Notification, bool>> filter)
        {
            var searchResult = this._notificationContext.Notifications.Where(filter).ToList();
            return searchResult;
        }

        public Notification Get(int id)
        {
            return this._notificationContext.Notifications.Single(n => n.Id == id);
        }

        public Notification Update(Notification model)
        {
            if (ReferenceEquals(model, null))
            {
                throw new ArgumentNullException();
            }

            var currentNotification = this._notificationContext.Notifications.Single(n => n.Id == model.Id);
            currentNotification = model;
            this._notificationContext.SaveChanges();
            return currentNotification;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposedValue)
            {
                if (disposing)
                {
                    GC.SuppressFinalize(this);
                }

                this._notificationContext.Dispose();
                this._disposedValue = true;
            }
        }

        ~NotificationRepository()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }
    }
}
