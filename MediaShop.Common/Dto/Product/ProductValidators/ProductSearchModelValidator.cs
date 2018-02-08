using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MediaShop.Common.Dto.Product.ProductValidators
{
    public class ProductSearchModelValidator : AbstractValidator<ProductSearchModel>
    {
        public ProductSearchModelValidator()
        {
            var availableValues = new string[] { ">", "<", "=", "Contains" };
            this.RuleFor(x => x.Operand).Must(s => availableValues.Contains(s)).WithMessage(x => $"Operand {x.Operand} is not available for comparison");
        }
    }
}
