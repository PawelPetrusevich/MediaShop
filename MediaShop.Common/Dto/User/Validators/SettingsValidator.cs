using FluentValidation;

namespace MediaShop.Common.Dto.User.Validators
{
    public class SettingsValidator : AbstractValidator<SettingsDto>
    {
        public SettingsValidator()
        {
            RuleFor(m => m.UserLogin).NotEmpty().MinimumLength(5).WithMessage("Blablabla");
            RuleFor(m => m.TimeZoneId).NotEmpty().MinimumLength(3).WithMessage("Error time zone");
            RuleFor(m => m.InterfaceLanguage).NotEmpty().WithMessage("Error Language");
            RuleFor(m => m.NotificationStatus).NotEmpty().WithMessage("Error notification");
        }
    }
}