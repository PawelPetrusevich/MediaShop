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

    class ProfileService:IProfileService
    {
        private readonly IProfileRepository storeProfile;
        private readonly IAccountRepository storeAccount;

        public ProfileService(IProfileRepository profileRepository, IAccountRepository accountRepository)
        {
            this.storeProfile = profileRepository;
            this.storeAccount = accountRepository;
        }

        public Profile Create(ProfileDto profileModel,string login)
        {
            var existingAccount = this.storeAccount.GetByLogin(login);

            if (existingAccount != null)
            {
                throw new ExistingLoginException(login);
            }

            var profile = Mapper.Map<Profile>(profileModel);

            var createdProfile = this.storeProfile.Add(profile);

            return createdProfile;
        }
    }
}
