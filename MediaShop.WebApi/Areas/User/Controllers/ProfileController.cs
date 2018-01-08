using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MediaShop.Common.Models.User;

namespace MediaShop.WebApi.Areas.User.Controllers
{
    using System;

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

        [HttpPost]
        public IHttpActionResult Post([FromBody] Profile profile)
        {
            return this.Content<Profile>(HttpStatusCode.Created, new Profile());
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
        public IHttpActionResult CreateProfile([FromBody] ProfileDto data,string login)
        {
            if (data == null || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmptyRegisterDate);
            }

            try
            {
                return Ok(_profileService.Create(data,login));
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

    }
}
