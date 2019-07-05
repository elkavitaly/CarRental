using System;
using System.Web.Mvc;
using BusinessLayer.Factory;
using BusinessLayer.Infrastructure;
using BusinessLayer.Models;
using BusinessLayer.Services;
using ViewLayer.Models;

namespace ViewLayer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CarAdminController : Controller
    {
        private readonly IAdminService _service;
        private readonly IUnitOfWork _unitOfWork;

        public CarAdminController()
        {
            _service = new AdminService();
            _unitOfWork = RepositoryFactory.Instance.Initialize;
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
            ViewBag.Url = "AddCar";
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
                    return RedirectToAction("Cars");
                }
                catch (Exception e)
                {
                    LoggerFactory.Logger.Error("Car wasn't added. Id: {0}. Message: {1}", car.Id, e.Message);
                }
            }

            ViewBag.Url = "AddCar";
            return View(car);
        }

        [HttpGet]
        public ActionResult EditCar(string id)
        {
            ViewBag.Url = "EditCar";
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

            return null;
        }
    }
}