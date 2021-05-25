using Microsoft.EntityFrameworkCore;
using NotSocialNetwork.Application.Configs;
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
            IUserService userService)
        {
            _publicationRepository = publicationRepository;
            _userService = userService;
        }

        private readonly IRepository<PublicationEntity> _publicationRepository;
        private readonly IUserService _userService;

        public IEnumerable<PublicationEntity> GetAll()
        {
            return _publicationRepository.GetAll()
                       .Include(p => p.Images)
                       .Include(p => p.Author)
                           .ThenInclude(u => u.Image);
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

        public IEnumerable<PublicationEntity> GetAllByAuthorId(Guid authorId)
        {
            IsAuthorFound(authorId);

            var publications = GetAll().Where(a => a.Author.Id == authorId);

            if (publications.Count() == 0)
            {
                throw new ObjectNotFoundException($"User by id: {authorId} don't have a publications.");
            }

            return publications;
        }

        public IEnumerable<PublicationEntity> GetByPagination(int index)
        {
            if (index < 0)
            {
                throw new InvalidOperationException("Index cannot be less than 0.");
            }

            var countOfSkipItems = index * PaginationConfig.MAX_ITEMS;

            var publications = GetAll()
                                   .Skip(countOfSkipItems)
                                   .Take(PaginationConfig.MAX_ITEMS);

            if (publications.Count() == 0)
            {
                throw new ObjectNotFoundException("No more publications.");
            }

            return publications;
        }

        public PublicationEntity Add(PublicationEntity publication)
        {
            if (GetAll().Any(p => p.Id == publication.Id))
            {
                throw new ObjectAlreadyExistException($"Publication by Id: {publication.Id} already exists.");
            }

            IsAuthorFound(publication.AuthorId);

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
