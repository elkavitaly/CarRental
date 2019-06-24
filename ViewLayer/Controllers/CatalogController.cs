using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLayer.Infrastructure;
using BusinessLayer.Services;
using BusinessLayer.Utils;
using ViewLayer.Models;

namespace ViewLayer.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IService _service;

        public CatalogController() => _service = new Service();

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
            var data = Util.ReadStream(Request.InputStream);
            var obj = Util.Deserialize<FilterViewModel>(data);
            var list = await Task.Run(() => _service.Filter(obj.Filter));
            list = await Task.Run(() => _service.Sort(list, obj.Sort));
            return PartialView("ListItems", list);
        }
    }
}