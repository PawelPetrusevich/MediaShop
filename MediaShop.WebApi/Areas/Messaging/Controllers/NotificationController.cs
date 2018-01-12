using MediaShop.BusinessLogic.Services;
using MediaShop.Common.Dto;
using MediaShop.Common.Interfaces.Services;
using MediaShop.WebApi.Properties;
using System.Net;
using System.Web.Http;

namespace MediaShop.WebApi.Areas.Messaging.Controllers
{
    [RoutePrefix("api/messaging")]
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
        public IHttpActionResult CreateNotification([FromBody]NotificationDto notification)
        {
            if (ReferenceEquals(notification, null) || !ModelState.IsValid)
            {
                return this.BadRequest(Resources.NotValidNotification);
            }

            try
            {
                return this.Ok(_notificationService.Notify(notification));
            }
            catch (System.Exception)
            {
                return this.InternalServerError();
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateNotification([FromBody]NotificationDto notification)
        {
            return this.Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody]long id)
        {
            return this.Ok();
        }
    }
}
