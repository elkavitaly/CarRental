using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Factory;
using BusinessLayer.Infrastructure;
using BusinessLayer.Services;
using BusinessLayer.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ViewLayer.Models;

namespace ViewLayer.Controllers
{
    /// <summary>
    /// Provide functions for interacting with users collection
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class UserAdminController : Controller
    {
        private readonly IAdminService _service;
        private readonly IUnitOfWork _unitOfWork;

        public UserAdminController()
        {
            _service = new AdminService();
            _unitOfWork = RepositoryFactory.Instance.Initialize;
        }

        public ActionResult Users()
        {
            ViewBag.Roles = _service.Roles().Select(role => role.Name).ToList();
            return View();
        }

        public ActionResult UsersList()
        {
            LoggerFactory.Logger.Info("Users list");
            var usersView = new List<ApplicationUserViewModel>();
            try
            {
                var users = _service.Users().ToList();
                foreach (var user in users)
                {
                    var roles = _service.GetRoles(user.Id);
                    var sb = new StringBuilder();
                    foreach (var role in roles)
                    {
                        sb.Append(role).Append(" ");
                    }

                    usersView.Add(new ApplicationUserViewModel()
                        {Email = user.Email, Id = user.Id, Roles = sb.ToString()});
                }
            }
            catch (Exception e)
            {
                LoggerFactory.Logger.Error("Users list wasn't formed. Message: {0}", e.Message);
            }


            return PartialView("ListUsers", usersView);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            ViewBag.Roles = _service.Roles().Select(role => role.Name).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(UserRegisterViewModel model)
        {
            try
            {
                LoggerFactory.Logger.Info("Adding new user");
                var obj = Util.ReadStream(Request.InputStream);
                var user = Util.Deserialize<UserRegisterViewModel>(obj);

                var appUser = new ApplicationUser {UserName = user.Email, Email = user.Email};
                var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                if (ValidatePassword(user.Password))
                {
                    manager.Create(appUser, user.Password);

                    foreach (var role in user.Roles)
                    {
                        _service.AddUserToRole(appUser.Id, role);
                    }
                }

                LoggerFactory.Logger.Info("User was added. Id: {0}", appUser.Id);
                return RedirectPermanent("Users");
            }
            catch (Exception e)
            {
                LoggerFactory.Logger.Error("User wasn't added. Message: {0}", e.Message);
            }

            return RedirectToAction("Users");
        }

        public ActionResult DeleteUser(string id)
        {
            try
            {
                LoggerFactory.Logger.Info("Deleting user. Id: {0}", id);
                _service.DeleteUser(id);
                LoggerFactory.Logger.Info("User was deleted. Id: {0}", id);
            }
            catch (Exception e)
            {
                LoggerFactory.Logger.Error("User wasn't deleted. Message: {0}", e.Message);
            }

            return null; //RedirectToAction("Users");
        }

        public ActionResult EditUser(string id)
        {
            return RedirectToAction("Users");
        }

        public ActionResult AddRole(string name)
        {
            LoggerFactory.Logger.Info("Adding role. Name: {0}", name);
            _service.AddRole(name);
            return RedirectToAction("Users");
        }

        public bool ValidatePassword(string password) =>
            password.Any(char.IsDigit) && password.Any(char.IsLower) && password.Any(char.IsUpper) &&
            (password.Any(char.IsSymbol) || password.Any(char.IsPunctuation));

        public string GetUsersRoles(string id)
        {
            var roles = _unitOfWork.Users.GetById(id).Roles;
            var result = roles.Select(role => _unitOfWork.Roles.GetById(role.RoleId).Name).ToList();
            return Util.Serialize(result);
        }

        public async Task<ActionResult> ChangeRoles()
        {
            var input = Util.ReadStream(Request.InputStream);
            var data = Util.Deserialize<string[]>(input);

            var id = data[0];
            var user = _unitOfWork.Users.GetById(id);
            var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roles = await manager.GetRolesAsync(user.Id);
            await manager.RemoveFromRolesAsync(user.Id, roles.ToArray());

            for (var i = 1; i < data.Length; i++)
            {
                _service.AddUserToRole(user.Id, data[i]);
            }

            return null;
        }
    }
}