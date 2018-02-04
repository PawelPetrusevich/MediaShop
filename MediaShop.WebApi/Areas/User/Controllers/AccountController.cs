using System;
using System.Collections.Generic;
using System.Net;
using System.Resources;
using System.Web.Http;
using AutoMapper;
using MediaShop.Common.Dto;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Exceptions.CartExseptions;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.User;
using MediaShop.WebApi.Filters;
using MediaShop.WebApi.Properties;
using Swashbuckle.Swagger.Annotations;

namespace MediaShop.WebApi.Areas.User.Controllers
{
    [RoutePrefix("api/account")]
    [AccountExceptionFilter]
    public class AccountController : ApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("register")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult RegisterUser([FromBody] RegisterUserDto data)
        {
            if (data == null || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmptyRegisterDate);
            }

            var result = _accountService.Register(data);
            return Ok(result);
        }

        [HttpGet]
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

        [HttpPost]
        [Route("login")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult Login([FromBody] LoginDto data)
        {
            if (data == null || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmtyData);
            }

            var user = _accountService.Login(data);
            return Ok(user);
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

            return Ok(new Account());
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