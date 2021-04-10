using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Business.Abstract;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : ControllerBase
    {
        private readonly IOperationClaimService _claimService;
        public OperationClaimsController(IOperationClaimService claimService)
        {
            _claimService = claimService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _claimService.GetClaims();
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
