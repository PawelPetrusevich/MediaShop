﻿using MediaShop.BusinessLogic.Services;
using MediaShop.Common.Dto;
using MediaShop.Common.Dto.Messaging;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Interfaces.Services;
using MediaShop.WebApi.Properties;
using System;
using System.Web.Http;

namespace MediaShop.WebApi.Areas.Messaging.Controllers
{
    [RoutePrefix("api/messaging")]
    [NotificationExceptionFilter]
    public class NotificationController : ApiController
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public IHttpActionResult Get(long userId)
        {
            var result = _notificationService.GetByUserId(userId);
            if (ReferenceEquals(result, null))
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        [HttpPost]
        public IHttpActionResult CreateNotification([FromBody] NotificationDto notification)
        {
            if (ReferenceEquals(notification, null) || !ModelState.IsValid)
            {
                return this.BadRequest(Resources.NotValidNotification);
            }

            try
            {
                return this.Ok(_notificationService.Notify(notification));
            }
            catch (ArgumentException ex)
            {
                return this.InternalServerError(ex);
            }
            catch (NotSubscribedUserException ex)
            {
                return this.InternalServerError(ex);
            }
        }
    }
}