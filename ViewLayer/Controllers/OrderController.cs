using System;
using System.Web.Mvc;
using BusinessLayer.Factory;
using BusinessLayer.Infrastructure;
using BusinessLayer.Models;
using Microsoft.AspNet.Identity;

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
            var car = _unitOfWork.Cars.GetById(id);
            return View(new Order {Car = car});
        }

        [HttpPost]
        public ActionResult Index(Order order)
        {
            if (ModelState.IsValid)
            {
                order.Id = Guid.NewGuid();
                order.UserId = Guid.Parse(User.Identity.GetUserId());
                order.CarEntityId = order.Car.Id;
                _unitOfWork.Orders.Add(order);
                _unitOfWork.Save();
                return RedirectPermanent(Url.Action("Index", "Catalog"));
            }

            return View(order);
        }
    }
}