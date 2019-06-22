using System;
using System.Collections.Generic;
using BusinessLayer.Infrastructure;
using BusinessLayer.Models;
using DataLayer.Contexts;

namespace DataLayer.Repositories
{
    public class UserRepository : IRepository<ApplicationUser>
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) => _context = context;

        public void Add(ApplicationUser item)
        {
            throw new NotImplementedException();
        }

        public void Delete(ApplicationUser item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}