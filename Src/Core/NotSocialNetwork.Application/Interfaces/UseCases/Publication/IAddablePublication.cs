using NotSocialNetwork.Application.Entities;

namespace NotSocialNetwork.Application.Interfaces.UseCases.Publication
{
    public interface IAddablePublication
    {
        PublicationEntity Add(PublicationEntity publication);
    }
}
