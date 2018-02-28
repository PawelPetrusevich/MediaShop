using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediaShop.BusinessLogic.Properties;
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
            if (subscribeData == null)
            {
                throw new ArgumentException(Resources.NullOrEmptyValue, nameof(subscribeData));
            }

            if (subscribeData.UserId < 1)
            {
                throw new ArgumentException(Resources.LessThanOrEqualToZeroValue, nameof(subscribeData.UserId));
            }

            if (string.IsNullOrWhiteSpace(subscribeData.DeviceIdentifier))
            {
                throw new ArgumentException(Resources.NullOrEmptyValueString, nameof(subscribeData.DeviceIdentifier));
            }

            var subscribe = GetSubscribe(subscribeData);
            subscribe = subscribe ?? _repository.Add(Mapper.Map<NotificationSubscribedUser>(subscribeData));
            return Mapper.Map<NotificationSubscribedUserDto>(subscribe);
        }

        public async Task<NotificationSubscribedUserDto> SubscribeAsync(NotificationSubscribedUserDto subscribeData)
        {
            if (subscribeData == null)
            {
                throw new ArgumentException(Resources.NullOrEmptyValue, nameof(subscribeData));
            }

            if (subscribeData.UserId < 1)
            {
                throw new ArgumentException(Resources.LessThanOrEqualToZeroValue, nameof(subscribeData.UserId));
            }

            if (string.IsNullOrWhiteSpace(subscribeData.DeviceIdentifier))
            {
                throw new ArgumentException(Resources.NullOrEmptyValueString, nameof(subscribeData.DeviceIdentifier));
            }

            var subscribe = await GetSubscribeAsync(subscribeData);
            subscribe = subscribe ?? await _repository.AddAsync(Mapper.Map<NotificationSubscribedUser>(subscribeData));
            return Mapper.Map<NotificationSubscribedUserDto>(subscribe);
        }

        public bool UserIsSubscribed(NotificationSubscribedUserDto subscribeModel)
        {
            if (subscribeModel == null)
            {
                throw new ArgumentException(Resources.NullOrEmptyValue, nameof(subscribeModel));
            }

            if (subscribeModel.UserId < 1)
            {
                throw new ArgumentException(Resources.LessThanOrEqualToZeroValue, nameof(subscribeModel.UserId));
            }

            if (string.IsNullOrWhiteSpace(subscribeModel.DeviceIdentifier))
            {
                throw new ArgumentException(Resources.NullOrEmptyValueString, nameof(subscribeModel.DeviceIdentifier));
            }

            var subscribe = GetSubscribe(subscribeModel);
            return subscribe != null;
        }

        public async Task<bool> UserIsSubscribedAsync(NotificationSubscribedUserDto subscribeModel)
        {
            if (subscribeModel == null)
            {
                throw new ArgumentException(Resources.NullOrEmptyValue, nameof(subscribeModel));
            }

            if (subscribeModel.UserId < 1)
            {
                throw new ArgumentException(Resources.LessThanOrEqualToZeroValue, nameof(subscribeModel.UserId));
            }

            if (string.IsNullOrWhiteSpace(subscribeModel.DeviceIdentifier))
            {
                throw new ArgumentException(Resources.NullOrEmptyValueString, nameof(subscribeModel.DeviceIdentifier));
            }

            var subscribe = await GetSubscribeAsync(subscribeModel);
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

        /// <summary>
        /// Get user subscribe
        /// </summary>
        /// <param name="subscribeModel">subscribe model</param>
        /// <returns>User subscribtion</returns>
        private async Task<NotificationSubscribedUser> GetSubscribeAsync(NotificationSubscribedUserDto subscribeModel)
        {
            return await _repository.GetAsync(subscribeModel.UserId, subscribeModel.DeviceIdentifier);
        }
    }
}