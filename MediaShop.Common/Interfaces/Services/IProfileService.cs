namespace MediaShop.Common.Interfaces.Services
{
    using MediaShop.Common.Dto.User;
    using MediaShop.Common.Models.User;

    public interface IProfileService
    {
        ProfileBl Create(ProfileBl profileModel);
    }
}
