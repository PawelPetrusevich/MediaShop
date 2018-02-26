using System.Linq;
using System.Security.Claims;
using MediaShop.WebApi.Properties;
using Microsoft.AspNet.SignalR;

namespace MediaShop.WebApi.Provider
{
    public class SignalRUserIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            var userIdentity = request.User.Identity as ClaimsIdentity;
            if (userIdentity.HasClaim(c => c.Type == Resources.ClaimTypeId))
            {
                return userIdentity.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId).Value;
            }

            return string.Empty;
        }
    }
}