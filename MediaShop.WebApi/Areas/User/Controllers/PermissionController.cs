using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MediaShop.WebApi.Filters;

namespace MediaShop.WebApi.Areas.User.Controllers
{
    using AutoMapper;

    using MediaShop.Common.Dto.User;
    using MediaShop.Common.Exceptions;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models.User;
    using MediaShop.WebApi.Properties;

    using Swashbuckle.Swagger.Annotations;

    [RoutePrefix("api/user")]
    [AccountExceptionFilter]
    public class PermissionController : ApiController
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService accountService)
        {
            _permissionService = accountService;
        }

        [HttpPost]
        [Route("{id}/permission/delete")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(AccountDbModel))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult RemovePermission([FromBody] PermissionDto data)
        {
            if (data == null || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmptyRegisterDate);
            }

            return Ok(_permissionService.RemovePermission(data));
        }

        [HttpPost]
        [Route("{id}/permission/add")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult SetPermission([FromBody] PermissionDto data)
        {
            return Ok(_permissionService.SetPermission(data));
        }
    }
}