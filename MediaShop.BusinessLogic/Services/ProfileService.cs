using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.BusinessLogic.Services
{
    using AutoMapper;

    using MediaShop.Common.Dto.User;
    using MediaShop.Common.Exceptions;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Interfaces.Services;

    using Microsoft.Win32.SafeHandles;

    using Profile = MediaShop.Common.Models.User.Profile;

    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository storeProfile;
        private readonly IAccountRepository storeAccount;

        public ProfileService(IProfileRepository profileRepository, IAccountRepository accountRepository)
        {
            this.storeProfile = profileRepository;
            this.storeAccount = accountRepository;
        }

        public Profile Create(ProfileDto profileModel)
        {
            var existingAccount = this.storeAccount.GetByLogin(profileModel.Login);

            if (existingAccount == null)
            {
                throw new ExistingLoginException(profileModel.Login);
            }

            var profile = Mapper.Map<Profile>(profileModel);

            if (profile != null)
            {
                profile.Id = existingAccount.ProfileId;

                var updatingProfile = this.storeProfile.Update(profile);

                return updatingProfile;
            }

            return null;
        }
    }
}
