using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Dto.User.Validators;

namespace MediaShop.Common.Interfaces.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Delete user by setting flag deleted in model account 
        /// </summary>
        Account SoftDeleteByUser(long idUser); 

        /// <summary>        
        /// Delete user by setting flag deleted in model account 
        /// </summary>
        Task<Account> SoftDeleteByUserAsync(long idUser);

        /// <summary>
        /// Delete user by setting flag deleted in model account 
        /// </summary>
        Task<UserDto> SoftDeleteAsync(long idUser);

        /// <summary>
        /// Get user information
        /// </summary>
        Account GetUserInfo(long idUser);

        /// <summary>
        /// Get user information
        /// </summary>
        Task<Account> GetUserInfoAsync(long idUser);

        /// <summary>
        /// Get user information
        /// </summary>
        Task<UserDto> GetUserDtoAsync(long idUser);

        /// <summary>
        /// Modiffy user settings
        /// </summary>
        /// <param name="settings">settings</param>
        /// <returns>account</returns>
        Settings ModifySettings(SettingsDto settings);

        /// <summary>
        /// Modiffy user settings
        /// </summary>
        /// <param name="settings">settings</param>
        /// <returns>account</returns>
        Task<Settings> ModifySettingsAsync(SettingsDto settings);

        /// <summary>
        /// Modiffy user profile
        /// </summary>
        /// <param name="profile">profile</param>
        /// <returns>account</returns>
        Profile ModifyProfile(ProfileDto profile);

        /// <summary>
        /// Modiffy user profile
        /// </summary>
        /// <param name="profile">profile</param>
        /// <returns>account</returns>
        Task<Profile> ModifyProfileAsync(ProfileDto profile);
    }
}
