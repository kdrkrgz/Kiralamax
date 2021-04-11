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
            RuleFor(x => x.RentDate).NotNull().NotEmpty();
            RuleFor(x => x.ReturnDate).NotNull().NotEmpty();
            RuleFor(x => x.Car).NotNull().NotEmpty();
            RuleFor(x => x.Customer).NotNull().NotEmpty();
        }

        private bool CompareDates(DateTime rentDate, DateTime returnDate)
        {
            if (returnDate < rentDate)
            {
                return false;
            }

            return true;
        }
    }
}
