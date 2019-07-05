using System.Collections.Generic;

namespace BusinessLayer.Infrastructure
{
    /// <summary>
    /// Provide functions for working with data context of database
    /// </summary>
    /// <typeparam name="T"></typeparam>
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