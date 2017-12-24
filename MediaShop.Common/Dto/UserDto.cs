using MediaShop.Common.Models;
using MediaShop.Common.Models.User;

namespace MediaShop.Common.Dto
{
    public class UserDto : Entity
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public Role UserRole { get; set; }
    }
}