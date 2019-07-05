using BusinessLayer.Infrastructure;
using BusinessLayer.Models;
using DataLayer.Contexts;
using DataLayer.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataLayer.Infrastructure
{
    /// <summary>
    /// Realization of interface IUnitOfWork
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context = new DataContext();

        public IRepository<Car> Cars => new CarRepository(_context);

        public IRepository<Order> Orders => new OrderRepository(_context);

        public void Save() => _context.SaveChanges();

        private readonly ApplicationDbContext _identityContext = new ApplicationDbContext();

        public IRepository<IdentityRole> Roles => new RoleRepository(_identityContext);

        public IRepository<ApplicationUser> Users => new UserRepository(_identityContext);

        public void SaveIdentity() => _identityContext.SaveChanges();
    }
}