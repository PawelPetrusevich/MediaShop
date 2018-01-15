using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.Notification;
using MediaShop.DataAccess.Context;
using System.Data.Entity;

namespace MediaShop.DataAccess.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private bool _disposedValue;

        private DbContext _notificationContext;

        private DbSet<Notification> _notifications;

        public NotificationRepository(DbContext context)
        {
            _notificationContext = context;
            _notifications = _notificationContext.Set<Notification>();
        }

        ~NotificationRepository()
        {
            this.Dispose(false);
        }

        public Notification Add(Notification model)
        {
            if (ReferenceEquals(model, null))
            {
                throw new ArgumentNullException(); //TODO: write message
            }

            using (this._notificationContext)
            {
                var currentNotification = this._notifications.Add(model);
                this._notificationContext.SaveChanges();
                return currentNotification;
            }
        }

        public Notification Delete(Notification model)
        {
            if (ReferenceEquals(model, null))
            {
                throw new ArgumentNullException(); //TODO: write message
            }

            using (this._notificationContext)
            {
                var currentNotification = this._notifications.Remove(model);
                this._notificationContext.SaveChanges();
                return currentNotification;
            }
        }

        public Notification Delete(long id)
        {
            var model = this._notifications.Single(n => n.Id == id);
            return this.Delete(model);
        }

        public IEnumerable<Notification> Find(Expression<Func<Notification, bool>> filter)
        {
            using (this._notificationContext)
            {
                var searchResult = this._notifications.Where(filter).ToList();
                return searchResult;
            }
        }

        public Notification Get(long id)
        {
            if (id < 0)
            {
                throw new ArgumentException("message"); //TODO: write message
            }
            return this._notifications.Single(n => n.Id == id);
        }

        public Notification Update(Notification model)
        {
            if (ReferenceEquals(model, null))
            {
                throw new ArgumentNullException(); //TODO: write message
            }

            using (this._notificationContext)
            {
                var currentNotification = this._notifications.Single(n => n.Id == model.Id);
                currentNotification = model;
                this._notificationContext.SaveChanges();
                return currentNotification;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
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
    }
}
