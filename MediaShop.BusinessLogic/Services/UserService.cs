using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MediaShop.Common.Dto;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.User;
using MediaShop.Common.Helpers;

namespace MediaShop.BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly IUserRepository store;

        public UserService(IUserRepository repository)
        {
            this.store = repository;
        }

        public bool Register(UserDto userModel)
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

            //TODO: Don't know what is the problem in Repository
            if (createdAccount == null)
            {
                return false;
            }

            return true;
        }

        public bool RemoveRole(int id, Role role)
        {
            var user = this.store.Find(account => account.Id == id).FirstOrDefault();

            return user?.Permissions.Remove(accountRole => accountRole == role) > 0;
        }
    }
}