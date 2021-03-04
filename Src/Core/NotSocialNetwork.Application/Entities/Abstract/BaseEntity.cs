using System;

namespace NotSocialNetwork.Application.Entities.Abstract
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset DateOfCreate { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            DateOfCreate = DateTime.Now;
        }
    }
}
