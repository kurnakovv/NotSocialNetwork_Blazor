using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Systems;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using System;
using System.Linq;

namespace NotSocialNetwork.Application.UseCases.Publication
{
    public class AddPublication : IAddablePublication
    {
        public AddPublication(
            IGetablePublication getablePublication,
            IGetableUser getableUser,
            IRepository<PublicationEntity> publicationRepository,
            IImageRepositorySystem imageRepositorySystem)
        {
            _getablePublication = getablePublication;
            _getableUser = getableUser;
            _publicationRepository = publicationRepository;
            _imageRepositorySystem = imageRepositorySystem;
        }

        private readonly IGetablePublication _getablePublication;
        private readonly IGetableUser _getableUser;
        private readonly IRepository<PublicationEntity> _publicationRepository;
        private readonly IImageRepositorySystem _imageRepositorySystem;

        public PublicationEntity Add(PublicationEntity publication)
        {
            if (_getablePublication.GetAll().Any(p => p.Id == publication.Id))
            {
                throw new ObjectAlreadyExistException($"Publication by Id: {publication.Id} already exists.");
            }

            IsAuthorFound(publication.AuthorId);

            if (publication.Images != null &&
                publication.Images.Count() != 0)
            {
                SaveImages(publication);
            }

            _publicationRepository.Add(publication);
            _publicationRepository.Commit();

            return publication;
        }

        private void IsAuthorFound(Guid id)
        {
            _getableUser.GetById(id);
        }

        private void SaveImages(PublicationEntity publication)
        {
            foreach (ImageEntity imageEntity in publication.Images)
            {
                _imageRepositorySystem.TrySave(imageEntity);
            }
        }
    }
}
