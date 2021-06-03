using NotSocialNetwork.Application.Entities;
using System.Threading.Tasks;

namespace NotSocialNetwork.Application.Interfaces.UseCases.Publication
{
    public interface IAddablePublicationAsync
    {
        Task<PublicationEntity> AddAsync(PublicationEntity publication);
    }
}
