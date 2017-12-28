using System.Linq;
using AutoMapper;
using MediaShop.Common.Dto;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.User;
using MediaShop.Common.Helpers;

namespace MediaShop.BusinessLogic
{
    /// <summary>
    /// Class with user service business logic
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IRespository<Account> store;

        /// <summary>
        /// Initialize new instanse of the class UserService
        /// </summary>
        /// <param name="repository">Data store</param>
        public UserService(IRespository<Account> repository)
        {
            this.store = repository;
        }

        /// <summary>
        /// User registration
        /// </summary>
        /// <param name="userModel">Dto registering user</param>
        /// <returns><value>true - is registered</value>
        /// <value>false - is not registered</value></returns>
        public bool Register(UserDto userModel)
        {
            if (this.store.Find(x => x.Login == userModel.Login).FirstOrDefault() != null)
            {
                throw new ExistingLoginException(userModel.Login);
            }

            var account = Mapper.Map<Account>(userModel);
            account.Permissions.Add(userModel.UserRole);

            var createdAccount = this.store.Add(account);

            if (createdAccount == null || createdAccount.Id == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Remove user role, from permissions list
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="role">Role</param>
        /// <returns><value>true - role removed</value>
        /// <value>false -role did not remove</value></returns>
        public bool RemoveRole(int id, Role role)
        {
            var user = this.store.Find(account => account.Id == id).FirstOrDefault();

            return user?.Permissions.Remove(accountRole => accountRole == role) > 0;
        }
    }
}