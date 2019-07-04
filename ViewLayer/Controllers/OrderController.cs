using System;
using System.Web.Mvc;
using BusinessLayer.Factory;
using BusinessLayer.Infrastructure;
using BusinessLayer.Models;
using Microsoft.AspNet.Identity;
using ViewLayer.Models;

namespace ViewLayer.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController() => _unitOfWork = RepositoryFactory.Instance.Initialize;

        [HttpGet]
        public ActionResult Index(string id)
        {
            if (User.IsInRole("Blocked"))
            {
                return RedirectToAction("Index", "Catalog");
            }

            var car = _unitOfWork.Cars.GetById(id);
            return View(new Order {Car = car});
        }

        [HttpPost]
        public ActionResult Index(Order order)
        {
            LoggerFactory.Logger.Info("Making order");

            try
            {
                order.Id = Guid.NewGuid();
                order.UserId = Guid.Parse(User.Identity.GetUserId());
                order.CarEntityId = order.Car.Id;
                order.DateTime = DateTime.UtcNow;
                _unitOfWork.Orders.Add(order);
                _unitOfWork.Save();
                LoggerFactory.Logger.Info("Order was made. Id: {0}", order.Id);
                return View("OrderPayment", order.Total);
            }
            catch (Exception e)
            {
                LoggerFactory.Logger.Error("Order wasn't made. Message: {0}", e.Message);
            }

            return View(order);
        }

        public ActionResult OrderPayment(double total)
        {
            ViewBag.Total = total;
            return View();
        }
    }
}