using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.BusinessLogic.Properties;
using MediaShop.Common.Exceptions.User;
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

        public Profile Create(Profile profileModel)
        {
            if (profileModel == null)
            {
                throw new ArgumentNullException(Resources.NullOrEmptyValue, nameof(profileModel));
            }

            var existingAccount = this.storeAccount.GetByLogin(profileModel.Login);

            if (existingAccount == null)
            {
                throw new ExistingLoginException(profileModel.Login);
            }

            var profile = Mapper.Map<ProfileDbModel>(profileModel);

            profile.Id = existingAccount.ProfileId ?? 0;

            var updatingProfile = this.storeProfile.Update(profile) ?? throw new CreateProfileException();
            var updatingProfileBl = Mapper.Map<Profile>(updatingProfile);

            return updatingProfileBl;
        }
    }
}
