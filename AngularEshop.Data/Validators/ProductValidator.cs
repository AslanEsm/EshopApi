using AngularEshop.Entities.Product;
using FluentValidation;

namespace AngularEshop.Data.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("پر کردن {PropertyName} اجباری است")
                .MaximumLength(100).WithMessage(" باید حداکثر {MaxLength} کاراکتر داشته باشد. {PropertyName}");

            RuleFor(p => p.ShortDescription)
                .NotEmpty().WithMessage("پر کردن {PropertyName} اجباری است")
                .MaximumLength(500).WithMessage(" باید حداکثر {MaxLength} کاراکتر داشته باشد. {PropertyName}");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("پر کردن {PropertyName} اجباری است");

            RuleFor(p => p.ImageName)
                .NotEmpty().WithMessage("پر کردن {PropertyName} اجباری است")
                .MaximumLength(150).WithMessage(" باید حداکثر {MaxLength} کاراکتر داشته باشد. {PropertyName}");
        }
    }
}