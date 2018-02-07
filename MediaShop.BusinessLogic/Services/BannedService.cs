using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.BusinessLogic.Services
{
    using AutoMapper;

    using MediaShop.Common.Dto.User;
    using MediaShop.Common.Exceptions;
    using MediaShop.Common.Interfaces.Services;

    public class BannedService : IBannedService
    {
        private readonly IAccountFactoryRepository _factoryRepository;

        public Account SetFlagIsBanned(Account accountBLmodel, bool flag)
        {
            var existingAccount = this._factoryRepository.Accounts.GetByLogin(accountBLmodel.Login) ??
                                  throw new NotFoundUserException();

            existingAccount.IsBanned = flag;

            var updatingAccount = this._factoryRepository.Accounts.Update(existingAccount);
            var updatingAccountBl = Mapper.Map<Account>(updatingAccount);

            return updatingAccountBl;
        }

        /// <summary>
        /// Set or remove flag banned
        /// </summary>
        /// <param name="accountBLmodel"></param>
        /// <param name="flag"></param>
        /// <returns>account</returns>
        public async Task<Account> SetFlagIsBannedAsync(Account accountBLmodel, bool flag)
        {
            var existingAccount = this._factoryRepository.Accounts.GetByLogin(accountBLmodel.Login) ??
                                  throw new NotFoundUserException();

            existingAccount.IsBanned = flag;

            var updatingAccount = await this._factoryRepository.Accounts.UpdateAsync(existingAccount);
            var updatingAccountBl = Mapper.Map<Account>(updatingAccount);

            return updatingAccountBl;
        }
    }
}
