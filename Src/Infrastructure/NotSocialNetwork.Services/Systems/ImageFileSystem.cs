using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Systems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;

namespace NotSocialNetwork.Services.Systems
{
    public class ImageFileSystem : IImageFileSystem
    {
        public ImageFileSystem(
            IRepositoryAsync<ImageEntity> imageRepository)
        {
            _imageRepository = imageRepository;
        }

        private readonly IRepositoryAsync<ImageEntity> _imageRepository;
        private static ICollection<string> _imageExtensions
            = new List<string> { ".jpg", ".png", ".jpeg", ".gif" };

        public void TrySave(ImageEntity file, string pathToSave)
        {
            if (file == null)
            {
                return;
            }

            file.Title = file.ImageFromForm.FileName;
            SaveFileToFolder(file, pathToSave);
        }

        public Guid Delete(Guid id, string filePath)
        {
            var file = _imageRepository.GetAsync(id);

            DeleteFileFromFolder(file.Result.Title, filePath);

            return file.Result.Id;
        }

        public void TryUpdate(UpdateFileDTO updateFile)
        {
            if (updateFile == null)
            {
                return;
            }

            Delete(updateFile.OldFile.Id, updateFile.FilePath);
            TrySave((ImageEntity)updateFile.NewFile, updateFile.FilePath);
        }

        private bool IsImageContainFormat(string title)
        {
            var fileExtension = Path.GetExtension(title);

            if (_imageExtensions.Contains(fileExtension))
            {
                return true;
            }

            return false;
        }

        private void SaveFileToFolder(ImageEntity file, string pathToSave)
        {
            var fileExtension = Path.GetExtension(file.Title);
            var uniqueFileName = Convert.ToString(Guid.NewGuid());

            // Delete spaces.
            file.Title = ContentDispositionHeaderValue.Parse(file.ImageFromForm.ContentDisposition).FileName.Trim('"');

            if (IsImageContainFormat(file.Title) == false)
            {
                throw new InvalidFileFormatException("The file contains the wrong format");
            }

            var newFileTitle = uniqueFileName + fileExtension;

            file.Title = pathToSave + "\\wwwroot\\userImages" + $@"\{newFileTitle}";

            using (FileStream fs = File.Create(file.Title))
            {
                file.ImageFromForm.CopyTo(fs);
                fs.Flush();
            }

            file.Title = newFileTitle;
        }

        private void DeleteFileFromFolder(string title, string filePath)
        {
            var fullPathToFile = filePath + "\\wwwroot\\userImages" + $@"\{title}";
            if (File.Exists(fullPathToFile) == false)
            {
                throw new ObjectNotFoundException($"Image by title {title} not found.");
            }

            File.Delete(fullPathToFile);
        }
    }
}
