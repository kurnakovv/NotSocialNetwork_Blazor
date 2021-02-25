using System;
using System.Collections.Generic;

namespace NotSocialNetwork.Application.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        T Add(T t);
        T Update(T t);
        T Delete(Guid id);
        void Commit();
    }
}
