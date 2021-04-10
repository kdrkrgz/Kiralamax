using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.Results;

namespace CarRental.Core.Extensions
{
    public class ValidationErrorDetails: ErrorDetails
    {
        public IEnumerable<ValidationFailure> ValidationErrors { get; set; }

    }
}
