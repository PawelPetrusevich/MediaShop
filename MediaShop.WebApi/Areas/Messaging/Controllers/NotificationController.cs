using MediaShop.BusinessLogic.Services;
using MediaShop.Common.Dto;
using MediaShop.Common.Dto.Messaging;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Interfaces.Services;
using MediaShop.WebApi.Properties;
using System;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Collections.Generic;

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
        [Route("/GetNotificationsForUser")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NotFound, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(IEnumerable<NotificationDto>))]
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
        [Route("/CreateNotification")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(ArgumentException))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(NotSubscribedUserException))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(NotificationDto))]
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