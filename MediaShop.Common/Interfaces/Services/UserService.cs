using System.Linq;
using AutoMapper;
using MediaShop.Common.Dto;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.User;

namespace MediaShop.Common.Interfaces.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IRespository<Account> _store;

        public UserService(IRespository<Account> store)
        {
            this._store = store;
        }

        public OperationResult Register(DtoUserModel userModel)
        {
            if (this._store.Find(x => x.Login == userModel.Login).FirstOrDefault() != null)
            {
                return new OperationResult(false, $"User with  such login is allready existed");
            }

            Mapper.Initialize(config => config.CreateMap<DtoUserModel, Account>()
                .ForMember(x => x.Login, opt => opt.MapFrom(m => m.Login))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(m => m.CreatedDate))
                .ForMember(x => x.CreatorId, opt => opt.MapFrom(m => m.CreatorId))
                .ForMember(x => x.Password, opt => opt.MapFrom(m => m.Password))
                .ForMember(x => x.Profile.DateOfBirth, opt => opt.MapFrom(m => m.DateOfBirth))
                .ForMember(x => x.Profile.Email, opt => opt.MapFrom(m => m.Email))
                .ForMember(x => x.Profile.FirstName, opt => opt.MapFrom(m => m.FirstName))
                .ForMember(x => x.Profile.LastName, opt => opt.MapFrom(m => m.LastName))
                .ForMember(x => x.Profile.Phone, opt => opt.MapFrom(m => m.Phone)));

            var account = Mapper.Map<Account>(userModel);

            var createdAccount = this._store.Add(account);

            //TODO Check if account was created. If not - return not.

            return new OperationResult(true);
        }
    }
}