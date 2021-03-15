using System;
using System.Collections.Generic;
using System.Linq;

namespace NotSocialNetwork.Application.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        T Get(Guid id);
        T Add(T t);
        T Update(T t);
        T Delete(Guid id);
        void Commit();
    }
}
