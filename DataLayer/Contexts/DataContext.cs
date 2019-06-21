using System.Data.Entity;
using DataLayer.Models;

namespace DataLayer.Contexts
{
    public class DataContext : DbContext
    {
        public DbSet<CarEntity> CarEntities { get; set; }
        public DbSet<OrderEntity> OrderEntities { get; set; }

        public DataContext() : base("CarRentalDatabase")
        {
        }
    }
}