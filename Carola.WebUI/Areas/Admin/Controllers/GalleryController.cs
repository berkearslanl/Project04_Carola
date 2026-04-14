using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.GalleryDtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GalleryController : Controller
    {
        private readonly IGalleryService _GalleryService;

        public GalleryController(IGalleryService GalleryService)
        {
            _GalleryService = GalleryService;
        }
        public async Task<IActionResult> GalleryList()
        {
            ViewData["Title"] = "Galeri Yönetimi";
            ViewData["Desc"] = "Galeri Listesi";

            var values = await _GalleryService.GetAllGalleryAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateGallery()
        {
            ViewData["Title"] = "Galeri Yönetimi";
            ViewData["Desc"] = "Galeri Ekleme";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateGallery(CreateGalleryDto createGalleryDto)
        {
            try
            {
                await _GalleryService.CreateGalleryAsync(createGalleryDto);
                return RedirectToAction("GalleryList", new { area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(createGalleryDto);
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateGallery(int id)
        {
            ViewData["Title"] = "Galeri Yönetimi";
            ViewData["Desc"] = "Galeri Güncelleme";
            var value = await _GalleryService.GetGalleryById(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateGallery(UpdateGalleryDto updateGalleryDto)
        {
            try
            {
                await _GalleryService.UpdateGalleryAsync(updateGalleryDto);
                return RedirectToAction("GalleryList", new { area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(updateGalleryDto);
            }
        }
        public async Task<IActionResult> DeleteGallery(int id)
        {
            await _GalleryService.DeleteGalleryAsync(id);
            return RedirectToAction("GalleryList", new { area = "Admin" });
        }
    }
}
