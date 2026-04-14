using Carola.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.ViewComponents
{
    public class HomeVideoViewComponent : ViewComponent
    {
        private readonly IVideoService _videoService;

        public HomeVideoViewComponent(IVideoService videoService)
        {
            _videoService = videoService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _videoService.GetAllVideoAsync();
            return View(values);
        }
    }
}
