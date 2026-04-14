using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.BrandDtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        public async Task<IActionResult> BrandList()
        {
            ViewData["Title"] = "Marka Yönetimi";
            ViewData["Desc"] = "Marka Listesi";

            var values = await _brandService.GetAllBrandAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateBrand()
        {
            ViewData["Title"] = "Marka Yönetimi";
            ViewData["Desc"] = "Marka Ekleme";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDto createBrandDto)
        {
            try
            {
                await _brandService.CreateBrandAsync(createBrandDto);
                return RedirectToAction("BrandList", new { area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(createBrandDto);
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateBrand(int id)
        {
            ViewData["Title"] = "Marka Yönetimi";
            ViewData["Desc"] = "Marka Güncelleme";
            var value = await _brandService.GetBrandById(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto)
        {
            try
            {
                await _brandService.UpdateBrandAsync(updateBrandDto);
                return RedirectToAction("BrandList", new { area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(updateBrandDto);
            }
        }
        public async Task<IActionResult> DeleteBrand(int id)
        {
            await _brandService.DeleteBrandAsync(id);
            return RedirectToAction("BrandList", new { area = "Admin" });
        }
    }
}
