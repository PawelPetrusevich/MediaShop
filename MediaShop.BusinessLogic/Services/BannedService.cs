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
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Interfaces.Services;

    public class BannedService : IBannedService
    {
        private readonly IAccountRepository accountRepository;

        public BannedService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public UserDto SetFlagIsBanned(long id, bool flag)
        {
            var existingAccount = this.accountRepository.Get(id) ?? throw new NotFoundUserException();

            existingAccount.IsBanned = flag;

            var updatingAccount = this.accountRepository.Update(existingAccount);
            var updatingAccountBl = Mapper.Map<UserDto>(updatingAccount);

            return updatingAccountBl;
        }

        /// <summary>
        /// Set or remove flag banned
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag"></param>
        /// <returns>account</returns>
        public async Task<UserDto> SetFlagIsBannedAsync(long id, bool flag)
        {
            var existingAccount = this.accountRepository.Get(id) ?? throw new NotFoundUserException();

            existingAccount.IsBanned = flag;

            var updatingAccount = await this.accountRepository.UpdateAsync(existingAccount);
            var updatingAccountBl = Mapper.Map<UserDto>(updatingAccount);

            return updatingAccountBl;
        }
    }
}
