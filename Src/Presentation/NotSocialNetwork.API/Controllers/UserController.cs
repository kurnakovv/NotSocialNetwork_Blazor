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
            IFileManager<ImageEntity> imageFileManager,
            IHostEnvironment hostEnvironment)
        {
            _userService = userService;
            _imageFileManager = imageFileManager;
            _hostEnvironment = hostEnvironment;
        }

        private readonly IUserService _userService;
        private readonly IFileManager<ImageEntity> _imageFileManager;
        private readonly IHostEnvironment _hostEnvironment;

        [HttpGet]
        public IEnumerable<UserEntity> Get()
        {
            return _userService.GetAll();
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
        public ActionResult<UserEntity> Add([FromForm] RegistrationUserDTO registrationUserDTO)
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

                    _imageFileManager.Save(user.Image, user.Image.ImageFromForm, _hostEnvironment.ContentRootPath);
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
