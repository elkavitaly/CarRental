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

        public ActionResult Cars()
        {
            return View();
        }

        public ActionResult CarsList()
        {
            LoggerFactory.Logger.Info("Car list");
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
            LoggerFactory.Logger.Info("Adding car");
            if (ModelState.IsValid)
            {
                try
                {
                    car.Id = Guid.NewGuid();
                    _unitOfWork.Cars.Add(car);
                    _unitOfWork.Save();
                    LoggerFactory.Logger.Info("Car was added. Id: {0}", car.Id);
                }
                catch (Exception e)
                {
                    LoggerFactory.Logger.Error("Car wasn't added. Id: {0}. Message: {1}", car.Id, e.Message);
                }
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
            try
            {
                LoggerFactory.Logger.Info("Editing car. Id: {0}", car.Id);
                _unitOfWork.Cars.Update(car);
                _unitOfWork.Save();
                LoggerFactory.Logger.Info("Car was edited. Id: {0}", car.Id);
            }
            catch (Exception e)
            {
                LoggerFactory.Logger.Error("Car wasn't edited. Id: {0}. Message: {1}", car.Id, e.Message);
            }

            return RedirectPermanent("Cars");
        }

        public ActionResult DeleteCar(string id)
        {
            try
            {
                LoggerFactory.Logger.Info("Deleting car. Id: {0}", id);
                _unitOfWork.Cars.Delete(id);
                _unitOfWork.Save();
                LoggerFactory.Logger.Info("Car was deleted. Id: {0}", id);
            }
            catch (Exception e)
            {
                LoggerFactory.Logger.Error("Car wasn't deleted. Message: {0}", e.Message);
            }

            return null; //RedirectPermanent("Cars");
        }

        public ActionResult Orders()
        {
            return View();
        }

        public ActionResult OrdersList()
        {
            LoggerFactory.Logger.Info("Orders list");
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
            try
            {
                var id = Util.ReadStream(Request.InputStream);
                LoggerFactory.Logger.Info("Confirming order. Id: {0}", id);
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
                LoggerFactory.Logger.Info("Order was confirmed. Id: {0}", id);
            }
            catch (Exception e)
            {
                LoggerFactory.Logger.Error("Order wasn't confirmed. Message: {0}", e.Message);
            }

            return null;
        }

        public ActionResult DeclineOrder()
        {
            try
            {
                var input = Util.ReadStream(Request.InputStream);
                var data = Util.Deserialize<string[]>(input);
                var order = _unitOfWork.Orders.GetById(data[0]);
                LoggerFactory.Logger.Info("Declining order. Id: {0}", order);
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
                LoggerFactory.Logger.Info("Order was declined. Id: {0}", order);
            }
            catch (Exception e)
            {
                LoggerFactory.Logger.Error("Order wasn't declined. Message: {0}", e.Message);
            }

            return null;
        }

        public ActionResult EditOrder(string id)
        {
            var order = _unitOfWork.Orders.GetById(id);
            order.Car = _unitOfWork.Cars.GetById(order.CarEntityId.ToString("D"));
            return View(order);
        }

        public bool ValidatePassword(string password) =>
            password.Any(char.IsDigit) && password.Any(char.IsLower) && password.Any(char.IsUpper) &&
            (password.Any(char.IsSymbol) || password.Any(char.IsPunctuation));
    }
}