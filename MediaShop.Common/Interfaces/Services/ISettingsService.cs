using MediaShop.Common.Dto.User;
using MediaShop.Common.Dto.User.Validators;
using MediaShop.Common.Models.User;

namespace MediaShop.Common.Interfaces.Services
{
    public interface ISettingsService
    {
        Settings Modify(Settings settings);
    }
}