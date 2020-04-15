using System.Web.Mvc;

namespace LogSystem.PL.Controllers
{
    [AllowAnonymous]
    public class MainController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

    }
}