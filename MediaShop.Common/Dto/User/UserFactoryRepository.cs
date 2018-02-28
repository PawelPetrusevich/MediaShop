using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Interfaces.Services;

namespace MediaShop.Common.Dto.User
{
    public class UserFactoryRepository : IUserFactoryRepository
    {
        public UserFactoryRepository(IAccountRepository accountRepository, IProfileRepository profileRepository, ISettingsRepository settingsRepository)
        {
            Accounts = accountRepository;
            Profiles = profileRepository;
            Settings = settingsRepository;
        }

        public IAccountRepository Accounts { get; set; }

        public IProfileRepository Profiles { get; set; }

        public ISettingsRepository Settings { get; set; }
    }
}