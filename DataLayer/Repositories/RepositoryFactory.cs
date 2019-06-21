using BusinessLayer.Infrastructure;
using DataLayer.Infrastructure;

namespace DataLayer.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IUnitOfWork Initialize => new UnitOfWork();
    }
}