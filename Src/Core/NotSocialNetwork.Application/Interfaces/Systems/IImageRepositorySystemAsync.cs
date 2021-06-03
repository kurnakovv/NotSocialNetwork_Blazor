using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using System;
using System.Threading.Tasks;

namespace NotSocialNetwork.Application.Interfaces.Systems
{
    public interface IImageRepositorySystemAsync
    {
        Task<ImageEntity> TrySaveAsync(ImageEntity image);
        Task<ImageEntity> GetAsync(Guid id);
        Task<Guid> TryUpdateAsync(UpdateFileDTO updateFile);
        Task<Guid> TryDeleteAsync(Guid id);
    }
}
