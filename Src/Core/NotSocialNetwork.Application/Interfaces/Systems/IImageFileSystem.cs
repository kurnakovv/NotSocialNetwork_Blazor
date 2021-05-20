using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities;
using System;

namespace NotSocialNetwork.Application.Interfaces.Systems
{
    public interface IImageFileSystem
    {
        void TrySave(ImageEntity image, string pathToSave);
        void TryUpdate(UpdateFileDTO updateFile);
        Guid Delete(Guid id, string filePath);
    }
}
