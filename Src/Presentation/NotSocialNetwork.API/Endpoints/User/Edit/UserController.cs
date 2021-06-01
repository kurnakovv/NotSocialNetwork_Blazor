using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace NotSocialNetwork.API.Endpoints.User.Edit
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        public UserController(
            IEditableUser editableUser,
            IMapper mapper)
        {
            _editableUser = editableUser;
            _mapper = mapper;
        }

        private readonly IEditableUser _editableUser;
        private readonly IMapper _mapper;

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
        public ActionResult<UserDTO> Update(UserEntity user)
        {
            try
            {
                _editableUser.Update(user);

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
        public ActionResult<UserDTO> Delete(Guid id)
        {
            try
            {
                var user = _editableUser.Delete(id);

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
