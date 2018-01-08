using System;
using System.Collections.Generic;
using System.Net;
using System.Resources;
using System.Web.Http;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.User;
using MediaShop.WebApi.Properties;
using Swashbuckle.Swagger.Annotations;

namespace MediaShop.WebApi.Areas.User.Controllers
{
    [RoutePrefix("api/settings")]
    public class SettingsController : ApiController
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpPost]
        [Route("modify")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Settings))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult ModifySettings([FromBody] SettingsDto settings)
        {
            if (settings == null || ModelState.IsValid)
            {
                return BadRequest(Resources.EmptyRegisterDate);
            }

            try
            {
                return Ok(_settingsService.Modify(settings));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
