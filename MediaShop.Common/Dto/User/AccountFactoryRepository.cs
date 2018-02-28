using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Interfaces.Services;

namespace MediaShop.Common.Dto.User
{
    public class AccountFactoryRepository : IAccountFactoryRepository
    {
        public AccountFactoryRepository(IAccountRepository accountRepository, IProfileRepository profileRepository, ISettingsRepository settingsRepository, IStatisticRepository statisticRepository)
        {
            Accounts = accountRepository;
            Profiles = profileRepository;
            Settings = settingsRepository;
            Statistics = statisticRepository;
        }

        public IAccountRepository Accounts { get; set; }

        public IProfileRepository Profiles { get; set; }

        public ISettingsRepository Settings { get; set; }

        public IStatisticRepository Statistics { get; set; }
    }
}