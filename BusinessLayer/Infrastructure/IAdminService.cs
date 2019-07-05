using System.Collections.Generic;
using BusinessLayer.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BusinessLayer.Infrastructure
{
    /// <summary>
    /// Provide functions for user's administration
    /// </summary>
    public interface IAdminService
    {
        IEnumerable<IdentityRole> Roles();

        void AddRole(string name);

        void DeleteRole(string id);

        IEnumerable<ApplicationUser> Users();

        void AddUser(ApplicationUser user);

        void DeleteUser(string userId);

        void AddUserToRole(string userId, string roleId);

        void DeleteUserFromRole(string userId, string roleName);

        IEnumerable<string> GetRoles(string userId);
    }
}