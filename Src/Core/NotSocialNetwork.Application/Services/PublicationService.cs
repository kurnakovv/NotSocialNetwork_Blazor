using Microsoft.EntityFrameworkCore;
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
            IRepository<PublicationEntity> publicationRepository,
            IRepository<PublicationImageEntity> publicationImageEntity,
            IUserService userService)
        {
            _publicationRepository = publicationRepository;
            _publicationImageEntity = publicationImageEntity;
            _userService = userService;
        }

        private readonly IRepository<PublicationEntity> _publicationRepository;
        private readonly IRepository<PublicationImageEntity> _publicationImageEntity;
        private readonly IUserService _userService;

        public IEnumerable<PublicationEntity> GetAll()
        {
            return _publicationRepository.GetAll()
                       .Include(p => p.Author);
                       //.Include(p => p.PublicationImages);
        }

        public PublicationEntity GetById(Guid id)
        {
            var publication = GetAll().FirstOrDefault(p => p.Id == id);

            if (publication == null)
            {
                throw new ObjectNotFoundException($"Publication by Id: {id} not found.");
            }

            return publication;
        }

        public PublicationEntity Add(PublicationEntity publication)
        {
            if (GetAll().Any(p => p.Id == publication.Id))
            {
                throw new ObjectAlreadyExistException($"Publication by Id: {publication.Id} already exists.");
            }

            IsAuthorFound(publication.AuthorId);

            if(publication.PublicationImages != null)
            {
                foreach (var publicationImage in publication.PublicationImages) 
                {
                    _publicationImageEntity.Add(publicationImage);
                }
            }
            
            _publicationRepository.Add(publication);
            _publicationRepository.Commit();

            return publication;
        }

        public PublicationEntity Update(PublicationEntity publication)
        {
            GetById(publication.Id);

            _publicationRepository.Update(publication);
            _publicationRepository.Commit();

            return publication;
        }

        public PublicationEntity Delete(Guid id)
        {
            var publication = GetById(id);

            _publicationRepository.Delete(publication.Id);
            _publicationRepository.Commit();

            return publication;
        }

        private void IsAuthorFound(Guid id)
        {
            _userService.GetById(id);
        }
    }
}
