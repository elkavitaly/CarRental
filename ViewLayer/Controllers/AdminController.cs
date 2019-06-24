using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Infrastructure;
using BusinessLayer.Services;
using BusinessLayer.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ViewLayer.Models;

namespace ViewLayer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _service;

        public AdminController() => _service = new AdminService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            return View();
        }

        public ActionResult ListUsers()
        {
            var users = _service.Users().ToList();
            var usersView = new List<ApplicationUserViewModel>();
            foreach (var user in users)
            {
                var roles = _service.GetRoles(user.Id);
                var sb = new StringBuilder();
                foreach (var role in roles)
                {
                    sb.Append(role).Append(" ");
                }

                usersView.Add(new ApplicationUserViewModel() {Email = user.Email, Id = user.Id, Roles = sb.ToString()});
            }

            return PartialView("ListUsers", usersView);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            ViewBag.Roles = _service.Roles().Select(role => role.Name).ToList();
            return View();
        }

        // add validation of model
        [HttpPost]
        public ActionResult AddUser(UserRegisterViewModel model)
        {
            var obj = Util.ReadStream(Request.InputStream);
            var user = Util.Deserialize<UserRegisterViewModel>(obj);

            var appUser = new ApplicationUser {UserName = user.Email, Email = user.Email};
            HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Create(appUser, user.Password);
            foreach (var role in user.Roles)
            {
                _service.AddUserToRole(appUser.Id, role);
            }

            return View("Users");
        }

        public ActionResult DeleteUser(string id)
        {
            _service.DeleteUser(id);
            return RedirectToAction("Users");
        }

        public ActionResult EditUser(string id)
        {
            return RedirectToAction("Users");
        }

        public ActionResult AddRole(string name)
        {
            _service.AddRole(name);
            return RedirectToAction("Users");
        }
    }
}