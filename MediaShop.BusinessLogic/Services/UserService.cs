using System;
using System.Threading.Tasks;
using AutoMapper;
using MediaShop.BusinessLogic.Properties;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Exceptions.PaymentExceptions;
using MediaShop.Common.Exceptions.User;
using MediaShop.Common.Interfaces.Services;

namespace MediaShop.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private IUserFactoryRepository _userRepository;

        public UserService(IUserFactoryRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Delete user by setting flag deleted in model account 
        /// </summary>
        /// <param name="idUser"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotFoundUserException"></exception>
        /// <returns>account</returns>
        public Account SoftDelete(long idUser)
        {
            if (idUser < 1)
            {
                throw new ArgumentException(Resources.InvalidIdValue, nameof(idUser));
            }

            var user = _userRepository.Accounts.Get(idUser) ?? throw new NotFoundUserException();

            var deletedUser = _userRepository.Accounts.SoftDelete(user.Id) ??
                              throw new DeleteUserException($"{idUser}");

            return Mapper.Map<Account>(deletedUser);
        }

        /// <summary>
        /// Delete user by setting flag deleted in model account 
        /// </summary>
        /// <param name="idUser"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotFoundUserException"></exception>
        /// <returns>account</returns>
        public async Task<Account> SoftDeleteAsync(long idUser)
        {
            if (idUser < 1)
            {
                throw new ArgumentException(Resources.InvalidIdValue, nameof(idUser));
            }

            var user = await _userRepository.Accounts.GetAsync(idUser) ?? throw new NotFoundUserException();

            var deletedUser = await _userRepository.Accounts.SoftDeleteAsync(user.Id) ??
                              throw new DeleteUserException($"{idUser}");

            return Mapper.Map<Account>(deletedUser);
        }

        /// <summary>
        /// Get user information
        /// </summary>
        /// <param name="idUser"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotFoundUserException"></exception>
        /// <returns>account</returns>
        public Account GetUserInfo(long idUser)
        {
            if (idUser < 1)
            {
                throw new ArgumentException(Resources.InvalidIdValue, nameof(idUser));
            }

            var user = _userRepository.Accounts.Get(idUser) ?? throw new NotFoundUserException();

            return Mapper.Map<Account>(user);
        }

        /// <summary>
        /// Get user information
        /// </summary>
        /// <param name="idUser"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NotFoundUserException"></exception>
        /// <returns>account</returns>
        public async Task<Account> GetUserInfoAsync(long idUser)
        {
            if (idUser < 1)
            {
                throw new ArgumentException(Resources.InvalidIdValue, nameof(idUser));
            }

            var user = await _userRepository.Accounts.GetAsync(idUser) ?? throw new NotFoundUserException();

            return Mapper.Map<Account>(user);
        }
    }
}