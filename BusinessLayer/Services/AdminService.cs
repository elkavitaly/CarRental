using System;
using System.Collections.Generic;
using BusinessLayer.Factory;
using BusinessLayer.Infrastructure;
using BusinessLayer.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BusinessLayer.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminService() => _unitOfWork = RepositoryFactory.Instance.Initialize;

        public IEnumerable<IdentityRole> Roles() => _unitOfWork.Roles.GetAll();

        public void AddRole(string name)
        {
            _unitOfWork.Roles.Add(new IdentityRole(name));
            _unitOfWork.SaveIdentity();
        }

        public void DeleteRole(Guid id)
        {
            _unitOfWork.Roles.Delete(_unitOfWork.Roles.GetById(id));
            _unitOfWork.SaveIdentity();
        }

        public void AddUserToRole(string userId, string roleId)
        {
//            _unitOfWork.Users.Roles.Add(new IdentityUserRole {RoleId = roleId, UserId = userId});
        }

        public void DeleteUserFromRole(string userId, string roleId) => throw new NotImplementedException();

        public IEnumerable<ApplicationUser> Users() => _unitOfWork.Users.GetAll();

        public void AddUser(ApplicationUser user)
        {
            _unitOfWork.Users.Add(user);
            _unitOfWork.SaveIdentity();
        }

        public void DeleteUser(Guid userId)
        {
            _unitOfWork.Users.Delete(_unitOfWork.Users.GetById(userId));
            _unitOfWork.SaveIdentity();
        }
    }
}