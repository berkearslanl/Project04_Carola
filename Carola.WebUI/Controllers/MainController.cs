using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
