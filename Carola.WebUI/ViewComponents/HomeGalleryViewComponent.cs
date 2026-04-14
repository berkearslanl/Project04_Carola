using Carola.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.ViewComponents
{
    public class HomeGalleryViewComponent : ViewComponent
    {
        private readonly IGalleryService _galleryService;

        public HomeGalleryViewComponent(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _galleryService.GetAllGalleryAsync();
            return View(values);
        }
    }
}
