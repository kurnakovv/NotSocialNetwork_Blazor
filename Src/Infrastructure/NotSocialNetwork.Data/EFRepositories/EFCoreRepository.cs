using Microsoft.EntityFrameworkCore;
using NotSocialNetwork.Application.Entities.Abstract;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.DBContexts;
using System;
using System.Linq;

namespace NotSocialNetwork.Data.EFRepositories
{
    public class EFCoreRepository<T> : IRepository<T> where T : BaseEntity
    {
        public EFCoreRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = appDbContext.Set<T>();
        }

        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbSet;

        public T Add(T t)
        {
            _dbSet.Add(t);
            return t;
        }

        public T Get(Guid id)
        {
            return _dbSet.Find(id);
        }

        public T Update(T t)
        {
            var oldT = Get(t.Id);
            _appDbContext.Entry(oldT).CurrentValues.SetValues(t);

            return t;
        }

        public T Delete(Guid id)
        {
            var t = Get(id);

            if (_appDbContext.Entry(t).State == EntityState.Detached)
            {
                _dbSet.Attach(t);
            }

            _dbSet.Remove(t);

            return t;
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public void Commit()
        {
            _appDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
