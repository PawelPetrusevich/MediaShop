using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Interfaces.Repositories;
using AutoMapper;
using MediaShop.BusinessLogic.Properties;
using MediaShop.Common.Exceptions;

namespace MediaShop.BusinessLogic.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IAccountRepository _accountRepository;

        public PermissionService(IAccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        /// <summary>
        /// Set permission
        /// </summary>
        /// <param name="permissionDto">Permission data</param>
        /// <returns>account</returns>
        public Account SetPermission(UserDto permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException(Resources.NullOrEmptyValue);
            }

            var user = _accountRepository.Get(permission.Id) ?? throw new NotFoundUserException();
            user.Permissions |= permission.Permissions;

            var result = _accountRepository.Update(user) ?? throw new UpdateAccountException();

            return Mapper.Map<Account>(result);
        }

        /// <summary>
        /// Remove permission
        /// </summary>
        /// <param name="permissionDto">Permission data</param>
        /// <returns>account</returns>
        public Account RemovePermission(UserDto permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException(Resources.NullOrEmptyValue);
            }

            var user = _accountRepository.Get(permission.Id) ?? throw new NotFoundUserException();
            user.Permissions &= ~permission.Permissions;

            var result = _accountRepository.Update(user) ?? throw new UpdateAccountException();

            return Mapper.Map<Account>(result);
        }

        public Account SetPermissionMask(UserDto permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException(Resources.NullOrEmptyValue);
            }

            var user = _accountRepository.Get(permission.Id) ?? throw new NotFoundUserException();
            user.Permissions = permission.Permissions;

            var result = _accountRepository.Update(user) ?? throw new UpdateAccountException();

            return Mapper.Map<Account>(result);
        }

        public async Task<Account> SetPermissionAsync(UserDto permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException(Resources.NullOrEmptyValue);
            }

            var user = await _accountRepository.GetAsync(permission.Id) ?? throw new NotFoundUserException();
            user.Permissions |= permission.Permissions;

            var result = await _accountRepository.UpdateAsync(user) ?? throw new UpdateAccountException();

            return Mapper.Map<Account>(result);
        }

        /// <summary>
        /// Remove permission
        /// </summary>
        /// <param name="permissionDto">Permissions data</param>
        /// <returns>account</returns>
        public async Task<Account> RemovePermissionAsync(UserDto permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException(Resources.NullOrEmptyValue);
            }

            var user = await _accountRepository.GetAsync(permission.Id) ?? throw new NotFoundUserException();
            user.Permissions &= ~permission.Permissions;

            var result = await _accountRepository.UpdateAsync(user) ?? throw new UpdateAccountException();

            return Mapper.Map<Account>(result);
        }
    }
}