using FluentValidation.Attributes;
using MediaShop.Common.Dto.Messaging.Validators;

namespace MediaShop.Common.Dto.User
{
    [Validator(typeof(AccountPwdRestoreValidator))]
    public class ResetPasswordDto
    {
        public string Token { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}