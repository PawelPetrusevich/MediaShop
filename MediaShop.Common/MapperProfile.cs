// <copyright file="MapperProfile.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using MediaShop.Common.Dto.User;

namespace MediaShop.Common
{
    using System;
    using AutoMapper;
    using MediaShop.Common.Dto;
    using MediaShop.Common.Models;
    using MediaShop.Common.Models.CartModels;
    using MediaShop.Common.Models.User;
    using MediaShop.Common.Models.Notification;

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
            this.CreateMap<UserDto, Account>().ReverseMap();
            this.CreateMap<Product, ContentCartDto>()
                .ForMember(item => item.Id, y => y.Ignore())
                .ForMember(item => item.ContentId, y => y.MapFrom(x => x.Id))
                .ForMember(item => item.CreatorId, m => m.Ignore());
            this.CreateMap<ContentCartDto, ContentCart>().ReverseMap()
                .ForMember(item => item.ContentName, x => x.MapFrom(y => y.Product.ContentName))
                .ForMember(item => item.PriceItem, x => x.MapFrom(y => y.Product.PriceItem))
                .ForMember(item => item.DescriptionItem, x => x.MapFrom(y => y.Product.DescriptionItem));
            this.CreateMap<Notification, NotificationDto>().ReverseMap()
                .ForMember(n => n.CreatedDate, obj => obj.UseValue(DateTime.Now))
                .ForMember(n => n.CreatorId, obj => obj.MapFrom(nF => nF.SenderId));
            this.CreateMap<NotificationSubscribedUser, NotificationSubscribedUserDto>().ReverseMap()
                .ForMember(n => n.CreatedDate, obj => obj.UseValue(DateTime.Now))
                .ForMember(n => n.CreatorId, obj => obj.MapFrom(nF => nF.UserId));
        }
    }
}
