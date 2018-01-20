using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Models.User;

namespace MediaShop.BusinessLogic.Services
{
    using AutoMapper;

    using MediaShop.Common.Dto.User;
    using MediaShop.Common.Exceptions;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Interfaces.Services;

    using Profile = MediaShop.Common.Dto.User.Profile;

    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository storeProfile;
        private readonly IAccountRepository storeAccount;

        public ProfileService(IProfileRepository profileRepository, IAccountRepository accountRepository)
        {
            this.storeProfile = profileRepository;
            this.storeAccount = accountRepository;
        }

        public Profile Create(Common.Dto.User.Profile profileModel)
        {
            var existingAccount = this.storeAccount.GetByLogin(profileModel.Login);

            if (existingAccount == null)
            {
                throw new ExistingLoginException(profileModel.Login);
            }

            var profile = Mapper.Map<ProfileDbModel>(profileModel);

            if (profile != null)
            {
                profile.Id = existingAccount.ProfileId ?? 0;

                var updatingProfile = this.storeProfile.Update(profile);
                var updatingProfileBl = Mapper.Map<Common.Dto.User.Profile>(updatingProfile);

                return updatingProfileBl;
            }

            return null;
        }
    }
}
