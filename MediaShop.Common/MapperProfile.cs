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
    using MediaShop.Common.Models.User;
    using MediaShop.Common.Models.Notification;
    using MediaShop.Common.Dto.Product;
    using MediaShop.Common.Models.Content;

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
                .ForMember(item => item.Id, y => y.Ignore())
                .ForMember(item => item.ContentId, y => y.MapFrom(x => x.Id))
                .ForMember(item => item.CreatorId, m => m.Ignore());
            this.CreateMap<ContentCartDto, ContentCart>().ReverseMap()
                .ForMember(item => item.ContentName, x => x.MapFrom(y => y.Product.ProductName))
                .ForMember(item => item.PriceItem, x => x.MapFrom(y => y.Product.ProductPrice))
                .ForMember(item => item.DescriptionItem, x => x.MapFrom(y => y.Product.Description));

            this.CreateMap<Dto.User.Profile, ProfileDbModel>()
                .ForMember(item => item.Id, m => m.Ignore())
                .ForMember(item => item.CreatedDate, m => m.Ignore())
                .ForMember(item => item.CreatorId, m => m.Ignore())
                .ReverseMap().ForMember(item => item.Login, m => m.Ignore());

            this.CreateMap<Notification, NotificationDto>().ReverseMap()
                .ForMember(n => n.CreatedDate, obj => obj.UseValue(DateTime.Now))
                .ForMember(n => n.CreatorId, obj => obj.MapFrom(nF => nF.SenderId));
            this.CreateMap<NotificationSubscribedUser, NotificationSubscribedUserDto>().ReverseMap()
                .ForMember(n => n.CreatedDate, obj => obj.UseValue(DateTime.Now))
                .ForMember(n => n.CreatorId, obj => obj.MapFrom(nF => nF.UserId));

            this.CreateMap<Account, AccountDbModel>()
                .ForMember(item => item.Id, opt => opt.Ignore()).ReverseMap();
            this.CreateMap<SettingsDbModel, Settings>().ForMember(item => item.AccountID, opt => opt.Ignore()).ReverseMap();
            this.CreateMap<RegisterUserDto, AccountDbModel>().ReverseMap();
            this.CreateMap<RoleUserDto, PermissionDto>();
            this.CreateMap<Account, RegisterUserDto>().ReverseMap();
            this.CreateMap<Dto.User.Profile, ProfileDto>().ReverseMap();
            this.CreateMap<Settings, SettingsDto>().ReverseMap();
            this.CreateMap<ProductDto, Product>().ReverseMap();
            this.CreateMap<UploadProductModel, Product>().ReverseMap();
            this.CreateMap<UploadProductModel, ProductDto>().ReverseMap();
            this.CreateMap<ContentCart, CompressedProductDTO>()
                .ForMember(item => item.Id, x => x.MapFrom(y => y.ProductId))
                .ForMember(item => item.ProductName, x => x.MapFrom(y => y.Product.ProductName))
                .ForMember(item => item.Content, x => x.MapFrom(y => Convert.ToBase64String(y.Product.CompressedProduct.Content)));
            this.CreateMap<ContentCart, OriginalProductDTO>()
                .ForMember(item => item.Id, x => x.MapFrom(y => y.ProductId))
                .ForMember(item => item.ProductName, x => x.MapFrom(y => y.Product.ProductName))
                .ForMember(item => item.Content, x => x.MapFrom(y => Convert.ToBase64String(y.Product.OriginalProduct.Content)));
            this.CreateMap<Product, CompressedProductDTO>()
                .ForMember(item => item.Content, obj => obj.MapFrom(y => Convert.ToBase64String(y.CompressedProduct.Content)));
        }
    }
}
