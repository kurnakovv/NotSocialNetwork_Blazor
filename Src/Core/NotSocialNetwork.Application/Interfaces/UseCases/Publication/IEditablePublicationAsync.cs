using NotSocialNetwork.Application.Entities;
using System;
using System.Threading.Tasks;

namespace NotSocialNetwork.Application.Interfaces.UseCases.Publication
{
    public interface IEditablePublicationAsync
    {
        Task<PublicationEntity> UpdateAsync(PublicationEntity publication);
        Task<PublicationEntity> DeleteAsync(Guid id);
    }
}
