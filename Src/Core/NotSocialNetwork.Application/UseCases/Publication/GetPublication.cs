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
            IRepository<PublicationEntity> publicationRepository,
            IGetableUser getableUser)
        {
            _publicationRepository = publicationRepository;
            _getableUser = getableUser;
        }

        private readonly IRepository<PublicationEntity> _publicationRepository;
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

        private void IsAuthorFound(Guid id)
        {
            _getableUser.GetById(id);
        }
    }
}
