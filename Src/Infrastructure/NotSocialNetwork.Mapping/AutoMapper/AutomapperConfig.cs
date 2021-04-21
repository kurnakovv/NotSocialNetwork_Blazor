using AutoMapper;
using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using System.Linq;

namespace NotSocialNetwork.Mapping.AutoMapper
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            UserMapper();

            PublicationMapper();

            ImageMapper();
        }

        private void UserMapper()
        {
            CreateMap<UserEntity, RegistrationUserDTO>().ReverseMap();
        }

        private void PublicationMapper()
        {
            CreateMap<PublicationEntity, PublicationDTO>()
                .ForMember(p => p.ImagePaths,
                           opt => opt.MapFrom(src => src.PublicationImages.Select(i => i.Title)))
                .ReverseMap();

            CreateMap<PublicationEntity, AddPublicationDTO>().ReverseMap();
            CreateMap<PublicationEntity, UpdatePublicationDTO>().ReverseMap();
        }

        public void ImageMapper()
        {
            CreateMap<ImageEntity, UpdateFileDTO>();
        }
    }
}
