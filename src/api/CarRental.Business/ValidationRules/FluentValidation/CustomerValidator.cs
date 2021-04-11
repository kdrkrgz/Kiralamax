using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Entities.Entities;
using FluentValidation;

namespace CarRental.Business.ValidationRules.FluentValidation
{
    public class CustomerValidator: AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.AppUser).NotNull();
            //RuleFor(x => x.CompanyName).MinimumLength(2).NotEmpty();
        }
    }
}
