using NotSocialNetwork.Application.Configs;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Systems;
using System;
using System.Linq;

namespace NotSocialNetwork.Services.Systems
{
    public class ImageRepositorySystem : IImageRepositorySystem
    {
        public ImageRepositorySystem(
               IRepositoryAsync<ImageEntity> imageRepository)
        {
            _imageRepository = imageRepository;
        }

        private readonly IRepositoryAsync<ImageEntity> _imageRepository;

        public ImageEntity Get(Guid id)
        {
            var image = _imageRepository.GetAsync(id);

            if (image == null)
            {
                throw new ObjectNotFoundException($"Image by id: {id} not found!");
            }

            return image.Result;
        }

        public ImageEntity TrySave(ImageEntity image)
        {
            if (image == null)
            {
                var defaultImage = Get(DefaultImageConfig.DEFAULT_IMAGE_ID);

                return defaultImage;
            }

            IsImageAlreadyExist(image);

            _imageRepository.AddAsync(image);

            return image;
        }

        public Guid TryUpdate(UpdateFileDTO updateFile)
        {
            if (updateFile.NewFile == null)
            {
                return updateFile.OldFile.Id;
            }

            TryDelete(updateFile.OldFile.Id);
            TrySave(updateFile.NewFile as ImageEntity);

            return updateFile.NewFile.Id;
        }

        public Guid TryDelete(Guid id)
        {
            _imageRepository.DeleteAsync(id);

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
