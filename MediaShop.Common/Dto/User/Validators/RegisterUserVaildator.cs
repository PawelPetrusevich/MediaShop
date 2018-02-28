using FluentValidation;
using MediaShop.Common.Properties;

namespace MediaShop.Common.Dto.User.Validators
{
    public class RegisterUserVaildator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserVaildator()
        {
            this.RuleFor(m => m.Login).NotEmpty().MinimumLength(2).WithMessage(Resources.IncorrectLogin);
            this.RuleFor(m => m.Password).Equal(m => m.ConfirmPassword).WithMessage(Resources.PasswordNotMatch);
            this.RuleFor(m => m.Email).EmailAddress().WithMessage(Resources.IncorrectEmail);
        }
    }
}