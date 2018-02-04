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
            //this.RuleFor(x => x.LeftValue).MinimumLength(2);
            //this.RuleFor(v => v.RightValue).NotEmpty();
        }
    }
}
