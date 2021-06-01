using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Systems;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using Swashbuckle.AspNetCore.Annotations;

namespace NotSocialNetwork.API.Endpoints.Authentication.Login
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public AuthenticationController(
            IJwtSystem jwtSystem,
            IGetableUser getableUser)
        {
            _jwtSystem = jwtSystem;
            _getableUser = getableUser;
        }

        private readonly IJwtSystem _jwtSystem;
        private readonly IGetableUser _getableUser;

        /// <summary>
        /// Login to the system.
        /// </summary>
        /// <param name="login">Login parameters.</param>
        /// <returns>Login result.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(LoginResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Login.",
            Description = "Login to the system."
        )]
        public ActionResult<LoginResult> Login(LoginDTO login)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return BadRequest("The entered data is not valid.");
                }

                var user = _getableUser.GetByEmail(login.Email);

                var token = _jwtSystem.GenerateToken(user);

                var result = new LoginResult()
                {
                    Token = token,
                    UserId = user.Id,
                };

                return Ok(result);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
