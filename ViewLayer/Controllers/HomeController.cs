using System.Web.Mvc;
using BusinessLayer.Factory;
using BusinessLayer.Infrastructure;

namespace ViewLayer.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public HomeController() => _unitOfWork = RepositoryFactory.Instance.Initialize;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}