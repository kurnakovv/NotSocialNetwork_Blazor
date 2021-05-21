using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Facades;
using NotSocialNetwork.Application.Interfaces.Systems;
using System;

namespace NotSocialNetwork.Services.Facades
{
    public class ImageFacade : IFileFacade<ImageEntity>
    {
        public ImageFacade(
            IImageFileSystem imageFileSystem,
            IImageRepositorySystem imageRepositorySystem)
        {
            _imageFileSystem = imageFileSystem;
            _imageRepositorySystem = imageRepositorySystem;
        }

        private readonly IImageFileSystem _imageFileSystem;
        private readonly IImageRepositorySystem _imageRepositorySystem;


        public ImageEntity Get(Guid id)
        {
            var image = _imageRepositorySystem.Get(id);

            return image;
        }

        public Guid Save(ImageEntity file, string pathToSave)
        {
            _imageFileSystem.TrySave(file, pathToSave);
            var image = _imageRepositorySystem.TrySave(file);

            return image.Id;
        }

        public Guid Update(UpdateFileDTO updateFile)
        {
            _imageFileSystem.TryUpdate(updateFile);
            _imageRepositorySystem.TryUpdate(updateFile);

            return updateFile.NewFile.Id;
        }

        public Guid Delete(Guid id, string filePath)
        {
            _imageFileSystem.Delete(id, filePath);
            _imageRepositorySystem.TryDelete(id);

            return id;
        }
    }
}
