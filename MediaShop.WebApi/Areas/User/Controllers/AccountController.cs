using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Resources;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using MediaShop.Common.Dto;
using MediaShop.Common.Dto.Messaging;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Exceptions.CartExceptions;
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
    [EnableCors("*", "*", "*")]
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

            try
            {
                var result = await _accountService.RegisterAsync(data);
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
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("confirm/{email}/{token}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult Confirm(string email, string token)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
            {
                return BadRequest(Resources.EmtyData);
            }      
            
            try
            {
                var account = _accountService.ConfirmRegistration(new AccountConfirmationDto() { Email = email, Token = token });
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
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("confirmAsync/{email}/{token}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public async Task<IHttpActionResult> ConfirmAsync(string email, string token)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
            {
                return BadRequest(Resources.EmtyData);
            }

            try
            {
                var account = await _accountService.ConfirmRegistrationAsync(new AccountConfirmationDto() { Email = email, Token = token });
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
        }

        [HttpPost]
        [Route("logout")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult LogOut()
        {
            var userClaims = HttpContext.Current.User.Identity as ClaimsIdentity;
            var id = Convert.ToInt64(userClaims.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId).Value);

            if (id < 1 || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmtyData);
            }

            var user = _accountService.Logout(id);
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("initRecoveryPassword")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public async Task<IHttpActionResult> InitRecoveryPasswordAync([FromBody] string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmtyData);
            }

            try
            {
                await _accountService.InitRecoveryPasswordAsync(email);
                return Ok(email);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
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

        [HttpPost]
        [AllowAnonymous]
        [Route("recoveryPassword")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public async Task<IHttpActionResult> RecoveryPasswordAsync([FromBody] ResetPasswordDto model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmtyData);
            }

            try
            {
                var account = await _accountService.RecoveryPasswordAsync(model);
                return Ok(account);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundUserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ConfirmationTokenException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getAllUsers")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(AccountDbModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult GetAllUsers()
        {
            try
            {
                return Ok(this._accountService.GetAllUsers());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}