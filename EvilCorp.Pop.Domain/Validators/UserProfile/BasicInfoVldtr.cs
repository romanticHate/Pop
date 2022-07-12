using EvilCorp.Pop.Domain.Aggregates.UserProfile;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCorp.Pop.Domain.Validators.UserProfile
{
    public  class BasicInfoVldtr:AbstractValidator<BasicInfo>
    {
        public BasicInfoVldtr()
        {
            RuleFor(info => info.FirstName)
           .NotNull().WithMessage("First name is required. It is currently null")
           .MaximumLength(15).WithMessage("First name can contain at most 15 characters long")
           .MinimumLength(3).WithMessage("First name must be at least 3 characters long");

            RuleFor(info => info.LastName)
            .NotNull().WithMessage("Last name is required. It is currently null")
            .MaximumLength(15).WithMessage("Last name can contain at most 15 characters long")
            .MinimumLength(3).WithMessage("Last name must be at least 3 characters long");

            RuleFor(info => info.EmailAddress)
            .NotNull().WithMessage("Email address is required")
            .EmailAddress().WithMessage("Provided string is not a correct email address format");

            RuleFor(info => info.DateOfBirth)
            .InclusiveBetween(new DateTime(DateTime.Now.AddYears(-100).Ticks),
            new DateTime(DateTime.Now.AddYears(-18).Ticks))
            .WithMessage("Age needs to be between 18 and 100");
        }
    }
}
