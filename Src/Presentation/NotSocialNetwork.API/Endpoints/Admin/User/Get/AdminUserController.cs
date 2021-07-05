using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotSocialNetwork.Application.Configs;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace NotSocialNetwork.API.Endpoints.Admin.User.Get
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleConfig.ADMINISTRATOR)]
    public class AdminUserController : ControllerBase
    {
        public AdminUserController(
            IGetableUser getableUser,
            IMapper mapper)
        {
            _getableUser = getableUser;
            _mapper = mapper;
        }

        private readonly IGetableUser _getableUser;
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
        public ActionResult<IEnumerable<UserDTO>> Get(int index = 0)
        {
            try
            {
                var usersEntity = _getableUser.GetByPagination(index);

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
        public ActionResult<UserDTO> Get(Guid id)
        {
            try
            {
                var userEntity = _getableUser.GetById(id);

                var userDTO = _mapper.Map<UserDTO>(userEntity);

                return Ok(userDTO);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
