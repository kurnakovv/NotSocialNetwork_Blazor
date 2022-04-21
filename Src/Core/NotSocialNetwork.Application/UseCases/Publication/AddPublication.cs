using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Systems;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NotSocialNetwork.Application.UseCases.Publication
{
    public class AddPublication : IAddablePublicationAsync
    {
        public AddPublication(
            IGetablePublication getablePublication,
            IGetableUser getableUser,
            IRepositoryAsync<PublicationEntity> publicationRepository,
            IImageRepositorySystemAsync imageRepositorySystem)
        {
            _getablePublication = getablePublication;
            _getableUser = getableUser;
            _publicationRepository = publicationRepository;
            _imageRepositorySystem = imageRepositorySystem;
        }

        private readonly IGetablePublication _getablePublication;
        private readonly IGetableUser _getableUser;
        private readonly IRepositoryAsync<PublicationEntity> _publicationRepository;
        private readonly IImageRepositorySystemAsync _imageRepositorySystem;

        public async Task<PublicationEntity> AddAsync(PublicationEntity publication)
        {
            CheckPublicationIsValid(publication);

            await TrySaveImages(publication);
            await _publicationRepository.AddAsync(publication);

            return publication;
        }

        private void CheckPublicationIsValid(PublicationEntity publication)
        {
            if (IsPublicationAlreadyExist(publication) == true)
            {
                throw new ObjectAlreadyExistException($"Publication by Id: {publication.Id} already exists.");
            }

            CheckAuthorIsValid(publication.AuthorId);
        }

        private bool IsPublicationAlreadyExist(PublicationEntity publication)
        {
            if(_getablePublication.GetAll().Any(p => p.Id == publication.Id) == true)
            {
                return true;
            }

            return false;
        }

        private void CheckAuthorIsValid(Guid id)
        {
            _getableUser.GetById(id);
        }

        private bool IsPublicationContainImages(PublicationEntity publication)
        {
            if (publication.Images != null &&
                publication.Images.Count() != 0)
            {
                return true;
            }

            return false;
        }

        private async Task TrySaveImages(PublicationEntity publication)
        {
            if (IsPublicationContainImages(publication)) 
            {
                foreach (ImageEntity imageEntity in publication.Images)
                {
                    await _imageRepositorySystem.TrySaveAsync(imageEntity);
                }
            }
        }
    }
}
