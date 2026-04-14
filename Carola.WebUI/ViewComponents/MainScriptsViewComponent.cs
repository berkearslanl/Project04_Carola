using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.ViewComponents
{
    public class MainScriptsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
