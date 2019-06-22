using BusinessLayer.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BusinessLayer.Infrastructure
{
    public interface IUnitOfWork
    {
        IRepository<Car> Cars { get; }
        IRepository<Order> Orders { get; }
        void Save();

        IRepository<IdentityRole> Roles { get; }
        IRepository<ApplicationUser> Users { get; }
        void SaveIdentity();
    }
}