using System.Web.Mvc;
using BusinessLayer.Infrastructure;
using BusinessLayer.Services;
using DataLayer.Tools;
using ViewLayer.Models;

namespace ViewLayer.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service) => _service = new AdminService();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Users()
        {
            return View(_service.Users());
        }

        public ActionResult AddUser(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var entity = Mapper.Map<BusinessLayer.Models.ApplicationUser, ApplicationUser>(user);
                _service.AddUser(entity);
            }

            return View("Users");
        }

        public ActionResult AddRole(string name)
        {
            _service.AddRole(name);
            return View("Users");
        }
    }
}