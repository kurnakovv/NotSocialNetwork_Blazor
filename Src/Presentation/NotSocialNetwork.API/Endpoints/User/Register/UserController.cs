using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Systems;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using Swashbuckle.AspNetCore.Annotations;

namespace NotSocialNetwork.API.Endpoints.User.Register
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        public UserController(
            IAddableUser addableUser,
            IMapper mapper,
            IJwtSystem jwtSystem)
        {
            _addableUser = addableUser;
            _mapper = mapper;
            _jwtSystem = jwtSystem;
        }

        private readonly IAddableUser _addableUser;
        private readonly IMapper _mapper;
        private readonly IJwtSystem _jwtSystem;

        /// <summary>
        /// Register user.
        /// </summary>
        /// <param name="registrationUserDTO">User parameters.</param>
        /// <returns>User.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Register.",
            Description = "Register user."
        )]
        public ActionResult<RegistrationResponseDTO> Register(RegistrationUserDTO registrationUserDTO)
        {
            try
            {
                var userEntity = _mapper.Map<UserEntity>(registrationUserDTO);

                _addableUser.Add(userEntity);

                var registrationResponseDTO = _mapper.Map<RegistrationResponseDTO>(userEntity);

                var token = _jwtSystem.GenerateToken(userEntity);
                registrationResponseDTO.Token = token;

                return Ok(registrationResponseDTO);
            }
            catch (InvalidFileFormatException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ObjectAlreadyExistException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
