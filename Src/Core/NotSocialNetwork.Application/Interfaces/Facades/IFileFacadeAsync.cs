using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.Entities.Abstract;
using System;
using System.Threading.Tasks;

namespace NotSocialNetwork.Application.Interfaces.Facades
{
    public interface IFileFacadeAsync<TFile>
        where TFile : BaseEntity
    {
        Task<Guid> SaveAsync(TFile file, string pathToSave);
        Task<TFile> GetAsync(Guid id);
        Task<Guid> UpdateAsync(UpdateFileDTO updateFile);
        Task<Guid> DeleteAsync(Guid id, string filePath);
    }
}
