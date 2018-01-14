// <copyright file="MapperProfile.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using MediaShop.Common.Dto.User;
using MediaShop.Common.Dto.User.Validators;

namespace MediaShop.Common
{
    using System;

    using AutoMapper;
    using MediaShop.Common.Dto;
    using MediaShop.Common.Models;
    using MediaShop.Common.Models.CartModels;
    using MediaShop.Common.Models.User;

    using Profile = AutoMapper.Profile;

    /// <summary>
    /// Class MapperProfile.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class MapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapperProfile"/> class.
        /// </summary>
        public MapperProfile()
        {
            this.CreateMap<Product, ContentCartDto>()
             .ForMember(item => item.CreatorId, m => m.Ignore());
            this.CreateMap<ContentCartDto, ContentCart>().ReverseMap();

            this.CreateMap<RegisterUserDto, Account>().ReverseMap();

            this.CreateMap<ProfileBl, Models.User.Profile>().ForMember(item => item.Id, m => m.Ignore())
                .ForMember(item => item.CreatedDate, m => m.Ignore())
                .ForMember(item => item.CreatorId, m => m.Ignore());

            this.CreateMap<AccountDomain, RegisterUserDto>();
            this.CreateMap<ProfileBl, ProfileDto>().ReverseMap();
            this.CreateMap<SettingsDomain, SettingsDto>().ReverseMap();

            this.CreateMap<Account, AccountDomain>().ReverseMap();
            this.CreateMap<Settings, SettingsDomain>().ReverseMap();
        }
    }
}
