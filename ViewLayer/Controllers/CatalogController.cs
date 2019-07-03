using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLayer.Factory;
using BusinessLayer.Infrastructure;
using BusinessLayer.Models;
using BusinessLayer.Services;
using BusinessLayer.Utils;
using ViewLayer.Models;

namespace ViewLayer.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IService _service;
        private readonly IUnitOfWork _unitOfWork;

        public CatalogController()
        {
            _unitOfWork = RepositoryFactory.Instance.Initialize;
            _service = new Service();
        }

        public ActionResult Index()
        {
            ViewBag.Filters = _service.FilterParameters();
            return View();
        }

        public string Item(string id)
        {
            var item = _service.GetById(id);
            return "result " + item.Name;
        }

        public async Task<ActionResult> Filter()
        {
            IEnumerable<Car> list = null;
            try
            {
                LoggerFactory.Logger.Info("Filtering catalog");
                var data = Util.ReadStream(Request.InputStream);
                var obj = Util.Deserialize<FilterViewModel>(data);
                list = await Task.Run(() => _service.Filter(obj.Filter));
                list = await Task.Run(() => _service.Sort(list, obj.Sort));
                LoggerFactory.Logger.Info("Catalog filtered");
            }
            catch (Exception e)
            {
                LoggerFactory.Logger.Error("Catalog wasn't filtered. Message: {0}", e.Message);
            }

            return PartialView("ListItems", list);
        }

        public async Task<ActionResult> Search()
        {
            var pattern = Util.ReadStream(Request.InputStream);
            var list = await Task.Run(() => _service.Search(_unitOfWork.Cars.GetAll(), pattern));
            return PartialView("ListItems", list);
        }
    }
}