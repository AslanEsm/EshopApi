using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularEshop.Entities.Site;
using FluentValidation;

namespace AngularEshop.Data.Validators
{
    public class SliderValidator : AbstractValidator<Slider>
    {
        public SliderValidator()
        {
            RuleFor(p => p.ImageName)
                .NotEmpty().WithMessage("پر کردن {PropertyName} اجباری است")
                .MaximumLength(150).WithMessage(" باید حداکثر {MaxLength} کاراکتر داشته باشد. {PropertyName}");

            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("پر کردن {PropertyName} اجباری است")
                .MaximumLength(500).WithMessage(" باید حداکثر {MaxLength} کاراکتر داشته باشد. {PropertyName}");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("پر کردن {PropertyName} اجباری است")
                .MaximumLength(1000).WithMessage(" باید حداکثر {MaxLength} کاراکتر داشته باشد. {PropertyName}");

            RuleFor(p => p.Link)
                .NotEmpty().WithMessage("پر کردن {PropertyName} اجباری است")
                .MaximumLength(150).WithMessage(" باید حداکثر {MaxLength} کاراکتر داشته باشد. {PropertyName}");
        }
    }
}
