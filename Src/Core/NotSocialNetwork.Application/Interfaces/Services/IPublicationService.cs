using NotSocialNetwork.Application.Entities;
using System;
using System.Collections.Generic;

namespace NotSocialNetwork.Application.Interfaces.Services
{
    public interface IPublicationService
    {
        IEnumerable<PublicationEntity> GetAll();
        PublicationEntity GetById(Guid id);
        IEnumerable<PublicationEntity> GetAllByAuthorId(Guid authorId);
        PublicationEntity Add(PublicationEntity publication);
        PublicationEntity Update(PublicationEntity publication);
        PublicationEntity Delete(Guid id);
    }
}
