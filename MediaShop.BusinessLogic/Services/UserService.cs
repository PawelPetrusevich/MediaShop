using System;
using System.Threading.Tasks;
using AutoMapper;
using MediaShop.BusinessLogic.Properties;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Dto.User.Validators;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Exceptions.User;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.User;
using Profile = MediaShop.Common.Dto.User.Profile;

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
        public Account SoftDeleteByUser(long idUser)
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
        public async Task<Account> SoftDeleteByUserAsync(long idUser)
        {
            if (idUser < 1)
            {
                throw new ArgumentException(Resources.InvalidIdValue, nameof(idUser));
            }

            var user = await _userRepository.Accounts.GetAsync(idUser).ConfigureAwait(false) ?? throw new NotFoundUserException();

            var deletedUser = await _userRepository.Accounts.SoftDeleteAsync(user.Id) ??
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
        public async Task<UserDto> SoftDeleteAsync(long idUser)
        {
            if (idUser < 1)
            {
                throw new ArgumentException(Resources.InvalidIdValue, nameof(idUser));
            }

            var user = await _userRepository.Accounts.GetAsync(idUser) ?? throw new NotFoundUserException();

            var deletedUser = await _userRepository.Accounts.SoftDeleteAsync(user.Id)
                              ?? throw new DeleteUserException($"{idUser}");

            return Mapper.Map<UserDto>(deletedUser);
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

            var user = await _userRepository.Accounts.GetAsync(idUser).ConfigureAwait(false) ?? throw new NotFoundUserException();

            return Mapper.Map<Account>(user);
        }

        public async Task<UserDto> GetUserDtoAsync(long idUser)
        {
            if (idUser < 1)
            {
                throw new ArgumentException(Resources.InvalidIdValue, nameof(idUser));
            }

            var user = await _userRepository.Accounts.GetAsync(idUser).ConfigureAwait(false) ?? throw new NotFoundUserException();

            return Mapper.Map<UserDto>(user);
        }

        public Settings ModifySettings(SettingsDto settings)
        {
            var user = _userRepository.Accounts.Get(settings.AccountId) ?? throw new NotFoundUserException();
            user.Settings.InterfaceLanguage = settings.InterfaceLanguage;
            user.Settings.NotificationStatus = settings.NotificationStatus;
            user.Settings.TimeZoneId = settings.TimeZoneId;

            var updatedUser = _userRepository.Accounts.Update(user) ?? throw new UpdateAccountException();

            return Mapper.Map<Settings>(updatedUser.Settings);
        }

        public async Task<Settings> ModifySettingsAsync(SettingsDto settings)
        {
            var user = await _userRepository.Accounts.GetAsync(settings.AccountId).ConfigureAwait(false) ?? throw new NotFoundUserException();
            user.Settings.InterfaceLanguage = settings.InterfaceLanguage;
            user.Settings.NotificationStatus = settings.NotificationStatus;

            var updatedUser = await _userRepository.Accounts.UpdateAsync(user).ConfigureAwait(false)
                              ?? throw new UpdateAccountException();

            return Mapper.Map<Settings>(updatedUser.Settings);
        }

        public Profile ModifyProfile(ProfileDto profile)
        {
            var user = _userRepository.Accounts.Get(profile.AccountId) ?? throw new NotFoundUserException();          
            user.Profile.DateOfBirth = profile.DateOfBirth;
            user.Profile.FirstName = profile.FirstName;
            user.Profile.LastName = profile.LastName;
            user.Profile.Phone = profile.Phone;

            var updatedUser = _userRepository.Accounts.Update(user) ?? throw new UpdateAccountException();

            return Mapper.Map<Profile>(updatedUser.Profile);
        }

        public async Task<Profile> ModifyProfileAsync(ProfileDto profile)
        {
            var user = await _userRepository.Accounts.GetAsync(profile.AccountId).ConfigureAwait(false) ?? throw new NotFoundUserException();
            user.Profile.DateOfBirth = profile.DateOfBirth;
            user.Profile.FirstName = profile.FirstName;
            user.Profile.LastName = profile.LastName;
            user.Profile.Phone = profile.Phone;

            var updatedUser = await _userRepository.Accounts.UpdateAsync(user).ConfigureAwait(false)
                              ?? throw new UpdateAccountException();

            return Mapper.Map<Profile>(updatedUser.Profile);
        }
    }
}