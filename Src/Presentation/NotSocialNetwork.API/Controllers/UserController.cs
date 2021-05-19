using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NotSocialNetwork.API.Attributes;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Managers;
using NotSocialNetwork.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace NotSocialNetwork.API.Controllers
{
    /// <summary>
    /// User.
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Get all users by pagination.
        /// </summary>
        /// <returns>Users.</returns>
        [HttpGet("index={index}")]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Get all by pagination.",
            Description = "Get all users by pagination."
        )]
        [JwtAuthorize]
        public ActionResult<IEnumerable<UserDTO>> Get(int index = 0)
        {
            try
            {
                var usersEntity = _userService.GetByPagination(index);

                var usersDTO = _mapper.Map<IEnumerable<UserDTO>>(usersEntity);

                return Ok(usersDTO);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>User.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Get by id.",
            Description = "Get user by id."
        )]
        [JwtAuthorize]
        public ActionResult<UserDTO> Get(Guid id)
        {
            try
            {
                var userEntity = _userService.GetById(id);

                var userDTO = _mapper.Map<UserDTO>(userEntity);

                return Ok(userDTO);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Add user.
        /// </summary>
        /// <param name="registrationUserDTO">User parameters.</param>
        /// <returns>User.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Add.",
            Description = "Add user."
        )]
        public ActionResult<UserDTO> Add(RegistrationUserDTO registrationUserDTO)
        {
            try
            {
                var userEntity = _mapper.Map<UserEntity>(registrationUserDTO);

                _userService.Add(userEntity);

                var userDTO = _mapper.Map<UserDTO>(userEntity);

                return Ok(userDTO);
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

        /// <summary>
        /// Update user.
        /// </summary>
        /// <param name="user">User parameters.</param>
        /// <returns>User.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Update.",
            Description = "Update user."
        )]
        [JwtAuthorize]
        public ActionResult<UserDTO> Update(UserEntity user)
        {
            try
            {
                _userService.Update(user);

                var userDTO = _mapper.Map<UserDTO>(user);

                return Ok(userDTO);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Delete user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>User.</returns>
        [HttpDelete]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Delete by id.",
            Description = "Delete user by id."
        )]
        [JwtAuthorize]
        public ActionResult<UserDTO> Delete(Guid id)
        {
            try
            {
                var user = _userService.Delete(id);

                var userDTO = _mapper.Map<UserDTO>(user);

                return Ok(userDTO);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
