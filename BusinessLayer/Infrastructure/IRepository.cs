using System;
using System.Collections.Generic;

namespace BusinessLayer.Infrastructure
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Delete(T item);
        void Delete(Guid id);
        T GetById(Guid id);
        IEnumerable<T> GetAll();
    }
}