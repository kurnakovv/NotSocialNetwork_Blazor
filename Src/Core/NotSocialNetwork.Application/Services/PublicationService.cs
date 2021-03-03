using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotSocialNetwork.Application.Services
{
    public class PublicationService : IPublicationService
    {
        public PublicationService(
            IRepository<PublicationEntity> publicationRepository)
        {
            _publicationRepository = publicationRepository;
        }

        private readonly IRepository<PublicationEntity> _publicationRepository;

        public IEnumerable<PublicationEntity> GetAll() => _publicationRepository.GetAll();

        public PublicationEntity GetById(Guid id)
        {
            var publication = _publicationRepository.Get(id);

            if(publication == null)
            {
                throw new ObjectNotFoundException($"Publication by Id: {id} not found.");
            }

            return publication;
        }

        public PublicationEntity Add(PublicationEntity publication)
        {
            if (GetAll().Contains(publication))
            {
                throw new ObjectAlreadyExistException($"Publication by Id: {publication.Id} already exists.");
            }

            _publicationRepository.Add(publication);
            _publicationRepository.Commit();

            return publication;
        }

        public PublicationEntity Update(PublicationEntity publication)
        {
            _publicationRepository.Get(publication.Id);

            _publicationRepository.Update(publication);
            _publicationRepository.Commit();

            return publication;
        }

        public PublicationEntity Delete(Guid id)
        {
            var publication = _publicationRepository.Get(id);

            _publicationRepository.Delete(publication.Id);
            _publicationRepository.Commit();

            return publication;
        }
    }
}
