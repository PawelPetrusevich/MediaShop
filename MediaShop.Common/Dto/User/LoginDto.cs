using FluentValidation.Attributes;
using MediaShop.Common.Dto.User.Validators;

namespace MediaShop.Common.Dto.User
{
    public class LoginDto
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}