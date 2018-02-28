// <copyright file="MapperProfile.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using System.Collections.Generic;
using MediaShop.Common.Dto.Messaging;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Helpers;
using MediaShop.Common.Dto.User.Validators;

namespace MediaShop.Common
{
    using System;

    using AutoMapper;
    using MediaShop.Common.Dto;
    using MediaShop.Common.Dto.Payment;
    using MediaShop.Common.Models;
    using MediaShop.Common.Models.User;
    using MediaShop.Common.Models.Notification;
    using MediaShop.Common.Dto.Messaging;
    using MediaShop.Common.Dto.Product;
    using MediaShop.Common.Models.Content;
    using MediaShop.Common.Models.PaymentModel;

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
                .ForMember(item => item.ContentName, y => y.MapFrom(x => x.ProductName))
                .ForMember(item => item.DescriptionItem, y => y.MapFrom(x => x.Description))
                .ForMember(item => item.PriceItem, y => y.MapFrom(x => x.ProductPrice));

            this.CreateMap<ContentCartDto, ContentCart>().ReverseMap()
                .ForMember(item => item.ContentId, x => x.MapFrom(y => y.ProductId))
                .ForMember(item => item.ContentName, x => x.MapFrom(y => y.Product.ProductName))
                .ForMember(item => item.PriceItem, x => x.MapFrom(y => y.Product.ProductPrice))
                .ForMember(item => item.DescriptionItem, x => x.MapFrom(y => y.Product.Description));

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

            this.CreateMap<Account, AccountDbModel>()
                .ForMember(item => item.Id, opt => opt.Ignore()).ReverseMap();
            this.CreateMap<AccountDbModel, AccountConfirmationDto>()
                .ForMember(item => item.Token, opt => opt.MapFrom(s => s.AccountConfirmationToken));
            this.CreateMap<AccountDbModel, AccountPwdRestoreDto>()
                .ForMember(item => item.Token, opt => opt.MapFrom(s => s.AccountConfirmationToken));
            this.CreateMap<AccountDbModel, UserDto>();
            this.CreateMap<Dto.User.Profile, ProfileDbModel>()
                .ForMember(item => item.Id, m => m.Ignore())
                .ForMember(item => item.CreatedDate, m => m.Ignore())
                .ForMember(item => item.CreatorId, m => m.Ignore())
                .ReverseMap();
            this.CreateMap<ProfileDto, ProfileDbModel>()                
                .ForMember(item => item.CreatedDate, m => m.Ignore())
                .ForMember(item => item.CreatorId, m => m.Ignore())
                .ReverseMap().ForMember(item => item.AccountId, m => m.Ignore());
            this.CreateMap<SettingsDbModel, Settings>().ReverseMap();
            this.CreateMap<SettingsDbModel, SettingsDto>()
                .ForMember(item => item.AccountId, m => m.Ignore())
                .ReverseMap()
                .ForMember(item => item.Id, m => m.Ignore())
                .ForMember(item => item.CreatedDate, m => m.Ignore())
                .ForMember(item => item.CreatorId, m => m.Ignore());
            this.CreateMap<RegisterUserDto, AccountDbModel>().ReverseMap();
            this.CreateMap<RoleUserDto, UserDto>();
            this.CreateMap<Account, RegisterUserDto>().ReverseMap();
            this.CreateMap<Account, UserDto>().ReverseMap();
            this.CreateMap<Dto.User.Profile, ProfileDto>().ReverseMap();
            this.CreateMap<Settings, SettingsDto>().ReverseMap();
            this.CreateMap<ProductDto, Product>().ReverseMap();
            this.CreateMap<UploadProductModel, Product>().ReverseMap();
            this.CreateMap<UploadProductModel, ProductDto>().ReverseMap();
            this.CreateMap<ContentCart, CompressedProductDTO>()
                .ForMember(item => item.Id, x => x.MapFrom(y => y.ProductId))
                .ForMember(item => item.ProductName, x => x.MapFrom(y => y.Product.ProductName))
                .ForMember(item => item.ProductType, x => x.MapFrom(y => y.Product.ProductType))
                .ForMember(item => item.ProductPrice, x => x.MapFrom(y => y.Product.ProductPrice))
                .ForMember(item => item.Content, x => x.MapFrom(y => Convert.ToBase64String(y.Product.CompressedProduct.Content)));
            this.CreateMap<ContentCart, OriginalProductDTO>()
                .ForMember(item => item.Id, x => x.MapFrom(y => y.ProductId))
                .ForMember(item => item.ProductName, x => x.MapFrom(y => y.Product.ProductName))
                .ForMember(item => item.Content, x => x.MapFrom(y => Convert.ToBase64String(y.Product.OriginalProduct.Content)));
            this.CreateMap<Product, CompressedProductDTO>()
                .ForMember(item => item.Content, obj => obj.MapFrom(y => Convert.ToBase64String(y.CompressedProduct.Content)));
            this.CreateMap<ContentCartDto, DefrayalDbModel>()
                .ForMember(item => item.Id, opt => opt.Ignore())
                .ForMember(item => item.CreatorId, opt => opt.Ignore())
                .ForMember(item => item.CreatedDate, opt => opt.Ignore())
                .ForMember(item => item.ModifierId, opt => opt.Ignore())
                .ForMember(item => item.ModifiedDate, opt => opt.Ignore());
            this.CreateMap<PayPal.Api.Payment, PayPalPaymentDbModel>()
                .ForMember(item => item.Id, opt => opt.UseValue<long>(1))
                .ForMember(item => item.PaymentId, opt => opt.MapFrom(n => n.id))
                .ForMember(item => item.State, opt => opt.UseValue<Common.Enums.PaymentEnums.PaymentStates>(Common.Enums.PaymentEnums.PaymentStates.Approved));
            this.CreateMap<PayPal.Api.Item, ItemDto>()
                .ForMember(item => item.Sku, opt => opt.MapFrom(n => n.sku))
                .ForMember(item => item.Name, opt => opt.MapFrom(n => n.name))
                .ForMember(item => item.Price, opt => opt.MapFrom(n => n.price));
            this.CreateMap<Product, ProductInfoDto>()
                .ForMember(x => x.Content, obj => obj.MapFrom(y => Convert.ToBase64String(y.ProtectedProduct.Content)));
        }
    }
}
