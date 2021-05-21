using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using System;

namespace NotSocialNetwork.Application.Interfaces.Systems
{
    public interface IImageRepositorySystem
    {
        ImageEntity TrySave(ImageEntity image);
        ImageEntity Get(Guid id);
        Guid TryUpdate(UpdateFileDTO updateFile);
        Guid TryDelete(Guid id);
    }
}
