using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediaShop.Common.Properties;

namespace MediaShop.Common.Dto.Messaging.Validators
{
    public class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordDtoValidator()
        {
            this.RuleFor(n => n).NotNull().WithMessage(Resources.NullOrEmptyValue);
            this.RuleFor(n => n.Email).NotNull().WithMessage(Resources.NullOrEmptyValueString).NotEmpty().WithMessage(Resources.NullOrEmptyValueString);
        }
    }
}
