﻿using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities.Abstract;
using System;

namespace NotSocialNetwork.Application.Interfaces.Managers
{
    public interface IFileSystem<TFile> where TFile: BaseEntity
    {
        Guid Save(TFile file, string pathToSave);
        TFile Get(Guid id);
        Guid Update(UpdateFileDTO updateFile);
        Guid Delete(Guid id, string filePath);
    }
}