using System;
using AutoMapper;
using MediaShop.Common.Dto;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.Notification;
using System.Collections.Generic;
using System.Linq;
using MediaShop.BusinessLogic.Properties;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Dto.Messaging;
using FluentValidation;
using MediaShop.Common.Dto.Messaging.Validators;
using System.Threading.Tasks;

namespace MediaShop.BusinessLogic.Services
{
    /// <summary>
    /// Service for send notifications to user
    /// </summary>
    public class NotificationService : INotificationService
    {
        private readonly INotificationSubscribedUserRepository _subscribedUserStore;
        private readonly INotificationRepository _notifcationStore;
        private readonly IValidator _validator;

        /// <summary>
        /// Initializes a new instance of the  <see cref="NotificationService"/> class.
        /// </summary>
        /// <param name="subscribedUserStore">Repository subscribed users</param>
        /// <param name="notifcationStore">Repository of notifications</param>
        public NotificationService(INotificationSubscribedUserRepository subscribedUserStore, INotificationRepository notifcationStore, IValidator<NotificationDto> validator)
        {
            this._subscribedUserStore = subscribedUserStore;
            this._notifcationStore = notifcationStore;
            this._validator = validator;
        }

        public NotificationDto Notify(NotificationDto notification)
        {
            var result = this._validator.Validate(notification);

            if (!result.IsValid)
            {
                throw new ArgumentException(result?.Errors.LastOrDefault().ErrorMessage);
            }

            if (string.IsNullOrWhiteSpace(notification.Title))
            {
                notification.Title = Resources.DefaultNotificationTitle;
            }

            var tokens = _subscribedUserStore.GetUserDeviceTokens(notification.ReceiverId);
            if (!tokens.Any())
            {
                throw new NotSubscribedUserException(Resources.NotSubscribedUserMessage);
            }

            var notify = _notifcationStore
                .Find(n => n.ReceiverId == notification.ReceiverId && n.Message == notification.Message &&
                           n.Title == notification.Title)
                .FirstOrDefault();

            notify = notify ?? _notifcationStore.Add(Mapper.Map<Notification>(notification));

            return Mapper.Map<NotificationDto>(notify);
        }

        public async Task<NotificationDto> NotifyAsync(NotificationDto notification)
        {
            var result = this._validator.Validate(notification);

            if (!result.IsValid)
            {
                throw new ArgumentException(result?.Errors.LastOrDefault().ErrorMessage);
            }

            if (string.IsNullOrWhiteSpace(notification.Title))
            {
                notification.Title = Resources.DefaultNotificationTitle;
            }

            var tokens = _subscribedUserStore.GetUserDeviceTokens(notification.ReceiverId);
            if (!tokens.Any())
            {
                throw new NotSubscribedUserException(Resources.NotSubscribedUserMessage);
            }

            var notify = _notifcationStore
                .Find(n => n.ReceiverId == notification.ReceiverId && n.Message == notification.Message &&
                           n.Title == notification.Title)
                .FirstOrDefault();

            notify = notify ?? await this._notifcationStore.AddAsync(Mapper.Map<Notification>(notification));

            return Mapper.Map<NotificationDto>(notify);
        }

        public NotificationDto AddToCartNotify(AddToCartNotifyDto data)
        {
            if (data == null)
            {
                throw new ArgumentException(Resources.NullOrEmptyValue, nameof(data));
            }

            if (string.IsNullOrWhiteSpace(data.ProductName))
            {
                throw new ArgumentException(Resources.NullOrEmptyValueString, nameof(data.ProductName));
            }

            if (data.ReceiverId < 1)
            {
                throw new ArgumentException(Resources.LessThanOrEqualToZeroValue, nameof(data.ReceiverId));
            }

            var tokens = _subscribedUserStore.GetUserDeviceTokens(data.ReceiverId);
            if (!tokens.Any())
            {
                throw new NotSubscribedUserException(Resources.NotSubscribedUserMessage);
            }

            var notification = Mapper.Map<NotificationDto>(data);
            notification.Title = Resources.DefaultNotificationTitle;

            var notify = _notifcationStore
                .Find(n => n.ReceiverId == data.ReceiverId && n.Message == notification.Message &&
                           n.Title == notification.Title)
                .FirstOrDefault();

            notify = notify ?? _notifcationStore.Add(Mapper.Map<Notification>(notification));

            return Mapper.Map<NotificationDto>(notify);
        }

        public async Task<NotificationDto> AddToCartNotifyAsync(AddToCartNotifyDto data)
        {
            if (data == null)
            {
                throw new ArgumentException(Resources.NullOrEmptyValue, nameof(data));
            }

            if (string.IsNullOrWhiteSpace(data.ProductName))
            {
                throw new ArgumentException(Resources.NullOrEmptyValueString, nameof(data.ProductName));
            }

            if (data.ReceiverId < 1)
            {
                throw new ArgumentException(Resources.LessThanOrEqualToZeroValue, nameof(data.ReceiverId));
            }

            var tokens = _subscribedUserStore.GetUserDeviceTokens(data.ReceiverId);
            if (!tokens.Any())
            {
                throw new NotSubscribedUserException(Resources.NotSubscribedUserMessage);
            }

            var notification = Mapper.Map<NotificationDto>(data);
            notification.Title = Resources.DefaultNotificationTitle;

            var notify = _notifcationStore
                .Find(n => n.ReceiverId == data.ReceiverId && n.Message == notification.Message &&
                           n.Title == notification.Title)
                .FirstOrDefault();

            notify = notify ?? await _notifcationStore.AddAsync(Mapper.Map<Notification>(notification));

            return Mapper.Map<NotificationDto>(notify);
        }

        public IEnumerable<NotificationDto> GetByUserId(long userId)
        {
            var result = _notifcationStore.Find(n => n.ReceiverId == userId);
            return Mapper.Map<List<NotificationDto>>(result);
        }

        public async Task<IEnumerable<NotificationDto>> GetByUserIdAsync(long userId)
        {
            var result = await _notifcationStore.FindAsync(n => n.ReceiverId == userId);
            return Mapper.Map<List<NotificationDto>>(result);
        }
    }
}