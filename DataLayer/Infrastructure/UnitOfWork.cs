using BusinessLayer.Infrastructure;
using BusinessLayer.Models;
using DataLayer.Contexts;
using DataLayer.Repositories;

namespace DataLayer.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context = new DataContext();

        public IRepository<Car> Cars => new CarRepository(_context);

        public IRepository<Order> Orders => new OrderRepository(_context);

        public void Save() => _context.SaveChanges();
    }
}