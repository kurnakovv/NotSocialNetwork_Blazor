using Microsoft.EntityFrameworkCore;
using NotSocialNetwork.Application.Entities.Abstract;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.DBContexts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NotSocialNetwork.Data.EFRepositories
{
    public class EFCoreRepository<TEntity> : IRepositoryAsync<TEntity>
        where TEntity : BaseEntity
    {
        public EFCoreRepository(
            AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = appDbContext.Set<TEntity>();
        }

        private readonly AppDbContext _appDbContext;
        private readonly DbSet<TEntity> _dbSet;

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _appDbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id);

            if (_appDbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
            await _appDbContext.SaveChangesAsync();

            return entity;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var oldEntity = await GetAsync(entity.Id);

            _appDbContext.Entry(oldEntity).CurrentValues.SetValues(entity);
            await _appDbContext.SaveChangesAsync();

            return entity;
        }

        public void Dispose()
        {
            _appDbContext.DisposeAsync();
        }
    }
}
