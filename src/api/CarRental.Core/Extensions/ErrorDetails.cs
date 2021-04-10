using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace CarRental.Core.Extensions
{
    // Error Details model and serialize error message w/ToString() method override
    public class ErrorDetails
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
