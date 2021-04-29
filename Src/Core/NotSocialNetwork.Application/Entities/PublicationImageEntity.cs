using System;
using NotSocialNetwork.Application.Entities.Abstract;

namespace NotSocialNetwork.Application.Entities
{
    public class PublicationImageEntity : BaseEntity
    {
        public PublicationEntity Publication { get; set; }
        public ImageEntity Image { get; set; }
    }
}