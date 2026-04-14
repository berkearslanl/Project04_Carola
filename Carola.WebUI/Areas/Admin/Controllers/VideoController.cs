using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.VideoDtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VideoController : Controller
    {
        private readonly IVideoService _videoService;

        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        public async Task<IActionResult> VideoList()
        {
            ViewData["Title"] = "Video Yönetimi";
            ViewData["Desc"] = "Video Listesi";
            var values = await _videoService.GetAllVideoAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateVideo()
        {
            ViewData["Title"] = "Video Yönetimi";
            ViewData["Desc"] = "Video Ekleme";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateVideo(CreateVideoDto createVideoDto)
        {
            try
            {
                await _videoService.CreateVideoAsync(createVideoDto);
                return RedirectToAction("VideoList", new { area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(createVideoDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateVideo(int id)
        {
            ViewData["Title"] = "Video Yönetimi";
            ViewData["Desc"] = "Video Güncelleme";
            var value = await _videoService.GetVideoById(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVideo(UpdateVideoDto updateVideoDto)
        {
            try
            {
                await _videoService.UpdateVideoAsync(updateVideoDto);
                return RedirectToAction("VideoList", new { area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(updateVideoDto);
            }
        }

        public async Task<IActionResult> DeleteVideo(int id)
        {
            await _videoService.DeleteVideoAsync(id);
            return RedirectToAction("VideoList", new { area = "Admin" });
        }
    }
}
