// <copyright file="MapperProfile.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using MediaShop.Common.Dto.Product;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Models.Content;

namespace MediaShop.Common
{
    using AutoMapper;
    using MediaShop.Common.Dto;
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
            this.CreateMap<UserDto, Account>().ReverseMap();
            this.CreateMap<ProductDto, Product>().ReverseMap();
            this.CreateMap<UploadModel, Product>().ReverseMap();
            this.CreateMap<UploadModel, ProductDto>().ReverseMap();
        }
    }
}
