using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Business.Abstract;
using CarRental.Entities.Entities;
using Org.BouncyCastle.Bcpg;
using CarRental.Entities.Dtos;
using AutoMapper;

namespace CarRental.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalManager;
        private readonly IMapper _mapper;
        public RentalsController(IRentalService rentalManager, IMapper mapper)
        {
            _rentalManager = rentalManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _rentalManager.GetRentalsWithDetails();
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _rentalManager.GetRentalWithDetails(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        
        [HttpPost]
        public IActionResult Add([FromForm]AppUserDto rentalWithCreditCardDto)
        {
   
            var rental = _mapper.Map<Rental>(rentalWithCreditCardDto);
            var creditCard = _mapper.Map<CreditCard>(rentalWithCreditCardDto);
            
            var result = _rentalManager.AddRentalWithPayment(rental, creditCard, rentalWithCreditCardDto.willBeRecorded);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _rentalManager.DeleteRentalById(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("complete/{id}")]
        public IActionResult CarRentComplete(int id)
        {
            var result = _rentalManager.CompleteRentalByCarId(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
