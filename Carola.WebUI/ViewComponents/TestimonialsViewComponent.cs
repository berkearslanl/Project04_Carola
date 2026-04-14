using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.ViewComponents
{
    public class TestimonialsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
