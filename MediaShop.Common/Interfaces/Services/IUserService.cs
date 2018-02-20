using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Dto.User;

namespace MediaShop.Common.Interfaces.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Delete user by setting flag deleted in model account 
        /// </summary>
        Account SoftDelete(long idUser);

        /// <summary>
        /// Delete user by setting flag deleted in model account 
        /// </summary>
        Task<Account> SoftDeleteAsync(long idUser);

        /// <summary>
        /// Get user information
        /// </summary>
        Account GetUserInfo(long idUser);

        /// <summary>
        /// Get user information
        /// </summary>
        Task<Account> GetUserInfoAsync(long idUser);
    }
}
