using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Dto;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.Notification;
using System.Net.Http;
using AutoMapper;

namespace MediaShop.BusinessLogic.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationSubscribedUserRepository _subscribedUserStore;
        private readonly INotificationRepository _notifcationStore;

        public NotificationService(INotificationSubscribedUserRepository subscribedUserStore, INotificationRepository notifcationStore)
        {
            this._subscribedUserStore = subscribedUserStore;
            this._notifcationStore = notifcationStore;
        }

        public NotificationDto Notify(NotificationDto notification)
        {
            var tokens = _subscribedUserStore.GetUserDeviceTokens(notification.ReceiverId);
            var notify = _notifcationStore
                .Find(n => n.ReceiverId == notification.ReceiverId && n.Message == notification.Message && n.Title == notification.Title)
                .FirstOrDefault();

            if (tokens.Count > 0 && notify == null)
            {
                notify = _notifcationStore.Add(Mapper.Map<Notification>(notification));
            }

            return Mapper.Map<NotificationDto>(notify);
        }
    }
}
