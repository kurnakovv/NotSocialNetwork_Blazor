using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Managers;
using NotSocialNetwork.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace NotSocialNetwork.API.Controllers
{
    [Route("api/authentication")]
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
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Login.",
            Description = "Login in system."
        )]
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
