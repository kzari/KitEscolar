using Microsoft.AspNetCore.Mvc;

namespace Kzari.KitEscolar.Web.Controllers.MVC
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}