using System;
using System.Linq;
using System.Text.RegularExpressions;
using FluentValidation;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Properties;

namespace MediaShop.Common.Dto.User.Validators
{
    public class ProfileValidator : AbstractValidator<ProfileDto>
    {
        public ProfileValidator()
        {
            this.RuleFor(m => m.FirstName).NotEmpty().MinimumLength(2).WithMessage(Resources.IncorrectFirstName);
            this.RuleFor(m => m.LastName).NotEmpty().MinimumLength(2).WithMessage(Resources.IncorrectLastName);
            this.RuleFor(m => m.DateOfBirth).NotNull().WithMessage(Resources.IncorrectDateValue);
            this.RuleFor(m => m.Phone).NotEmpty().Must(this.CheckPhoneNumber)
                .WithMessage(Resources.IncorrectPhoneformat);
        }

        private bool CheckPhoneNumber(string phone)
        {
            Regex regex = new Regex(Resources.RegExpressionPhone);
            var result = regex.Match(phone);

            return result.Success;
        }
    }
}
