using NotSocialNetwork.Application.Entities;
using System;

namespace NotSocialNetwork.Application.Interfaces.UseCases.Publication
{
    public interface IEditablePublication
    {
        PublicationEntity Update(PublicationEntity publication);
        PublicationEntity Delete(Guid id);
    }
}
