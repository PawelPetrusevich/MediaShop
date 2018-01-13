using System;
using AutoMapper;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Dto.User.Validators;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.User;

namespace MediaShop.BusinessLogic.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _storeSettings;
        private readonly IAccountRepository _storeAccount;

        public SettingsService(IAccountRepository repositoryAccount, ISettingsRepository repositorySettings)
        {
            _storeSettings = repositorySettings;
            _storeAccount = repositoryAccount;
        }

        public SettingsDomain Modify(SettingsDomain settings)
        {
            var user = _storeAccount.Get(settings.AccountID);
            
            if (user == null)
            {
                throw new NotFoundUserException();
            }

            var userSettings = Mapper.Map<SettingsDomain>(settings);
            userSettings.Id = user.SettingsId;

            //TODO made for building, we should return and work with SettingsDomain, not Settings
            var settingsData = Mapper.Map<Settings>(userSettings);

            var modiffiedSettings = _storeSettings.Update(settingsData);

            return userSettings;
        }
    }
}