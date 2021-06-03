using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Facades;
using NotSocialNetwork.Application.Interfaces.Systems;
using System;
using System.Threading.Tasks;

namespace NotSocialNetwork.Services.Facades
{
    public class ImageFacade : IFileFacadeAsync<ImageEntity>
    {
        public ImageFacade(
            IImageFileSystemAsync imageFileSystem,
            IImageRepositorySystemAsync imageRepositorySystem)
        {
            _imageFileSystem = imageFileSystem;
            _imageRepositorySystem = imageRepositorySystem;
        }

        private readonly IImageFileSystemAsync _imageFileSystem;
        private readonly IImageRepositorySystemAsync _imageRepositorySystem;


        public async Task<ImageEntity> GetAsync(Guid id)
        {
            var image = await _imageRepositorySystem.GetAsync(id);

            return image;
        }

        public async Task<Guid> SaveAsync(ImageEntity file, string pathToSave)
        {
            await _imageFileSystem.TrySaveAsync(file, pathToSave);
            var image = await _imageRepositorySystem.TrySaveAsync(file);

            return image.Id;
        }

        public async Task<Guid> UpdateAsync(UpdateFileDTO updateFile)
        {
            await _imageFileSystem.TryUpdateAsync(updateFile);
            await _imageRepositorySystem.TryUpdateAsync(updateFile);

            return updateFile.NewFile.Id;
        }

        public async Task<Guid> DeleteAsync(Guid id, string filePath)
        {
            await _imageFileSystem.DeleteAsync(id, filePath);
            await _imageRepositorySystem.TryDeleteAsync(id);

            return id;
        }
    }
}
