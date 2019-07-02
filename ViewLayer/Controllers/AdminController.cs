using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Factory;
using BusinessLayer.Infrastructure;
using BusinessLayer.Models;
using BusinessLayer.Services;
using BusinessLayer.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ViewLayer.Models;
using ApplicationUser = ViewLayer.Models.ApplicationUser;

namespace ViewLayer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _service;
        private readonly IUnitOfWork _unitOfWork;

        public AdminController()
        {
            _service = new AdminService();
            _unitOfWork = RepositoryFactory.Instance.Initialize;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            return View();
        }

        public ActionResult UsersList()
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

            return RedirectPermanent("Users");
        }

        public ActionResult DeleteUser(string id)
        {
            _service.DeleteUser(id);
            return null; //RedirectToAction("Users");
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

        public ActionResult Cars()
        {
            return View();
        }

        public ActionResult CarsList()
        {
            return PartialView("CarsList", _unitOfWork.Cars.GetAll());
        }

        [HttpGet]
        public ActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCar(Car car)
        {
            if (ModelState.IsValid)
            {
                car.Id = Guid.NewGuid();
                _unitOfWork.Cars.Add(car);
                _unitOfWork.Save();
            }

            return RedirectPermanent("Cars");
        }

        [HttpGet]
        public ActionResult EditCar(string id)
        {
            return View("AddCar", _unitOfWork.Cars.GetById(id));
        }

        [HttpPost]
        public ActionResult EditCar(Car car)
        {
            _unitOfWork.Cars.Update(car);
            _unitOfWork.Save();
            return RedirectPermanent("Cars");
        }

        public ActionResult DeleteCar(string id)
        {
            _unitOfWork.Cars.Delete(id);
            _unitOfWork.Save();
            return null; //RedirectPermanent("Cars");
        }

        public ActionResult Orders()
        {
            return View();
        }

        public ActionResult OrdersList()
        {
            var orderList = _unitOfWork.Orders.GetAll();
            var users = _unitOfWork.Users;
            foreach (var order in orderList)
            {
                order.User = users.GetById(order.UserId.ToString("D"));
            }

            return PartialView("OrdersList", orderList);
        }

        public ActionResult DeleteOrder()
        {
            throw new NotImplementedException();
        }

        public ActionResult ConfirmOrder()
        {
            var id = Util.ReadStream(Request.InputStream);
            var order = _unitOfWork.Orders.GetById(id);
            order.Status = "Confirmed";
            _unitOfWork.Orders.Update(order);
//            var message = new IdentityMessage
//            {
//                Body = "Your order is confirmed.\nOrder number: " + order.Id,
//                Subject = "Car rental",
//                Destination = order.User.Email
//            };
//            var emailService = new EmailService();
//            await emailService.SendAsync(message);
            return null;
        }

        public ActionResult DeclineOrder()
        {
            var input = Util.ReadStream(Request.InputStream);
            var data = Util.Deserialize<string[]>(input);
            var order = _unitOfWork.Orders.GetById(data[0]);
            order.Status = "Declined";
            _unitOfWork.Orders.Update(order);
//            var message = new IdentityMessage
//            {
//                Body = "Order number: " + order.Id + "was declined.\n" + data[1],
//                Subject = "Car rental",
//                Destination = order.User.Email
//            };
//            var emailService = new EmailService();
//            await emailService.SendAsync(message);
            return null;
        }

        public ActionResult EditOrder(string id)
        {
            var order = _unitOfWork.Orders.GetById(id);
            order.Car = _unitOfWork.Cars.GetById(order.CarEntityId.ToString("D"));
            return View(order);
        }
    }
}