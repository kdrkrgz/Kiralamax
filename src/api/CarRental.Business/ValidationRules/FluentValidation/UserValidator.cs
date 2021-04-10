using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Entities.Entities;
using FluentValidation;

namespace CarRental.Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<AppUser>
    {
        public UserValidator()
        {
            RuleFor(x => x.Customer).NotNull();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.FirstName).MinimumLength(2).NotEmpty().NotNull().Must(StartsWithA).WithMessage("Müşteri ismi \"A\" ile başlamalı");
            RuleFor(x => x.LastName).MinimumLength(2).NotEmpty().NotNull();
        }

        // Müşteri ismi "A" ile başlamalı
        private bool StartsWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
