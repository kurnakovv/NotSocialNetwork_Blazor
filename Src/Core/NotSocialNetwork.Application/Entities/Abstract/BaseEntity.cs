using System;

namespace NotSocialNetwork.Application.Entities.Abstract
{
    public abstract class BaseEntity
    {
        public Guid Id { get; }
        public DateTimeOffset DateOfCreate { get; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            DateOfCreate = DateTime.Now;
        }
    }
}
