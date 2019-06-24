using System.Collections.Generic;
using BusinessLayer.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BusinessLayer.Infrastructure
{
    public interface IAdminService
    {
        IEnumerable<IdentityRole> Roles();

        void AddRole(string name);

        void DeleteRole(string id);

        IEnumerable<ApplicationUser> Users();

        void AddUser(ApplicationUser user);

        void DeleteUser(string userId);

        void AddUserToRole(string userId, string roleId);

        void DeleteUserFromRole(string userId, string roleId);

        IEnumerable<string> GetRoles(string userId);
    }
}