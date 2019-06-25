using System.Collections.Generic;

namespace BusinessLayer.Infrastructure
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Update(T item);
        void Delete(T item);
        void Delete(string id);
        T GetById(string id);
        IEnumerable<T> GetAll();
    }
}