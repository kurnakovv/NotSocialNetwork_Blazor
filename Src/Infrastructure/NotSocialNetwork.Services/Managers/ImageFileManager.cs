using Microsoft.AspNetCore.Http;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Managers;
using NotSocialNetwork.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace NotSocialNetwork.Services.Managers
{
    public class ImageFileManager : IFileManager<ImageEntity>
    {
        public ImageFileManager(
            IRepository<ImageEntity> imageRepository)
        {
            _imageRepository = imageRepository;
        }

        private readonly IRepository<ImageEntity> _imageRepository;
        private static ICollection<string> _imageExtensions 
            = new List<string> { ".jpg", ".png", ".jpeg", ".gif" };

        public ImageEntity Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid Save(ImageEntity file, IFormFile fileFromForm, string pathToSave)
        {
            file.Title = fileFromForm.FileName;
            SaveFileToFolder(file, fileFromForm, pathToSave);

            SaveFilePath(file);

            return file.Id;
        }
        
        public Guid Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid Update(ImageEntity file, IFormFile fileFromForm)
        {
            throw new NotImplementedException();
        }

        private bool IsImageConteinFormat(string title)
        {
            var fileExtension = Path.GetExtension(title);

            if(_imageExtensions.Contains(fileExtension))
            {
                return true;
            }

            return false;
        }

        private void SaveFilePath(ImageEntity file)
        {
            var isContainImage = _imageRepository.GetAll().Any(i => i.Id == file.Id);

            if (isContainImage == true)
            {
                throw new ObjectAlreadyExistException($"Image by id: {file.Id} already exist.");
            }

            _imageRepository.Add(file);
            _imageRepository.Commit();
        }

        private void SaveFileToFolder(ImageEntity file, IFormFile fileFromForm, string pathToSave)
        {
            var newFileTitle = string.Empty;
            var fileExtension = Path.GetExtension(file.Title);
            var uniqueFileName = Convert.ToString(Guid.NewGuid());

            file.Title = ContentDispositionHeaderValue.Parse(fileFromForm.ContentDisposition).FileName.Trim('"');

            if (IsImageConteinFormat(file.Title) == false)
            {
                throw new InvalidFileFormatException("The file contains the wrong format");
            }

            newFileTitle = uniqueFileName + fileExtension;

            file.Title = pathToSave + "\\wwwroot\\userImages" + $@"\{newFileTitle}";

            using (FileStream fs = File.Create(file.Title))
            {
                fileFromForm.CopyTo(fs);
                fs.Flush();
            }

            file.Title = newFileTitle;
        }
    }
}
