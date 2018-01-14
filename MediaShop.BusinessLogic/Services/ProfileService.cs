﻿using System;
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

        public ProfileBl Create(ProfileBl profileModel)
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
                var updatingProfileBl = Mapper.Map<ProfileBl>(profileModel);

                return updatingProfileBl;
            }

            return null;
        }
    }
}
