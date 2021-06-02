using System;
using System.Linq;
using System.Threading.Tasks;

namespace NotSocialNetwork.Application.Interfaces.Repositories
{
    public interface IRepositoryAsync<TEntity> : IDisposable
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetAsync(Guid id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(Guid id);
    }
}
