using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediaShop.Common.Dto;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.Notification;

namespace MediaShop.BusinessLogic.Services
{
    /// <summary>
    /// Service for subscribe user to receive notifications
    /// </summary>
    public class NotificationSubscribedUserService : INotificationSubscribedUserService
    {
        private readonly INotificationSubscribedUserRepository _repository;

        /// <summary>
        /// Initializes a new instance of the  <see cref="NotificationSubscribedUserService"/> class.
        /// </summary>
        /// <param name="subscribedUserStore">Subscribed users repository</param>
        public NotificationSubscribedUserService(INotificationSubscribedUserRepository subscribedUserStore)
        {
            _repository = subscribedUserStore;
        }

        public NotificationSubscribedUserDto Subscribe(NotificationSubscribedUserDto subscribeData)
        {
            var subscribe = GetSubscribe(subscribeData);
            subscribe = subscribe ?? _repository.Add(Mapper.Map<NotificationSubscribedUser>(subscribeData));
            return Mapper.Map<NotificationSubscribedUserDto>(subscribe);
        }

        public bool UserIsSubscribed(NotificationSubscribedUserDto subscribeModel)
        {
            var subscribe = GetSubscribe(subscribeModel);
            return subscribe != null;
        }

        /// <summary>
        /// Get user subscribe
        /// </summary>
        /// <param name="subscribeModel">subscribe model</param>
        /// <returns>User subscribtion</returns>
        private NotificationSubscribedUser GetSubscribe(NotificationSubscribedUserDto subscribeModel)
        {
            return _repository.Get(subscribeModel.UserId, subscribeModel.DeviceIdentifier);
        }
    }
}