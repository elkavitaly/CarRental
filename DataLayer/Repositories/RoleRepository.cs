using System.Collections.Generic;
using BusinessLayer.Infrastructure;
using DataLayer.Contexts;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataLayer.Repositories
{
    public class RoleRepository : IRepository<IdentityRole>
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context) => _context = context;

        public void Add(IdentityRole item) => _context.Roles.Add(item);

        public void Update(IdentityRole item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(IdentityRole item) => _context.Roles.Remove(item);

        public void Delete(string id) =>
            _context.Roles.Remove(_context.Roles.Find(id) ?? throw new KeyNotFoundException());

        public IdentityRole GetById(string id) => _context.Roles.Find(id);

        public IEnumerable<IdentityRole> GetAll() => _context.Roles;
    }
}