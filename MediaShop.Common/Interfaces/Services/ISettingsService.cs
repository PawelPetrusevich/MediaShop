using System.Threading.Tasks;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Dto.User.Validators;
using MediaShop.Common.Models.User;

namespace MediaShop.Common.Interfaces.Services
{
    public interface ISettingsService
    {
        Settings Create(Settings settings);

        Task<Settings> CreateAsync(Settings settings);
    }
}