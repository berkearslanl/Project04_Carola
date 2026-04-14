using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.Controllers
{
    public class _MainLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
