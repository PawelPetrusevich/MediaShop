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

        public Settings ModifySettings(SettingsDto settings)
        {
            //Validator
            var user = _userRepository.Accounts.Get(settings.AccountId) ?? throw new NotFoundUserException();
            var settingsUser = _userRepository.Settings.Get(user.Settings.Id);
            settingsUser.InterfaceLanguage = settings.InterfaceLanguage;
            settingsUser.NotificationStatus = settings.NotificationStatus;
            settingsUser.TimeZoneId = settings.TimeZoneId;

            var updatedSettings = _userRepository.Settings.Update(settingsUser) ?? throw new UpdateSettingsException();

            return Mapper.Map<Settings>(updatedSettings);
        }

        public async Task<Settings> ModifySettingsAsync(SettingsDto settings)
        {
            //Validator
            var user = await _userRepository.Accounts.GetAsync(settings.AccountId) ?? throw new NotFoundUserException();
            var settingsUser = _userRepository.Settings.Get(user.Settings.Id);
            settingsUser.InterfaceLanguage = settings.InterfaceLanguage;
            settingsUser.NotificationStatus = settings.NotificationStatus;
            settingsUser.TimeZoneId = settings.TimeZoneId;

            var updatedSettings = await _userRepository.Settings.UpdateAsync(settingsUser) ?? throw new UpdateSettingsException();
       
            return Mapper.Map<Settings>(updatedSettings);
        }

        public Profile ModifyProfile(ProfileDto profile)
        {
            //Validator
            var user = _userRepository.Accounts.Get(profile.AccountId) ?? throw new NotFoundUserException();
            var profileUser = _userRepository.Profiles.Get(user.Profile.Id);
            profileUser.DateOfBirth = profile.DateOfBirth;
            profileUser.FirstName = profile.FirstName;
            profileUser.LastName = profile.LastName;
            profileUser.Phone = profile.Phone;

            var updatedProfile = _userRepository.Profiles.Update(profileUser) ?? throw new UpdateProfileException();

            return Mapper.Map<Profile>(updatedProfile);
        }

        public async Task<Profile> ModifyProfileAsync(ProfileDto profile)
        {
            //Validator
            var user = await _userRepository.Accounts.GetAsync(profile.AccountId) ?? throw new NotFoundUserException();
            var profileUser = _userRepository.Profiles.Get(user.Profile.Id);
            profileUser.DateOfBirth = profile.DateOfBirth;
            profileUser.FirstName = profile.FirstName;
            profileUser.LastName = profile.LastName;
            profileUser.Phone = profile.Phone;

            var updatedUser = await _userRepository.Profiles.UpdateAsync(profileUser) ?? throw new UpdateProfileException();

            return Mapper.Map<Profile>(updatedUser);
        }
    }
}