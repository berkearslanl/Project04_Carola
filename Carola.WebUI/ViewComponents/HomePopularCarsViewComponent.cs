using Carola.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Carola.WebUI.ViewComponents
{
    public class HomePopularCarsViewComponent : ViewComponent
    {
        private readonly ICarService _carService;

        public HomePopularCarsViewComponent(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _carService.GetLast6CarAsync();
            return View(values);
        }
    }
}
