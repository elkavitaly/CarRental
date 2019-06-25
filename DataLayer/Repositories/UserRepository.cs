using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Infrastructure;
using BusinessLayer.Models;
using DataLayer.Contexts;

namespace DataLayer.Repositories
{
    public class UserRepository : IRepository<ApplicationUser>
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) => _context = context;

        public void Add(ApplicationUser item) => _context.Users.Add(item);

        public void Update(ApplicationUser item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(ApplicationUser item) => _context.Users.Remove(item);

        public void Delete(string id) => _context.Users.Remove(GetById(id));

        public ApplicationUser GetById(string id) => _context.Users.First(user => user.Id == id);

        public IEnumerable<ApplicationUser> GetAll() => _context.Users;
    }
}