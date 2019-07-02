using System;
using System.Collections.Generic;
using System.Data.Entity;
using BusinessLayer.Infrastructure;
using BusinessLayer.Models;
using DataLayer.Contexts;
using DataLayer.Models;
using DataLayer.Tools;

namespace DataLayer.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context) => _context = context;

        public void Add(Order item) => _context.OrderEntities.Add(Mapper.Map<OrderEntity, Order>(item));

        public void Update(Order item)
        {
            var order = _context.OrderEntities.Find(item.Id);
            var props = order.GetType().GetProperties();
            foreach (var prop in props)
            {
                var name = prop.Name;
                if (name.Equals("Id") || name.Equals("CarEntityId") || name.Equals("CarEntity"))
                {
                    continue;
                }

                var value = item.GetType().GetProperty(name)?.GetValue(item);
                order.GetType().GetProperty(name)?.SetValue(order, value);
            }

            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Order item) => _context.OrderEntities.Remove(Mapper.Map<OrderEntity, Order>(item));

        public void Delete(string id) =>
            _context.OrderEntities.Remove(_context.OrderEntities.Find(Guid.Parse(id)) ??
                                          throw new KeyNotFoundException());

        public Order GetById(string id) => Mapper.Map<Order, OrderEntity>(_context.OrderEntities.Find(Guid.Parse(id)));

        public IEnumerable<Order> GetAll() => Mapper.MapOrders(_context.OrderEntities.Include("CarEntity"));
    }
}