using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Resources;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using MediaShop.Common.Dto;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Exceptions.CartExseptions;
using MediaShop.Common.Exceptions.User;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.User;
using MediaShop.WebApi.Filters;
using MediaShop.WebApi.Properties;
using Microsoft.AspNet.Identity;
using Swashbuckle.Swagger.Annotations;

namespace MediaShop.WebApi.Areas.User.Controllers
{
    using System.Text;

    [RoutePrefix("api/account")]
    [AccountExceptionFilter]
    [Authorize]
    public class AccountController : ApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult RegisterUser([FromBody] RegisterUserDto data)
        {
            if (data == null || !ModelState.IsValid)
            {
                var sb = new StringBuilder();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        sb.AppendFormat("{0} ! ", error.ErrorMessage);
                    }
                }
               
                return BadRequest(sb.ToString());
            }

            var result = _accountService.Register(data);
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("registerAsync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public async Task<IHttpActionResult> RegisterUserAsync([FromBody] RegisterUserDto data)
        {
            if (data == null || !ModelState.IsValid)
            {
                var sb = new StringBuilder();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        sb.AppendFormat("{0} ! ", error.ErrorMessage);
                    }
                }

                return BadRequest(sb.ToString());
            }

            var result = await _accountService.RegisterAsync(data);
            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("confirm/{email}/{id}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult Confirm(string email, long id)
        {
            if (string.IsNullOrWhiteSpace(email) || id <= 0)
            {
                return BadRequest(Resources.EmtyData);
            }

            var account = _accountService.ConfirmRegistration(email, id);
            return Ok(account);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("confirmAsync/{email}/{id}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public async Task<IHttpActionResult> ConfirmAsync(string email, long id)
        {
            if (string.IsNullOrWhiteSpace(email) || id <= 0)
            {
                return BadRequest(Resources.EmtyData);
            }

            var account = await _accountService.ConfirmRegistrationAsync(email, id);
            return Ok(account);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult Login([FromBody]LoginDto data)
        {
            if (data == null || string.IsNullOrWhiteSpace(data.Login) || string.IsNullOrWhiteSpace(data.Password) || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmtyData);
            }

            ClaimsIdentity result = HttpContext.Current.User as ClaimsIdentity;
            var emailAuthorized = result.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email))?.Value;
            if (string.IsNullOrWhiteSpace(emailAuthorized))
            {
                throw new AuthorizedDataException(Resources.EmptyAutorizedData);
            }

            var user = _accountService.ValidateUserByToken(data, emailAuthorized);

            user = _accountService.Login(data);            

            return Ok(result);
        }

        [HttpPost]
        [Route("loginAsync")]
        [AllowAnonymous]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public async Task<IHttpActionResult> LoginAsync([FromBody]LoginDto data)
        {
            if (data == null || string.IsNullOrWhiteSpace(data.Login) || string.IsNullOrWhiteSpace(data.Password) || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmtyData);
            }

            ClaimsIdentity result = HttpContext.Current.User as ClaimsIdentity;
            var emailAuthorized = result.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email))?.Value;           
            if (string.IsNullOrWhiteSpace(emailAuthorized))
            {
                throw new AuthorizedDataException(Resources.EmptyAutorizedData);
            }

            var user = await _accountService.ValidateUserByTokenAsync(data, emailAuthorized);

            user = await _accountService.LoginAsync(data);

            return Ok(result);
        }

        [HttpPost]
        [Route("logout")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult LogOut([FromBody] long id)
        {
            if (id < 1 || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmtyData);
            }

            var user = _accountService.Logout(id);
            return Ok(user);
        }

        [HttpPost]
        [Route("recoveryPassword")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult RecoveryPassword([FromBody] string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmtyData);
            }

            return Ok(new Account());
        }
    }
}