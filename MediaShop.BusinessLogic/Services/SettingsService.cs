using System;
using AutoMapper;
using MediaShop.BusinessLogic.Properties;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Dto.User.Validators;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Exceptions.User;
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

        public Settings Modify(Settings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(Resources.NullOrEmptyValue, nameof(settings));
            }

            var user = _storeAccount.Get(settings.AccountID);

            if (user == null)
            {
                throw new NotFoundUserException();
            }
            
            var settingsData = Mapper.Map<SettingsDbModel>(settings);
            settingsData.Id = user.SettingsId ?? 0;

            var settedSettings = _storeSettings.Update(settingsData) ?? throw new ModiffySettingsException();

            return Mapper.Map<Settings>(settedSettings);
        }
    }
}