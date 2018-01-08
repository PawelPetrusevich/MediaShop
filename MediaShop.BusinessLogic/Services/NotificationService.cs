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

namespace MediaShop.BusinessLogic.Services
{
    public class NotificationService : INotificationService
    {
        private static readonly HttpClient Client = new HttpClient();

        private readonly INotificationSubscribedUserRepository _subscribedUserStore;
        private readonly INotificationRepository _notifcationStore;

        public NotificationService(INotificationSubscribedUserRepository subscribedUserStore, INotificationRepository notifcationStore)
        {
            this._subscribedUserStore = subscribedUserStore;
            this._notifcationStore = notifcationStore;
        }

        public NotificationDto Notify(NotificationDto notification)
        {
            var tokens = this._subscribedUserStore.GetUserDeviceTokens(notification.ReceiverId);

            if (tokens.Count > 0)
            {
                //TODO: тут нужно сгенерить пост запрос вида: 
                /*
                    POST /fcm/send HTTP/1.1
                    Host: fcm.googleapis.com
                    Authorization: key=YOUR-SERVER-KEY
                    Content-Type: application/json

                    {
                      "notification": {
                        "title": "Ералаш",
                        "body": "Начало в 21:00",
                        "icon": "https://eralash.ru.rsz.io/sites/all/themes/eralash_v5/logo.png?width=40&height=40",
                        "click_action": "http://eralash.ru/"
                      },
                      "to": "YOUR-TOKEN-ID"
                    }
             */
                /*если полчателей несколько то вместо "to"   "registration_ids": [
                                                                                       "YOUR-TOKEN-ID-1",
                                                                                       "YOUR-TOKEN-ID-2"
                                                                                       "YOUR-TOKEN-ID-3"
                                                                                     ]
                */
            }

            return new NotificationDto();
        }
    }
}
