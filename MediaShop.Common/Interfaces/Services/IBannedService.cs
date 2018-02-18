using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.Common.Interfaces.Services
{
    using MediaShop.Common.Dto.User;

    public interface IBannedService
    {
        /// <summary>
        /// Set or remove flag banned
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag"></param>
        /// <returns>account</returns>
        UserDto SetFlagIsBanned(long id, bool flag);

        /// <summary>
        /// Set or remove flag banned async
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag"></param>
        /// <returns>account</returns>
        Task<UserDto> SetFlagIsBannedAsync(long id, bool flag);
    }
}
