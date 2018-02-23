using MediaShop.Common.Models.User;

namespace MediaShop.Common.Dto.User
{
    public class UserDto
    {
        public long Id { get; set; }

        public Permissions Permissions { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public bool IsBanned { get; set; }

        public bool IsDeleted { get; set; }
    }
}