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
        bool Register(UserDto userModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        bool RemoveRole(int id, Role role);
    }
}
