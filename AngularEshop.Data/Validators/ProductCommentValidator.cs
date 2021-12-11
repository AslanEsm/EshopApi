using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularEshop.Entities.Product;
using FluentValidation;

namespace AngularEshop.Data.Validators
{
    public class ProductCommentValidator : AbstractValidator<ProductComment>
    {
        public ProductCommentValidator()
        {
            RuleFor(p => p.Text)
                .NotEmpty().WithMessage("پر کردن {PropertyName} اجباری است")
                .MaximumLength(1000).WithMessage(" باید حداکثر {MaxLength} کاراکتر داشته باشد. {PropertyName}");
        }
    }
}
