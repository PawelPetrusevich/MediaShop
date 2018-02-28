using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MediaShop.WebApi.Areas.User.Controllers
{
    using AutoMapper;

    using MediaShop.Common.Dto.User;
    using MediaShop.Common.Exceptions;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models.User;
    using MediaShop.WebApi.Filters;
    using MediaShop.WebApi.Properties;

    using Swashbuckle.Swagger.Annotations;

    [RoutePrefix("api/user")]
    [MediaAuthorizationFilter(Permission = Permissions.See)]
    public class BannedController : ApiController
    {
        private readonly IBannedService _bannedService;

        public BannedController(IBannedService bannedService)
        {
            _bannedService = bannedService;
        }

        [HttpPost]
        [Route("banned/set")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(AccountDbModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult SetFlagIsBanned([FromBody] long id)
        {
            if (id < 1)
            {
                return BadRequest(Resources.EmtyData);
            }

            try
            {
                var a = _bannedService.SetFlagIsBanned(id, true);
                var b = Mapper.Map<UserDto>(a);
                return Ok(b);
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
        [Route("banned/remove")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(AccountDbModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult RemoveFlagIsBanned([FromBody] long id)
        {
            if (id < 1)
            {
                return BadRequest(Resources.EmtyData);
            }

            try
            {
                return Ok(Mapper.Map<UserDto>(_bannedService.SetFlagIsBanned(id, false)));
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
        [Route("bannedAsync/set")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(AccountDbModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult SetFlagIsBannedAsync([FromBody] long id)
        {
            if (id < 1)
            {
                return BadRequest(Resources.EmtyData);
            }

            try
            {
                return Ok(Mapper.Map<UserDto>(_bannedService.SetFlagIsBannedAsync(id, true)));
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
        [Route("bannedAsync/remove")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(AccountDbModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult RemoveFlagIsBannedAsync([FromBody] long id)
        {
            if (id < 1)
            {
                return BadRequest(Resources.EmtyData);
            }

            try
            {
                return Ok(Mapper.Map<UserDto>(_bannedService.SetFlagIsBannedAsync(id, false)));
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