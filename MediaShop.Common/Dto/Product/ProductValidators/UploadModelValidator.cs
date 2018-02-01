using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediaShop.Common.Properties;

namespace MediaShop.Common.Dto.Product.ProductValidators
{
    public class UploadModelValidator : AbstractValidator<UploadProductModel>
    {
        public UploadModelValidator()
        {
            this.RuleFor(v => v.ProductName).NotEmpty().WithMessage(Resources.EmptyFieldMessage);

            this.RuleFor(v => v.ProductName).MinimumLength(5).MaximumLength(50)
                .WithMessage(Resources.FailedEnteringProducName);

            this.RuleFor(v => v.Description).NotEmpty().WithMessage(Resources.EmptyFieldMessage);

            this.RuleFor(v => v.Description).MaximumLength(300).WithMessage(Resources.FailedEnteringDescription);

            this.RuleFor(v => v.ProductPrice).NotEmpty().WithMessage(Resources.EmptyFieldMessage);
        }
    }
}
