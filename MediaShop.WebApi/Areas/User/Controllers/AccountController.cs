using System;
using System.Collections.Generic;
using System.Net;
using System.Resources;
using System.Web.Http;
using AutoMapper;
using MediaShop.Common.Dto;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Exceptions;
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
        }

        [HttpGet]
        [Route("confirm/{email}/{id}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult Confirm(string email, long id)
        {
            if (string.IsNullOrEmpty(email) || id == 0)
            {
                return BadRequest(Resources.EmtyData);
            }

            try
            {
                var account = _accountService.ConfirmRegistration(email, id);

                //TODO redirect to login
                return Ok(account);
            }
            catch (NotFoundUserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
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
                var authorizedUser = _accountService.Login(data);
                return Ok(authorizedUser);
            }
            catch (IncorrectPasswordException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("SetBanned")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(AccountDbModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult SetFlagIsBanned([FromBody] RegisterUserDto data)
        {
            if (data == null || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmptyRegisterDate);
            }

            try
            {
                var account = Mapper.Map<Account>(data);
                return Ok(_accountService.SetRemoveFlagIsBanned(account, true));
            }
            catch (ExistingLoginException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("RemoveBanned")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(AccountDbModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult RemoveFlagIsBanned([FromBody]RegisterUserDto data)
        {
            if (data == null || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmptyRegisterDate);
            }

            try
            {
                var account = Mapper.Map<Account>(data);
                return Ok(_accountService.SetRemoveFlagIsBanned(account, false));
            }
            catch (ExistingLoginException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("removeRole")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(AccountDbModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult RemoveRole([FromBody] RoleUserDto data)
        {
            if (data == null || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmptyRegisterDate);
            }

            try
            {
                var roleUserBl = Mapper.Map<RoleUserBl>(data);
                return Ok(_accountService.RemoveRole(roleUserBl));
            }
            catch (ExistingLoginException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("addRole")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(PermissionDomain))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult AddRole([FromBody] RoleUserDto data)
        {
            if (data == null || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmtyData);
            }

            try
            {
                var roleDomain = Mapper.Map<RoleUserBl>(data);
                return Ok(_accountService.AddRole(roleDomain));
            }
            catch (NotFoundUserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}