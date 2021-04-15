using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Exceptions;
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
            IUserService userService)
        {
            _userService = userService;
        }

        private readonly IUserService _userService;

        [HttpPost]
        public ActionResult<bool> Login(LoginDTO login)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return BadRequest("The entered data is not valid.");
                }

                _userService.GetByEmail(login.Email);

                return Ok(true);
            }
            catch(ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
