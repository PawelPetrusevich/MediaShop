using MediaShop.Common.Dto;
using MediaShop.Common.Models.User;

namespace MediaShop.Common.Interfaces.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Registration user
        /// </summary>
        /// <param name="userModel">Required data for registration</param>
        /// <returns>Is user registered or no,</returns>
        Account Register(UserDto userModel);
    }
}