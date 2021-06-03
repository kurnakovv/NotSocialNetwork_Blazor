using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using System;
using System.Threading.Tasks;

namespace NotSocialNetwork.Application.UseCases.Publication
{
    public class EditPublication : IEditablePublicationAsync
    {
        public EditPublication(
            IGetablePublication getablePublication,
            IRepositoryAsync<PublicationEntity> publicationRepository)
        {
            _getablePublication = getablePublication;
            _publicationRepository = publicationRepository;
        }

        private readonly IGetablePublication _getablePublication;
        private readonly IRepositoryAsync<PublicationEntity> _publicationRepository;

        public async Task<PublicationEntity> UpdateAsync(PublicationEntity publication)
        {
            _getablePublication.GetById(publication.Id);

            await _publicationRepository.UpdateAsync(publication);

            return publication;
        }

        public async Task<PublicationEntity> DeleteAsync(Guid id)
        {
            var publication = _getablePublication.GetById(id);

            await _publicationRepository.DeleteAsync(id);

            return publication;
        }
    }
}
