using BusinessLayer.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BusinessLayer.Infrastructure
{
    /// <summary>
    /// Provide single access to all models in database
    /// </summary>
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