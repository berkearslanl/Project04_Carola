using Carola.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.ViewComponents
{
    public class HomeWhyChooseUsViewComponent : ViewComponent
    {
        private readonly IWhyUsService _whyUsService;

        public HomeWhyChooseUsViewComponent(IWhyUsService whyUsService)
        {
            _whyUsService = whyUsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _whyUsService.GetAllWhyUsAsync();
            return View(values);
        }
    }
}
