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
    public class NotificationSubscribedUserService : INotificationSubscribedUserService
    {
        private readonly INotificationSubscribedUserRepository _subscribedUserStore;

        public NotificationSubscribedUserService(INotificationSubscribedUserRepository subscribedUserStore)
        {
            _subscribedUserStore = subscribedUserStore;
        }

        public NotificationSubscribedUserDto Subscribe(NotificationSubscribedUserDto subscribeData)
        {
            var subscribe = GetSubscribe(subscribeData);

            if (subscribe == null)
            {
                subscribe = _subscribedUserStore.Add(Mapper.Map<NotificationSubscribedUser>(subscribeData));
            }
            return Mapper.Map<NotificationSubscribedUserDto>(subscribe);
        }

        private NotificationSubscribedUser GetSubscribe(NotificationSubscribedUserDto subscribeModel)
        {
            return _subscribedUserStore.Get(subscribeModel.UserId, subscribeModel.DeviceIdentifier);
        }

        public bool UserIsSubscribed(NotificationSubscribedUserDto subscribeModel)
        {
            var subscribe = GetSubscribe(subscribeModel);
            return subscribe != null;
        }
    }
}
