using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.ViewComponents
{
    public class AdminScriptsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
