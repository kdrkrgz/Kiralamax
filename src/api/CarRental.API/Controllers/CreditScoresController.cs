using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Core.Utilities;
using CarRental.Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    // Fake Customer Credit Score Controller
    [ApiController]
    [Route("api/[controller]")]
    public class CreditScoresController: Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var score = RandomCreditScoreGenerator(0, 1900);
            var creditScore = new CustomerCreditScore
            {
                CreditScore = score
            };
            IDataResult<CustomerCreditScore> creditScoreReturn = new SuccessDataResult<CustomerCreditScore>(creditScore,"Müşreti findeks puanı başarıyla getirildi.");
            return Ok(creditScoreReturn);
        }

        private int RandomCreditScoreGenerator(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }


    }

    public class CustomerCreditScore
    {
        public int CreditScore { get; set; }
    }
}
