using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLayer.Factory;
using BusinessLayer.Infrastructure;
using BusinessLayer.Services;
using BusinessLayer.Utils;
using Microsoft.AspNet.Identity;
using ViewLayer.Models;

namespace ViewLayer.Controllers
{
    /// <summary>
    /// Provide functions for interacting with orders collection
    /// </summary>
    [Authorize(Roles = "Admin,Manager")]
    public class OrderAdminController : Controller
    {
        private readonly IAdminService _service;
        private readonly IUnitOfWork _unitOfWork;

        public OrderAdminController()
        {
            _service = new AdminService();
            _unitOfWork = RepositoryFactory.Instance.Initialize;
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

        public async Task<ActionResult> ConfirmOrder()
        {
            try
            {
                var id = Util.ReadStream(Request.InputStream);
                LoggerFactory.Logger.Info("Confirming order. Id: {0}", id);
                var order = _unitOfWork.Orders.GetById(id);
                order.Status = "Confirmed";
                _unitOfWork.Orders.Update(order);
                var user = _unitOfWork.Users.GetById(order.UserId.ToString("D"));
                var message = new IdentityMessage
                {
                    Body = "Your order is confirmed.\nOrder number: " + order.Id,
                    Subject = "Car rental",
                    Destination = user.Email
                };
                var emailService = new EmailService();
                await emailService.SendAsync(message);
                LoggerFactory.Logger.Info("Order was confirmed. Id: {0}", id);
            }
            catch (Exception e)
            {
                LoggerFactory.Logger.Error("Order wasn't confirmed. Message: {0}", e.Message);
            }

            return null;
        }

        public async Task<ActionResult> DeclineOrder()
        {
            try
            {
                var input = Util.ReadStream(Request.InputStream);
                var data = Util.Deserialize<string[]>(input);
                var order = _unitOfWork.Orders.GetById(data[0]);
                LoggerFactory.Logger.Info("Declining order. Id: {0}", order);
                order.Status = "Declined";
                _unitOfWork.Orders.Update(order);
                var user = _unitOfWork.Users.GetById(order.UserId.ToString("D"));
                var message = new IdentityMessage
                {
                    Body = "Order number: " + order.Id + " was declined.\n" + data[1],
                    Subject = "Car rental",
                    Destination = user.Email
                };
                var emailService = new EmailService();
                await emailService.SendAsync(message);
                LoggerFactory.Logger.Info("Order was declined. Id: {0}", order);
            }
            catch (Exception e)
            {
                LoggerFactory.Logger.Error("Order wasn't declined. Message: {0}", e.Message);
            }

            return null;
        }
    }
}