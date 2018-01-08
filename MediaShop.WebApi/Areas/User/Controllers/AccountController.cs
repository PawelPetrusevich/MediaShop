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
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return this.Ok(new List<Account>());
        }

        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult GetById(long id)
        {
            return this.Ok(new Account());
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
                return Ok(_userService.Register(Mapper.Map<UserDto>(data)));
            }
            catch (ExistingLoginException ex)
            {
                return BadRequest(string.Empty);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPatch]
        [Route("patch")]
        public IHttpActionResult SmallUpdate([FromBody]UpdateModelDto model)
        {
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]Account account)
        {
            return this.Ok(new Account());
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody]long id)
        {
            return this.Ok();
        }
    }
}
