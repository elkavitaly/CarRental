using BusinessLayer.Models;

namespace BusinessLayer.Infrastructure
{
    public interface IUnitOfWork
    {
        IRepository<Car> Cars { get; }
        IRepository<Order> Orders { get; }
        void Save();
    }
}