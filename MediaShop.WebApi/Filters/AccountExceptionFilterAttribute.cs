using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Exceptions.CartExceptions;

namespace MediaShop.WebApi.Filters
{
    public class AccountExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public bool AllowMultiple { get; }

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            if (actionExecutedContext.Exception != null)
            {
                switch (actionExecutedContext.Exception)
                {
                    case ExistingLoginException login:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                            HttpStatusCode.BadRequest, login.Message);
                        break;
                    case NotFoundUserException notFoundUser:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                            HttpStatusCode.BadRequest, notFoundUser.Message);
                        break;
                    case CanNotSendEmailException canNotSendEmail:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                            HttpStatusCode.BadRequest, canNotSendEmail.Message);
                        break;
                    case AddAccountException addAccount:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                            HttpStatusCode.BadRequest, addAccount.Message);
                        break;
                    case ConfirmedUserException confirmedUser:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                            HttpStatusCode.BadRequest, confirmedUser.Message);
                        break;
                    case AddProfileException addProfile:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                            HttpStatusCode.BadRequest, addProfile.Message);
                        break;
                    case AddSettingsException addSettings:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                            HttpStatusCode.BadRequest, addSettings.Message);
                        break;
                    case UpdateAccountException updateAccount:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                            HttpStatusCode.BadRequest, updateAccount.Message);
                        break;
                    case IncorrectPasswordException incorrectPassword:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                            HttpStatusCode.BadRequest, incorrectPassword.Message);
                        break;
                    case AddStatisticException addStatistic:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                            HttpStatusCode.BadRequest, addStatistic.Message);
                        break;
                    default:
                       actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                           HttpStatusCode.InternalServerError, actionExecutedContext.Exception);
                       break;
                }               
            }

            return Task.FromResult<object>(null);
        }
    }
}