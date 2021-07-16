using Microsoft.EntityFrameworkCore;
using NotSocialNetwork.Application.Configs;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotSocialNetwork.Application.UseCases.Publication
{
    public class GetPublication : IGetablePublication
    {
        public GetPublication(
            IRepositoryAsync<PublicationEntity> publicationRepository,
            IGetableUser getableUser)
        {
            _publicationRepository = publicationRepository;
            _getableUser = getableUser;
        }

        private readonly IRepositoryAsync<PublicationEntity> _publicationRepository;
        private readonly IGetableUser _getableUser;

        public IEnumerable<PublicationEntity> GetAll()
        {
            return _publicationRepository.GetAll()
                             .Include(p => p.Images)
                             .Include(p => p.Author)
                                 .ThenInclude(u => u.Image);
        }

        public PublicationEntity GetById(Guid id)
        {
            var publication = GetAll()
                                  .FirstOrDefault(p => p.Id == id);

            CheckPublicationIsValid(publication, id);

            return publication;
        }

        public IEnumerable<PublicationEntity> GetAllByAuthorId(Guid authorId)
        {
            CheckAuthorIsValid(authorId);

            var publications = GetAll()
                                   .Where(a => a.Author.Id == authorId);

            CheckPublicationsCountIsValid(publications, $"User by id: {authorId} don't have a publications.");

            return publications;
        }

        public IEnumerable<PublicationEntity> GetByPagination(int index)
        {
            CheckIndexIsValid(index);
            var publications = GetPublicationsByPagination(index);
            CheckPublicationsCountIsValid(publications, "No more publications.");

            return publications;
        }

        private IEnumerable<PublicationEntity> GetPublicationsByPagination(int index)
        {
            var countOfSkipItems = index * PaginationConfig.MAX_ITEMS;
            var publications = GetAll()
                                   .Skip(countOfSkipItems)
                                   .Take(PaginationConfig.MAX_ITEMS);

            return publications;
        }

        private void CheckPublicationIsValid(PublicationEntity publication, Guid id)
        {
            if (publication == null)
            {
                throw new ObjectNotFoundException($"Publication by Id: {id} not found.");
            }
        }

        private void CheckAuthorIsValid(Guid id)
        {
            _getableUser.GetById(id);
        }

        private void CheckPublicationsCountIsValid(IEnumerable<PublicationEntity> publications, string exceptionMessage)
        {
            if (IsEmptyPublicationsCount(publications))
            {
                throw new ObjectNotFoundException(exceptionMessage);
            }
        }

        private void CheckIndexIsValid(int index)
        {
            if (IsInvalidIndex(index))
            {
                throw new InvalidOperationException("Index cannot be less than 0.");
            }
        }

        private bool IsEmptyPublicationsCount(IEnumerable<PublicationEntity> publications)
        {
            if (publications.Count() == 0)
            {
                return true;
            }

            return false;
        }

        private bool IsInvalidIndex(int index)
        {
            if (index < 0)
            {
                return true;
            }

            return false;
        }
    }
}
