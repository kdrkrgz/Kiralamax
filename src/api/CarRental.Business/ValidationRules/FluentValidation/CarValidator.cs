using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Entities.Entities;
using FluentValidation;

namespace CarRental.Business.ValidationRules.FluentValidation
{
    public class CarValidator: AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(x => x.Brand).NotNull().NotEmpty();
            RuleFor(x => x.CarModel).NotNull().NotEmpty();
            RuleFor(x => x.Category).NotNull().NotEmpty();
            RuleFor(x => x.Color).NotNull().NotEmpty();
            RuleFor(x => x.DailyPrice).NotNull().NotEmpty();
            RuleFor(x => x.EngineCapacity).NotNull().NotEmpty();
            RuleFor(x => x.FuelType).NotNull().NotEmpty();
            RuleFor(x => x.Gear).NotNull().NotEmpty();
            RuleFor(x => x.MinCreditScore).NotNull().NotEmpty();
            RuleFor(x => x.ModelYear).NotNull().NotEmpty();
        }
    }
}
