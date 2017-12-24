using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Dto;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.User;
using MediaShop.DataAccess.Repositories;

namespace MediaShop.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IRespository<Account> userRepository;

        public UserService(IRespository<Account> respository)
        {
            this.userRepository = respository ?? throw new ArgumentNullException(nameof(respository));
        }

        public object Respository { get; set; }

        public bool Register(RegistrationUserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool RemoveRole(int id, Role role)
        {
            var user = this.userRepository.Find(account => account.Id == id).FirstOrDefault();

            if (user?.Permissions.RemoveWhere(accountRole => accountRole == role) > 0)
            {
                return true;
            }

            return false;
        }
    }
}
