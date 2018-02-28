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
using System.Threading.Tasks;
using System.Web.Http.Cors;
using MediaShop.WebApi.Filters;
using MediaShop.Common.Models.User;

namespace MediaShop.WebApi.Areas.Messaging.Controllers
{
    [RoutePrefix("api/messaging")]
    [NotificationExceptionFilter]
    [EnableCors("*", "*", "*")]
    [MediaAuthorizationFilter(Permission = Permissions.See)]
    public class NotificationController : ApiController
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        [Route("GetNotificationsForUser")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NotFound, "Notifications for this user not found", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "Notifications've got", typeof(IEnumerable<NotificationDto>))]
        public IHttpActionResult Get(long userId)
        {
            var result = _notificationService.GetByUserId(userId);
            if (ReferenceEquals(result, null))
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        [HttpGet]
        [Route("GetNotificationsForUserAsync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NotFound, "Notifications for this user not found", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "Notifications've got", typeof(IEnumerable<NotificationDto>))]
        public async Task<IHttpActionResult> GetAsync(long userId)
        {
            var result = await _notificationService.GetByUserIdAsync(userId);
            if (ReferenceEquals(result, null))
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        [HttpPost]
        [Route("CreateNotification")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Model is not valid", typeof(ArgumentException))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Not Subscribed user", typeof(NotSubscribedUserException))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Model is not valid", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "Notification created", typeof(NotificationDto))]
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

        [HttpPost]
        [Route("CreateNotificationAsync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Model is not valid", typeof(ArgumentException))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Not Subscribed user", typeof(NotSubscribedUserException))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Model is not valid", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "Notification created", typeof(NotificationDto))]
        public async Task<IHttpActionResult> CreateNotificationAsync([FromBody] NotificationDto notification)
        {
            if (ReferenceEquals(notification, null) || !ModelState.IsValid)
            {
                return this.BadRequest(Resources.NotValidNotification);
            }

            try
            {
                return this.Ok(await _notificationService.NotifyAsync(notification));
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