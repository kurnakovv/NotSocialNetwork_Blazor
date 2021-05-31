using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using System;

namespace NotSocialNetwork.Application.UseCases.Publication
{
    public class EditPublication : IEditablePublication
    {
        public EditPublication(
            IGetablePublication getablePublication,
            IRepository<PublicationEntity> publicationRepository)
        {
            _getablePublication = getablePublication;
            _publicationRepository = publicationRepository;
        }

        private readonly IGetablePublication _getablePublication;
        private readonly IRepository<PublicationEntity> _publicationRepository;

        public PublicationEntity Update(PublicationEntity publication)
        {
            _getablePublication.GetById(publication.Id);

            _publicationRepository.Update(publication);
            _publicationRepository.Commit();

            return publication;
        }

        public PublicationEntity Delete(Guid id)
        {
            var publication = _getablePublication.GetById(id);

            _publicationRepository.Delete(publication.Id);
            _publicationRepository.Commit();

            return publication;
        }
    }
}
