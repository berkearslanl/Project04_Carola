using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.ViewComponents
{
    public class AdminNavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
