using System.Collections.Generic;
using BusinessLayer.Infrastructure;
using BusinessLayer.Models;
using DataLayer.Contexts;
using DataLayer.Models;
using DataLayer.Tools;

namespace DataLayer.Repositories
{
    public class CarRepository : IRepository<Car>
    {
        private readonly DataContext _context;

        public CarRepository(DataContext context) => _context = context;

        public void Add(Car item) => _context.CarEntities.Add(Mapper.Map<CarEntity, Car>(item));

        public void Delete(Car item) => _context.CarEntities.Remove(Mapper.Map<CarEntity, Car>(item));

        public void Delete(string id) =>
            _context.CarEntities.Remove(_context.CarEntities.Find(id) ?? throw new KeyNotFoundException());

        public Car GetById(string id) => Mapper.Map<Car, CarEntity>(_context.CarEntities.Find(id));

        public IEnumerable<Car> GetAll() => Mapper.MapEnumerable<Car, CarEntity>(_context.CarEntities);
    }
}