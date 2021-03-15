using Microsoft.AspNetCore.Http;
using NotSocialNetwork.Application.Entities.Abstract;
using System;

namespace NotSocialNetwork.Application.Interfaces.Managers
{
    public interface IFileManager<TFile> where TFile: BaseEntity
    {
        Guid Save(TFile file, IFormFile fileFromForm, string pathToSave);
        TFile Get(Guid id);
        Guid Update(TFile file, IFormFile fileFromForm);
        Guid Delete(Guid id);
    }
}
