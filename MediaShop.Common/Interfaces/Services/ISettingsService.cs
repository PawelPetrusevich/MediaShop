using MediaShop.Common.Dto.User;
using MediaShop.Common.Models.User;

namespace MediaShop.Common.Interfaces.Services
{
    public interface ISettingsService
    {
        Settings Modify(SettingsDto settings);
    }
}