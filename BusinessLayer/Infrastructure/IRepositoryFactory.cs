using System.Dynamic;

namespace BusinessLayer.Infrastructure
{
    public interface IRepositoryFactory
    {
        IUnitOfWork Initialize { get; }
    }
}