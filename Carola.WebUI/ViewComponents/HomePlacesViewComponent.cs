using Carola.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Carola.WebUI.ViewComponents
{
    public class HomePlacesViewComponent : ViewComponent
    {
        private readonly ILocationService _locationService;

        public HomePlacesViewComponent(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _locationService.GetAllLocationAsync();
            return View(values);
        }
    }
}
