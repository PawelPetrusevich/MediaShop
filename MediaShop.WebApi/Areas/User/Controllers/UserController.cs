using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Exceptions.User;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.User;
using MediaShop.WebApi.Areas.User.Controllers.Helpers;
using MediaShop.WebApi.Filters;
using MediaShop.WebApi.Properties;
using Swashbuckle.Swagger.Annotations;

namespace MediaShop.WebApi.Areas.User.Controllers
{
    [RoutePrefix("api/user")]
    [EnableCors("*", "*", "*")]
    [AccountExceptionFilter]
    [MediaAuthorizationFilter(Permission = Permissions.See)]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("delete")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult Delete()
        {
            var userClaims = HttpContext.Current.User.Identity as ClaimsIdentity
                             ?? throw new ArgumentNullException(nameof(HttpContext.Current.User.Identity));
            var idUser = Convert.ToInt64(userClaims.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId)?.Value);

            if (idUser < 1 || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmtyData);
            }

            try
            {
                var user = _userService.SoftDeleteByUser(idUser);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundUserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DeleteUserException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("deleteAsync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public async Task<IHttpActionResult> DeleteAsync()
        {
            var userClaims = HttpContext.Current.User.Identity as ClaimsIdentity
                             ?? throw new ArgumentNullException(nameof(HttpContext.Current.User.Identity));
            var idUser = Convert.ToInt64(userClaims.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId)?.Value);

            if (idUser < 1 || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmtyData);
            }

            try
            {
                var user = await _userService.SoftDeleteByUserAsync(idUser);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundUserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DeleteUserException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getUserInfo")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult GetUserInfo()
        {
            var userClaims = HttpContext.Current.User.Identity as ClaimsIdentity
                             ?? throw new ArgumentNullException(nameof(HttpContext.Current.User.Identity));
            var idUser = Convert.ToInt64(userClaims.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId)?.Value);

            if (idUser < 1 || !ModelState.IsValid)
            {
                return BadRequest(Resources.IncorrectData);
            }

            try
            {
                var user = _userService.GetUserInfo(idUser);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundUserException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getUserInfoAsync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public async Task<IHttpActionResult> GetUserInfoAsync()
        {
            var userClaims = HttpContext.Current.User.Identity as ClaimsIdentity
                             ?? throw new ArgumentNullException(nameof(HttpContext.Current.User.Identity));
            var idUser = Convert.ToInt64(userClaims.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId)?.Value);

            if (idUser < 1 || !ModelState.IsValid)
            {
                return BadRequest(Resources.IncorrectData);
            }

            try
            {
                var user = await _userService.GetUserInfoAsync(idUser);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundUserException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getUserInfoByIdAsync/{id}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public async Task<IHttpActionResult> GetUserInfoByIdAsync(long id)
        {
            if (id < 1 || !ModelState.IsValid)
            {
                return BadRequest(Resources.IncorrectData);
            }

            try
            {
                var user = await _userService.GetUserInfoAsync(id);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundUserException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getUserDtoAsync/{id}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public async Task<IHttpActionResult> GetUserDtoAsync(long id)
        {
            if (id < 1 || !ModelState.IsValid)
            {
                return BadRequest(Resources.IncorrectData);
            }

            try
            {
                var user = await _userService.GetUserDtoAsync(id);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundUserException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("modifySettings")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult ModifySettings([FromBody] SettingsDto settings)
        {
            if (settings == null || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmtyData);
            }

            var userClaims = HttpContext.Current.User.Identity as ClaimsIdentity
                             ?? throw new ArgumentNullException(nameof(HttpContext.Current.User.Identity));
            var idUser = Convert.ToInt64(userClaims.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId)?.Value);

            if (idUser < 1 || !ModelState.IsValid)
            {
                return BadRequest(Resources.IncorrectData);
            }

            try
            {
                settings.AccountId = idUser;
                var user = _userService.ModifySettings(settings);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundUserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AddAccountException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("modifySettingsAsync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public async Task<IHttpActionResult> ModifySettingsAsync([FromBody] SettingsDto settings)
        {
            if (settings == null)
            {
                return BadRequest(Resources.EmtyData);
            }

            var userClaims = HttpContext.Current.User.Identity as ClaimsIdentity
                             ?? throw new ArgumentNullException(nameof(HttpContext.Current.User.Identity));
            var idUser = Convert.ToInt64(userClaims.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId)?.Value);

            if (idUser < 1)
            {
                return BadRequest(Resources.IncorrectData);
            }

            try
            {
                settings.AccountId = idUser;
                var user = await _userService.ModifySettingsAsync(settings);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundUserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AddAccountException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("modifyProfile")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public IHttpActionResult ModifyProfile([FromBody] ProfileDto profile)
        {
            if (profile == null)
            {
                return BadRequest(Resources.EmtyData);
            }

            var userClaims = HttpContext.Current.User.Identity as ClaimsIdentity
                             ?? throw new ArgumentNullException(nameof(HttpContext.Current.User.Identity));
            var idUser = Convert.ToInt64(userClaims.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId)?.Value);

            if (idUser < 1 || !ModelState.IsValid)
            {
                return BadRequest(ShowErrorsHelper.ShowErrors(ModelState));
            }

            try
            {
                profile.AccountId = idUser;
                var user = _userService.ModifyProfile(profile);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundUserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AddAccountException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("modifyProfileAsync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public async Task<IHttpActionResult> ModifyProfilesAsync([FromBody] ProfileDto profile)
        {
            if (profile == null)
            {
                return BadRequest(Resources.EmtyData);
            }

            var userClaims = HttpContext.Current.User.Identity as ClaimsIdentity
                             ?? throw new ArgumentNullException(nameof(HttpContext.Current.User.Identity));
            var idUser = Convert.ToInt64(userClaims.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId)?.Value);

            if (idUser < 1 || !ModelState.IsValid)
            {
                return BadRequest(ShowErrorsHelper.ShowErrors(ModelState));
            }

            try
            {
                profile.AccountId = idUser;
                var user = await _userService.ModifyProfileAsync(profile);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundUserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AddAccountException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("deleteByIdAsync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Account))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        public async Task<IHttpActionResult> SetFlagIsDeletedAsync([FromBody] long id)
        {
            if (id < 1)
            {
                return BadRequest(Resources.EmtyData);
            }

            try
            {
                return Ok(await _userService.SoftDeleteAsync(id));
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