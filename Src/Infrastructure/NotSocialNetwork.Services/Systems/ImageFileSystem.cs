using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Systems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NotSocialNetwork.Services.Systems
{
    public class ImageFileSystem : IImageFileSystemAsync
    {
        public ImageFileSystem(
            IRepositoryAsync<ImageEntity> imageRepository)
        {
            _imageRepository = imageRepository;
        }

        private readonly IRepositoryAsync<ImageEntity> _imageRepository;
        private readonly ICollection<string> _imageExtensions
            = new List<string> { ".jpg", ".png", ".jpeg", ".gif" };

        public async Task TrySaveAsync(ImageEntity file, string pathToSave)
        {
            if (file == null)
            {
                return;
            }

            file.Title = file.ImageFromForm.FileName;
            await SaveFileToFolder(file, pathToSave);
        }

        public async Task<Guid> DeleteAsync(Guid id, string filePath)
        {
            var file = await _imageRepository.GetAsync(id);

            DeleteFileFromFolder(file.Title, filePath);

            return file.Id;
        }

        public async Task TryUpdateAsync(UpdateFileDTO updateFile)
        {
            if (updateFile == null)
            {
                return;
            }

            await DeleteAsync(updateFile.OldFile.Id, updateFile.FilePath);
            await TrySaveAsync((ImageEntity)updateFile.NewFile, updateFile.FilePath);
        }

        private async Task SaveFileToFolder(ImageEntity file, string pathToSave)
        {
            var fileExtension = Path.GetExtension(file.Title);
            if (IsValidFormat(fileExtension) == false)
            {
                throw new InvalidFileFormatException("The file contains the wrong format.");
            }

            var uniqueFileName = Convert.ToString(Guid.NewGuid());

            file.Title = DeleteSpaces(file);

            var newFileTitle = uniqueFileName + fileExtension;

            file.Title = pathToSave + "\\wwwroot\\userImages" + $@"\{newFileTitle}";

            using (FileStream fs = File.Create(file.Title))
            {
                await file.ImageFromForm.CopyToAsync(fs);
                await fs.FlushAsync();
            }

            file.Title = newFileTitle;
        }

        private bool IsValidFormat(string fileExtension)
        {
            if (_imageExtensions.Contains(fileExtension))
            {
                return true;
            }

            return false;
        }

        private string DeleteSpaces(ImageEntity file)
        {
            var titleWithDeletedSpaces = ContentDispositionHeaderValue.Parse(file.ImageFromForm.ContentDisposition).FileName.Trim('"');

            return titleWithDeletedSpaces;
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
