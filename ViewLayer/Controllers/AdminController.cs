using System.Web.Mvc;

namespace ViewLayer.Controllers
{
    /// <summary>
    /// Admin panel
    /// </summary>
    [Authorize(Roles = "Admin,Manager")]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}