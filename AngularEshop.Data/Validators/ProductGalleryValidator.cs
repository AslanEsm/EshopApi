using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularEshop.Entities.Product;
using FluentValidation;

namespace AngularEshop.Data.Validators
{
    public class ProductGalleryValidator : AbstractValidator<ProductGallery>
    {
        public ProductGalleryValidator()
        {
            RuleFor(p => p.ImageName)
                .NotEmpty().WithMessage("پر کردن {PropertyName} اجباری است")
                .MaximumLength(100).WithMessage(" باید حداکثر {MaxLength} کاراکتر داشته باشد. {PropertyName}");
        }
    }
}
