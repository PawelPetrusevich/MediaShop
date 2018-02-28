using FluentValidation.Attributes;
using MediaShop.Common.Dto.User.Validators;

namespace MediaShop.Common.Dto.User
{
    [Validator(typeof(RegisterUserVaildator))]
    public class RegisterUserDto
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        public string Origin { get; set; }
    }
}