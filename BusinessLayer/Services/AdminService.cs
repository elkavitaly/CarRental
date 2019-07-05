using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Factory;
using BusinessLayer.Infrastructure;
using BusinessLayer.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BusinessLayer.Services
{
    /// <summary>
    /// Realization of IAdminService
    /// </summary>
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

        public void DeleteRole(string name)
        {
            var roleId = _unitOfWork.Roles.GetAll().First(r => r.Name.Equals(name)).Id;
            _unitOfWork.Roles.Delete(roleId);
            _unitOfWork.SaveIdentity();
        }

        public void AddUserToRole(string userId, string role)
        {
            var roleId = _unitOfWork.Roles.GetAll().First(r => r.Name.Equals(role)).Id;
            _unitOfWork.Users.GetAll().First(u => u.Id.Equals(userId)).Roles
                .Add(new IdentityUserRole() {RoleId = roleId, UserId = userId});
            _unitOfWork.SaveIdentity();
        }

        public void DeleteUserFromRole(string userId, string roleId)
        {
            var role = new IdentityUserRole() {UserId = userId, RoleId = roleId};
            _unitOfWork.Users.GetById(userId).Roles.Remove(role);
            _unitOfWork.SaveIdentity();
        }

        public IEnumerable<ApplicationUser> Users() => _unitOfWork.Users.GetAll();

        public void AddUser(ApplicationUser user)
        {
            _unitOfWork.Users.Add(user);
            _unitOfWork.SaveIdentity();
        }

        public void DeleteUser(string userId)
        {
            _unitOfWork.Users.Delete(_unitOfWork.Users.GetById(userId));
            _unitOfWork.SaveIdentity();
        }

        public IEnumerable<string> GetRoles(string userId)
        {
            var userRoles = _unitOfWork.Users.GetById(userId).Roles;
            var roles = _unitOfWork.Roles.GetAll().ToList();
            return userRoles.Select(userRole => roles.First(r => r.Id.Equals(userRole.RoleId)).Name).ToList();
        }
    }
}