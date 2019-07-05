using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLayer.Models;
using DataLayer.Models;

namespace DataLayer.Tools
{
    /// <summary>
    /// Service class for mapping objects
    /// </summary>
    public static class Mapper
    {
        public static TTo Map<TTo, TFrom>(TFrom entity)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TFrom, TTo>()).CreateMapper();
            return mapper.Map<TFrom, TTo>(entity);
        }

        public static IEnumerable<TTo> MapEnumerable<TTo, TFrom>(IEnumerable<TFrom> entity)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TFrom, TTo>()).CreateMapper();
            return mapper.Map<IEnumerable<TFrom>, IEnumerable<TTo>>(entity);
        }

        public static IEnumerable<Order> MapOrders(IEnumerable<OrderEntity> orders)
        {
            return orders.Select(order => new Order
                {
                    CarEntityId = order.CarEntityId,
                    Car = Map<Car, CarEntity>(order.CarEntity),
                    Driver = order.Driver,
                    End = order.End,
                    Start = order.Start,
                    FullName = order.FullName,
                    Passport = order.Passport,
                    Id = order.Id,
                    UserId = order.UserId,
                    Total = order.Total,
                    DateTime = order.DateTime,
                    Status = order.Status
                })
                .ToList();
        }
    }
}