using NotSocialNetwork.Application.Entities;
using System;
using System.Collections.Generic;

namespace NotSocialNetwork.Application.Interfaces.UseCases.Publication
{
    public interface IGetablePublication
    {
        IEnumerable<PublicationEntity> GetAll();
        PublicationEntity GetById(Guid id);
        IEnumerable<PublicationEntity> GetAllByAuthorId(Guid authorId);
        IEnumerable<PublicationEntity> GetByPagination(int index);
    }
}
