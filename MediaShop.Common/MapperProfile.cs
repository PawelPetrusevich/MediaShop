﻿// <copyright file="MapperProfile.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>
namespace MediaShop.Common
{
using System;
using AutoMapper;
using MediaShop.Common.Dto;
using MediaShop.Common.Models.User;
using MediaShop.Common.Models.Notification;

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
            this.CreateMap<UserDto, Account>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Id))
                .ForMember(x => x.Login, opt => opt.MapFrom(m => m.Login))
                .ForMember(x => x.Password, opt => opt.MapFrom(m => m.Password))
                .ForMember(x => x.CreatorId, opt => opt.MapFrom(m => m.CreatorId))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(m => m.CreatedDate))
                .ForMember(x => x.ModifierId, opt => opt.MapFrom(m => m.ModifierId))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(m => m.ModifiedDate)).ReverseMap();
            this.CreateMap<Notification, NotificationDto>().ReverseMap().ForMember(n => n.CreatedDate, obj => obj.UseValue(DateTime.Now));
        }
    }
}
