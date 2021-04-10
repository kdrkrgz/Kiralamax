using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Entities.Entities;
using FluentValidation;

namespace CarRental.Business.ValidationRules.FluentValidation
{
    public class RentalValidation:AbstractValidator<Rental>
    {
        public RentalValidation()
        {
            RuleFor(x => x.ReturnDate);
        }
        // TODO: Validations Ruleslar yazılmaya devam edilecek
    }
}
