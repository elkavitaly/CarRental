using System.Web.Mvc;

namespace ViewLayer.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}