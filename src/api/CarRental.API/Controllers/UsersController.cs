using AutoMapper;
using CarRental.Business.Abstract;
using CarRental.Core.Entities.Concrete;
using CarRental.Entities.Dtos;
using CarRental.Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(IUserService userService,  IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet("userdata/{email}")]
        public IActionResult Get(string email)
        {
            var result = _userService.GetByMail(email);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("{email}")]
        public IActionResult GetLoggedInUserDetails(string email)
        {
            var result = _userService.GetUserDetails(email);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _userService.GetUsers();
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("userclaims/{id}")]
        public IActionResult GetUserClaims(int id)
        {
            var result = _userService.GetClaimsByUserId(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Post(UserDataDto userData)
        {
            var user = _mapper.Map<AppUser>(userData);
            var customer = _mapper.Map<Customer>(userData);
            var result = _userService.UpdateUser(user, customer);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("updateuserclaims")]
        public IActionResult UpdateUserClaims(UserClaimUpdateDto userClaimData)
        {
            var result = _userService.UpdateUserClaims(userClaimData);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{userEmail}")]
        public IActionResult Delete(string userEmail)
        {
            var result = _userService.DeleteUser(userEmail);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
