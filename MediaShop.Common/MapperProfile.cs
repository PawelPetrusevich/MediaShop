﻿// <copyright file="MapperProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MediaShop.Common
{
    using AutoMapper;
    using MediaShop.Common.Dto;
    using MediaShop.Common.Models.User;

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
            Mapper.Initialize(config => config.CreateMap<Models.Entity, Dto.ProductDto>().ForMember(x => x.ProductId, opt => opt.MapFrom(source => source.Id)).ReverseMap());

            Mapper.Initialize(config => config.CreateMap<Models.Content.Product, Dto.ProductDto>().ReverseMap());

            // to do: output result, add source references (objects)
            this.CreateMap<UserDto, Account>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Id))
                .ForMember(x => x.Login, opt => opt.MapFrom(m => m.Login))
                .ForMember(x => x.Password, opt => opt.MapFrom(m => m.Password))
                .ForMember(x => x.CreatorId, opt => opt.MapFrom(m => m.CreatorId))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(m => m.CreatedDate))
                .ForMember(x => x.ModifierId, opt => opt.MapFrom(m => m.ModifierId))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(m => m.ModifiedDate)).ReverseMap();
        }
    }
}
