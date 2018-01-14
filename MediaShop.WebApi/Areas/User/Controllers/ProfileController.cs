using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MediaShop.Common.Models.User;

namespace MediaShop.WebApi.Areas.User.Controllers
{
    using System;
    using System.Web.Services.Description;

    using AutoMapper;

    using MediaShop.Common.Dto.User;
    using MediaShop.Common.Exceptions;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.WebApi.Properties;

    using Newtonsoft.Json.Linq;

    using Swashbuckle.Swagger.Annotations;

    using Profile = MediaShop.Common.Models.User.Profile;

    [RoutePrefix("api/profile")]
    public class ProfileController : ApiController
    {
        private readonly IProfileService _profileService;
        
        public ProfileController(IProfileService profileService)
        {
            this._profileService = profileService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return this.Ok(new List<Profile>());
        }

        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult GetById(long id)
        {
            return this.Ok(new Profile());
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]Profile profile)
        {
            return this.Ok(new Profile());
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody]long? id)
        {
            return this.Ok();
        }

        [HttpPost]
        [Route("register")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Profile))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult Post([FromBody] ProfileDto data)
        {
            if (data == null || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmptyRegisterDate);
            }

            try
            {
                var dataDto = Mapper.Map<ProfileBl>(data);

                var result = _profileService.Create(dataDto);

                return Ok(Mapper.Map<ProfileDto>(result));
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
    }
}
