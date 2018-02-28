using MediaShop.Common.Interfaces.Repositories;

namespace MediaShop.Common.Interfaces.Services
{
    public interface IUserFactoryRepository
    {
        IAccountRepository Accounts { get; set; }

        IProfileRepository Profiles { get; set; }

        ISettingsRepository Settings { get; set; }
    }
}
