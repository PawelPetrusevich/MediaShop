using MediaShop.Common.Dto;

namespace MediaShop.Common.Interfaces.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Registration user
        /// </summary>
        /// <param name="userModel">Required data for registration</param>
        /// <returns>Is user registered or no</returns>
        bool Register(RegistrationUserModel userModel);
    }
}