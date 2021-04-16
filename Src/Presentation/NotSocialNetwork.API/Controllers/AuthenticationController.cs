using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Managers;
using NotSocialNetwork.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotSocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public AuthenticationController(
            IUserService userService,
            IJwtSystem jwtSystem)
        {
            _userService = userService;
            _jwtSystem = jwtSystem;
        }

        private readonly IUserService _userService;
        private readonly IJwtSystem _jwtSystem;

        [HttpPost]
        public ActionResult<string> Login(LoginDTO login)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return BadRequest("The entered data is not valid.");
                }

                var user = _userService.GetByEmail(login.Email);
                
                var token = _jwtSystem.GenerateToken(user);

                return Ok(token);
            }
            catch(ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
