using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using System;
using System.Threading.Tasks;

namespace NotSocialNetwork.Application.Interfaces.Systems
{
    public interface IImageFileSystemAsync
    {
        Task TrySaveAsync(ImageEntity image, string pathToSave);
        Task TryUpdateAsync(UpdateFileDTO updateFile);
        Task<Guid> DeleteAsync(Guid id, string filePath);
    }
}
