﻿using System;
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
using MediaShop.WebApi.Properties;
using Swashbuckle.Swagger.Annotations;

namespace MediaShop.WebApi.Areas.User.Controllers
{
    [RoutePrefix("api/account")]
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

            try
            {
                var result = _accountService.Register(data);
                return Ok(result);
            }
            catch (ExistingLoginException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AddAccountException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (CanNotSendEmailException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("confirm/{email}/{id}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult Confirm(string email, long id)
        {
            if (string.IsNullOrWhiteSpace(email) || id < 1)
            {
                return BadRequest(Resources.EmtyData);
            }

            try
            {
                var account = _accountService.ConfirmRegistration(email, id);
                return Ok(account);
            }
            catch (NotFoundUserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ConfirmedUserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AddProfileException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AddSettingsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UpdateAccountException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
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

            try
            {
                var user = _accountService.Login(data);
                return Ok(user);
            }
            catch (NotFoundUserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (IncorrectPasswordException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AddStatisticException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UpdateAccountException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
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