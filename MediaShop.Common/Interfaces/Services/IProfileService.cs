using System.Threading.Tasks;

namespace MediaShop.Common.Interfaces.Services
{
    using MediaShop.Common.Dto.User;
    using MediaShop.Common.Models.User;

    public interface IProfileService
    {
        Profile Create(Profile profileModel);

        Task<Profile> CreateAsync(Profile profileModel);
    }
}
