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

        public Account SetRemoveFlagIsBanned(Account accountBLmodel, bool flag)
        {
            var existingAccount = this._factoryRepository.Accounts.GetByLogin(accountBLmodel.Login) ??
                                  throw new NotFoundUserException();

            existingAccount.IsBanned = flag;

            var updatingAccount = this._factoryRepository.Accounts.Update(existingAccount);
            var updatingAccountBl = Mapper.Map<Account>(updatingAccount);

            return updatingAccountBl;
        }
    }
}
