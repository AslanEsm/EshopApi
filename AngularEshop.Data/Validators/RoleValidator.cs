using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularEshop.Entities.Access;
using FluentValidation;

namespace AngularEshop.Data.Validators
{
    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("پر کردن {PropertyName} اجباری است")
                .MaximumLength(100).WithMessage(" باید حداکثر {MaxLength} کاراکتر داشته باشد. {PropertyName}");

            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("پر کردن {PropertyName} اجباری است")
                .MaximumLength(100).WithMessage(" باید حداکثر {MaxLength} کاراکتر داشته باشد. {PropertyName}");


            ;
        }
    }
}
