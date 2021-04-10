using CarRental.Core.Utilities.Results;
using CarRental.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Controllers
{
    // Fake Payment Controller
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Pay(CreditCardDto creditCardDto)
        {
            var result = GetPaymentResult(creditCardDto.CardNumber);
            if (result)
            {
                return Ok(new SuccessResult("Ödeme başarıyla gerçekleştirildi."));
            }
            return BadRequest(new ErrorResult("Ödeme gerçekleştirilemedi lütfen daha sonra tekrar deneyiniz."));
        }

        private bool GetPaymentResult(string creditCardNumber)
        {
            if (creditCardNumber.EndsWith("1"))
            {
                return false;
            }
            return true;
        }
    }

}
