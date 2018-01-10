// <copyright file="MapperProfile.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using MediaShop.Common.Dto.User;

namespace MediaShop.Common
{
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
               this.CreateMap<Product, ContentCart>()
                .ForMember(item => item.CreatorId, m => m.Ignore());
            this.CreateMap<RegisterUserDto, Account>().ReverseMap();
            this.CreateMap<SettingsDto, Settings>().ForMember(x => x.AccountId, opt => opt.MapFrom(m => m.UserId)).ReverseMap();
        }
    }
}
