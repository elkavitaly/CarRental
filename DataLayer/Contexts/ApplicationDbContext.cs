using BusinessLayer.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataLayer.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public ApplicationDbContext Create() => this;
    }
}