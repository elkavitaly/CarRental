using System;
using System.Collections.Generic;
using BusinessLayer.Infrastructure;
using DataLayer.Contexts;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataLayer.Repositories
{
    /// <summary>
    /// Provide access to car table in database
    /// </summary>
    public class RoleRepository : IRepository<IdentityRole>
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context) => _context = context;

        public void Add(IdentityRole item) => _context.Roles.Add(item);

        public void Update(IdentityRole item)
        {
            throw new NotImplementedException();
        }

        public void Delete(IdentityRole item) => _context.Roles.Remove(item);

        public void Delete(string id) =>
            _context.Roles.Remove(_context.Roles.Find(id) ?? throw new KeyNotFoundException());

        public IdentityRole GetById(string id) => _context.Roles.Find(id);

        public IEnumerable<IdentityRole> GetAll() => _context.Roles;
    }
}