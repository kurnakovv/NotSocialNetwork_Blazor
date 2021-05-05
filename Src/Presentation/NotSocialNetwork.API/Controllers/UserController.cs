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
    [JwtAuthorize]
    public class UserController : ControllerBase
    {
        public UserController(
            IUserService userService,
            IFileSystem<ImageEntity> imageFileSystem,
            IHostEnvironment hostEnvironment)
        {
            _userService = userService;
            _imageFileSystem = imageFileSystem;
            _hostEnvironment = hostEnvironment;
        }

        private readonly IUserService _userService;
        private readonly IFileSystem<ImageEntity> _imageFileSystem;
        private readonly IHostEnvironment _hostEnvironment;

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>Users.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserEntity>), StatusCodes.Status200OK)]
        [SwaggerOperation(
            Summary = "Get all.",
            Description = "Get all users."
        )]
        public ActionResult<IEnumerable<UserEntity>> Get()
        {
            return Ok(_userService.GetAll());
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>User.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Get by id.",
            Description = "Get user by id."
        )]
        public ActionResult<UserEntity> Get(Guid id)
        {
            try
            {
                return Ok(_userService.GetById(id));
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
        [ProducesResponseType(typeof(RegistrationUserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Add.",
            Description = "Add user."
        )]
        public ActionResult<RegistrationUserDTO> Add(RegistrationUserDTO registrationUserDTO)
        {
            try
            {
                // TODO: Add mapping.
                var image = new ImageEntity();
                var user = new UserEntity();

                if (registrationUserDTO.Files != null)
                {
                    image.ImageFromForm = registrationUserDTO.Files;
                    image.Title = registrationUserDTO.Files.FileName;

                    user.Name = registrationUserDTO.Name;
                    user.Email = registrationUserDTO.Email;
                    user.DateOfBirth = registrationUserDTO.DateOfBirth;
                    user.Image = image;

                    _imageFileSystem.Save(user.Image, _hostEnvironment.ContentRootPath);
                }
                else
                {
                    user.Name = registrationUserDTO.Name;
                    user.Email = registrationUserDTO.Email;
                    user.DateOfBirth = registrationUserDTO.DateOfBirth;
                    user.Image = null;
                }

                _userService.Add(user);

                return Ok(registrationUserDTO);
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
        [ProducesResponseType(typeof(UserEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Update.",
            Description = "Update user."
        )]
        public ActionResult<UserEntity> Update(UserEntity user)
        {
            try
            {
                _userService.Update(user);

                return Ok(user);
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
        [ProducesResponseType(typeof(UserEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Delete by id.",
            Description = "Delete user by id."
        )]
        public ActionResult<UserEntity> Delete(Guid id)
        {
            try
            {
                var user = _userService.Delete(id);

                return Ok(user);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
