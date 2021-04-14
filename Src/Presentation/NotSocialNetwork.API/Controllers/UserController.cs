using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Entities.Abstract;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Managers;
using NotSocialNetwork.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NotSocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet]
        public IEnumerable<UserEntity> Get()
        {
            return _userService.GetAll();
        }

        /// <summary>Checks if there is a user in the database with the appropriate parameters.</summary>
        /// <param name="userDTO">DTO of the user with data about him for authentication.</param>
        /// <returns>If the user was found - true, if he was not found or an error occurred while sending the request - false.</returns>
        [HttpPost("CheckUser")]
        public ActionResult<RegistrationUserDTO> GetByEmail(RegistrationUserDTO userDTO)
        {
            try
            {
                var usersFromDb = _userService.GetAll();

                var foundedUser = usersFromDb.FirstOrDefault(user => user.Email == userDTO.Email);

                if (foundedUser != default)
                {
                    return Ok(foundedUser.Id);
                }
                throw new ObjectNotFoundException("user didn't found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<UserEntity> Get(Guid id)
        {
            try
            {
                return _userService.GetById(id);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
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

                return Ok(user);
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

        [HttpPut]
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

        [HttpDelete]
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
