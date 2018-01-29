using MediaShop.BusinessLogic.Services;
using MediaShop.Common.Dto;
using MediaShop.Common.Dto.Messaging;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Interfaces.Services;
using MediaShop.WebApi.Properties;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

namespace MediaShop.WebApi.Areas.Messaging.Controllers
{
    [RoutePrefix("api/messaging")]
    public class NotificationController : ApiController, IExceptionFilter
    {
        private readonly INotificationService _notificationService;        

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public bool AllowMultiple => throw new NotImplementedException();

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
            catch (ArgumentException ex)
            {
                return this.InternalServerError(ex);
            }
            catch (NotSubscribedUserException ex)
            {
                return this.InternalServerError(ex);
            }
        }

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            actionExecutedContext.Response = actionExecutedContext.Request
                .CreateErrorResponse(HttpStatusCode.InternalServerError, actionExecutedContext.Exception.Message);
            return Task.FromResult<object>(null);
        }
    }
}
