using NotSocialNetwork.Application.Configs;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Systems;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NotSocialNetwork.Services.Systems
{
    public class ImageRepositorySystem : IImageRepositorySystemAsync
    {
        public ImageRepositorySystem(
               IRepositoryAsync<ImageEntity> imageRepository)
        {
            _imageRepository = imageRepository;
        }

        private readonly IRepositoryAsync<ImageEntity> _imageRepository;

        public async Task<ImageEntity> GetAsync(Guid id)
        {
            var image = await _imageRepository.GetAsync(id);

            if (image == null)
            {
                throw new ObjectNotFoundException($"Image by id: {id} not found!");
            }

            return image;
        }

        public async Task<ImageEntity> TrySaveAsync(ImageEntity image)
        {
            if (image == null)
            {
                var defaultImage = await GetAsync(DefaultImageConfig.DEFAULT_IMAGE_ID);

                return defaultImage;
            }

            IsImageAlreadyExist(image);

            await _imageRepository.AddAsync(image);

            return image;
        }

        public async Task<Guid> TryUpdateAsync(UpdateFileDTO updateFile)
        {
            if (updateFile.NewFile == null)
            {
                return updateFile.OldFile.Id;
            }

            await TryDeleteAsync(updateFile.OldFile.Id);
            await TrySaveAsync(updateFile.NewFile as ImageEntity);

            return updateFile.NewFile.Id;
        }

        public async Task<Guid> TryDeleteAsync(Guid id)
        {
            await _imageRepository.DeleteAsync(id);

            return id;
        }

        private void IsImageAlreadyExist(ImageEntity image)
        {
            var isContainImage = _imageRepository.GetAll().Any(i => i.Id == image.Id);

            if (isContainImage == true)
            {
                throw new ObjectAlreadyExistException($"Image by id: {image.Id} already exist.");
            }
        }
    }
}
