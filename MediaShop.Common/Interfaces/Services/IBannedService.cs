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
        /// <param name="accountBLmodel"></param>
        /// <param name="flag"></param>
        /// <returns>account</returns>
        Account SetFlagIsBanned(Account accountBLmodel, bool flag);

        /// <summary>
        /// Set or remove flag banned async
        /// </summary>
        /// <param name="accountBLmodel"></param>
        /// <param name="flag"></param>
        /// <returns>account</returns>
        Task<Account> SetFlagIsBannedAsync(Account accountBLmodel, bool flag);
    }
}
