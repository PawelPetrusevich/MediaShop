using System;
using AutoMapper;
using MediaShop.Common.Dto.User;
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

        public Settings Modify(SettingsDto settings)
        {
            var user = _storeAccount.Get(settings.UserId);
            
            if (user == null)
            {
                throw new NotFoundUserException();
            }

            var userSettings = Mapper.Map<Settings>(settings);
            userSettings.Id = user.SettingsId;

            var modiffiedSettings = _storeSettings.Update(userSettings);

            return modiffiedSettings;
        }
    }
}