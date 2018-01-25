// <copyright file="MapperProfile.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using MediaShop.Common.Dto.User;
using MediaShop.Common.Helpers;

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
                .ForMember(item => item.CreatorId, m => m.Ignore());
            this.CreateMap<ContentCartDto, ContentCart>().ReverseMap();
            this.CreateMap<Notification, NotificationDto>().ReverseMap()
                .ForMember(n => n.CreatedDate, obj => obj.UseValue(DateTime.Now))
                .ForMember(n => n.CreatorId, obj => obj.MapFrom(nF => nF.SenderId));
            this.CreateMap<NotificationSubscribedUser, NotificationSubscribedUserDto>().ReverseMap()
                .ForMember(n => n.CreatedDate, obj => obj.UseValue(DateTime.Now))
                .ForMember(n => n.CreatorId, obj => obj.MapFrom(nF => nF.UserId));
            this.CreateMap<AddToCartNotifyDto, NotificationDto>()
                .ForMember(
                    n => n.Message,
                    obj => obj.ResolveUsing(d => NotificationHelper.FormatAddProductToCartMessage(d.ProductName)))
                .ForMember(n => n.SenderId, obj => obj.MapFrom(s => s.ReceiverId));
        }
    }
}