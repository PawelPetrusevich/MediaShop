using System;
using System.Collections.Generic;
using System.Net;
using System.Resources;
using System.Web.Http;
using AutoMapper;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Dto.User.Validators;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.User;
using MediaShop.WebApi.Properties;
using Swashbuckle.Swagger.Annotations;

namespace MediaShop.WebApi.Areas.User.Controllers
{
    [RoutePrefix("api/userSettings")]
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
        public IHttpActionResult ModifySettings([FromBody] SettingsDto userSettings)
        {
            if (userSettings == null || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmptyRegisterDate);
            }

            try
            {
                var settings = Mapper.Map<SettingsDomain>(userSettings);
                return Ok(_settingsService.Modify(settings));
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
