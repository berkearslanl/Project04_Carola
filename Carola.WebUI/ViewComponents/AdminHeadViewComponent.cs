using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.ViewComponents
{
    public class AdminHeadViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
