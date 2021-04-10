using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Utilities.Email
{
    public interface IEMailService
    {
        Task Send(string subject, string body, string toEmail);
    }
}
