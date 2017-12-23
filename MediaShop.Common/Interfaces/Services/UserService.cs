﻿using System.Linq;
using AutoMapper;
using MediaShop.Common.Dto;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.User;

namespace MediaShop.Common.Interfaces.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IRespository<Account> store;

        public UserService(IRespository<Account> repository)
        {
            this.store = repository;
        }

        public Account Register(UserDto userModel)
        {
            if (this.store.Find(x => x.Login == userModel.Login).FirstOrDefault() != null)
            {
                throw new ExistingLoginException(userModel.Login);
            }

            Mapper.Initialize(config => config.CreateMap<UserDto, Account>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.Id))
                .ForMember(x => x.Login, opt => opt.MapFrom(m => m.Login))
                .ForMember(x => x.Password, opt => opt.MapFrom(m => m.Password))
                .ForMember(x => x.CreatorId, opt => opt.MapFrom(m => m.CreatorId))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(m => m.CreatedDate))
                .ForMember(x => x.ModifierId, opt => opt.MapFrom(m => m.ModifierId))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(m => m.ModifiedDate)));

            var account = Mapper.Map<Account>(userModel);
            account.Permissions.Add(userModel.UserRole);

            var createdAccount = this.store.Add(account);

            //TODO Check if account was created. If not - return not.

            return createdAccount;
        }
    }
}