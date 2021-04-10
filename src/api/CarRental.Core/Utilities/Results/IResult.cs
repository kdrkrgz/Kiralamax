using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CarRental.Core.Utilities
{
    public interface IResult
    {
        bool IsSuccess { get; }
        string Message { get; }
    }
}
