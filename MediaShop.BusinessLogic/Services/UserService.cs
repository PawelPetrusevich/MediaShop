using System;
using System.Threading.Tasks;
using AutoMapper;
using MediaShop.BusinessLogic.Properties;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Exceptions.PaymentExceptions;
using MediaShop.Common.Exceptions.User;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.User;

namespace MediaShop.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private IAccountRepository _userRepository;

        public UserService(IAccountRepository userRepository)
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

            var user = _userRepository.Get(idUser) ?? throw new NotFoundUserException();

            var deletedUser = _userRepository.SoftDelete(user.Id) ??
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

            var user = await _userRepository.GetAsync(idUser) ?? throw new NotFoundUserException();

            var deletedUser = await _userRepository.SoftDeleteAsync(user.Id) ??
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

            var user = _userRepository.Get(idUser) ?? throw new NotFoundUserException();

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

            var user = await _userRepository.GetAsync(idUser) ?? throw new NotFoundUserException();

            return Mapper.Map<Account>(user);
        }

        public Account ModifySettings(SettingsDto settings)
        {
            //Validator
            var user = _userRepository.Get(settings.AccountId) ?? throw new NotFoundUserException();

            var settingsUpdated = Mapper.Map<SettingsDbModel>(settings);
            user.Settings = settingsUpdated;
            
            var updatedUser = _userRepository.Update(user) ?? throw new UpdateAccountException();

            return Mapper.Map<Account>(updatedUser);
        }

        public async Task<Account> ModifySettingsAsync(SettingsDto settings)
        {
            //Validator
            var user = await _userRepository.GetAsync(settings.AccountId) ?? throw new NotFoundUserException();

            var settingsUpdated = Mapper.Map<SettingsDbModel>(settings);
            user.Settings = settingsUpdated;

            var updatedUser = await _userRepository.UpdateAsync(user) ?? throw new UpdateAccountException();
       
            return Mapper.Map<Account>(updatedUser);
        }

        public Account ModifyProfile(ProfileDto profile)
        {
            //Validator
            var user = _userRepository.Get(profile.AccountId) ?? throw new NotFoundUserException();

            var profileUpdated = Mapper.Map<ProfileDbModel>(profile);
            user.Profile = profileUpdated;

            var updatedUser = _userRepository.Update(user) ?? throw new UpdateAccountException();

            return Mapper.Map<Account>(updatedUser);
        }

        public async Task<Account> ModifyProfileAsync(ProfileDto profile)
        {
            //Validator
            var user = await _userRepository.GetAsync(profile.AccountId) ?? throw new NotFoundUserException();

            var profileUpdated = Mapper.Map<ProfileDbModel>(profile);
            user.Profile = profileUpdated;

            var updatedUser = await _userRepository.UpdateAsync(user) ?? throw new UpdateAccountException();

            return Mapper.Map<Account>(updatedUser);
        }
    }
}