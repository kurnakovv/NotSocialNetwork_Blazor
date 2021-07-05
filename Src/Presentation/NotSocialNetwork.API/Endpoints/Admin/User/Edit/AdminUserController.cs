using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotSocialNetwork.Application.Configs;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace NotSocialNetwork.API.Endpoints.Admin.User.Edit
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleConfig.ADMINISTRATOR)]
    public class AdminUserController : ControllerBase
    {
        public AdminUserController(
            IEditableUserAsync editableUser,
            IMapper mapper)
        {
            _editableUser = editableUser;
            _mapper = mapper;
        }

        private readonly IEditableUserAsync _editableUser;
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
        public async Task<ActionResult<UserDTO>> Update(UserEntity user)
        {
            try
            {
                await _editableUser.UpdateAsync(user);

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
        public async Task<ActionResult<UserDTO>> Delete(Guid id)
        {
            try
            {
                var user = await _editableUser.DeleteAsync(id);

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
