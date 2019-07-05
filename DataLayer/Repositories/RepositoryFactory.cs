using BusinessLayer.Infrastructure;
using DataLayer.Infrastructure;

namespace DataLayer.Repositories
{
    /// <summary>
    /// Create new instance of IUnitOfWork
    /// </summary>
    public class RepositoryFactory : IRepositoryFactory
    {
        public IUnitOfWork Initialize => new UnitOfWork();
    }
}